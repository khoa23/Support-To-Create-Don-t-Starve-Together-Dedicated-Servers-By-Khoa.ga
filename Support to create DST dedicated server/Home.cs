using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Support_to_create_DST_dedicated_server
{
    public partial class Home : Form
    {
        // ─── State ───────────────────────────────────────────────────────
        private string folderGameDST   = "";   // e.g. C:\...\Don't Starve Together
        private string path_cluster_full = ""; // full path to Cluster folder
        private string path_cluster      = ""; // relative e.g. 440725477\Cluster_1

        private string hostNameA = "", descriptionA = "", passwordA = "", maxPlayerA = "";

        // ─── Running Processes ────────────────────────────────────────────
        private Process processMaster = null;
        private Process processCaves  = null;

        // ─── Constructor ─────────────────────────────────────────────────
        public Home()
        {
            InitializeComponent();
            AutoDetectGameDirectory();
            SetServerRunning(false);
        }

        // ═══════════════════════════════════════════════════════════════
        // AUTO-DETECT  (vox-launcher style)
        // ═══════════════════════════════════════════════════════════════

        /// <summary>
        /// Tries to find the DST game directory from registry (same logic as vox-launcher).
        /// </summary>
        private void AutoDetectGameDirectory()
        {
            try
            {
                // Method 1: Steam App 322330 registry key (most reliable)
                using var key1 = Registry.LocalMachine.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 322330");
                if (key1 != null)
                {
                    string path = key1.GetValue("InstallLocation") as string;
                    if (TrySetGameDirectory(path)) return;
                }
            }
            catch { }

            try
            {
                // Method 2: Steam SteamPath + steamapps/common
                using var key2 = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
                if (key2 != null)
                {
                    string steamPath = (key2.GetValue("SteamPath") as string)?.Replace("/", "\\");
                    if (!string.IsNullOrEmpty(steamPath))
                    {
                        string candidate = Path.Combine(steamPath, "steamapps", "common", "Don't Starve Together");
                        if (TrySetGameDirectory(candidate)) return;

                        // Also scan other library folders
                        string vdf = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
                        if (File.Exists(vdf))
                        {
                            foreach (Match m in Regex.Matches(File.ReadAllText(vdf), "\"path\"\\s+\"(.+?)\""))
                            {
                                string libPath = m.Groups[1].Value.Replace("\\\\", "\\");
                                string dst = Path.Combine(libPath, "steamapps", "common", "Don't Starve Together");
                                if (TrySetGameDirectory(dst)) return;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private bool TrySetGameDirectory(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            if (!ValidateGameDirectory(path)) return false;

            folderGameDST = path;
            lbFolderGame.Text = path;
            UpdateDot(picGameStatus, true);
            return true;
        }

        // ═══════════════════════════════════════════════════════════════
        // LAUNCH DATA  (vox-launcher: retrieve ownerdir, storage_root, ugc_dir)
        // ═══════════════════════════════════════════════════════════════

        private class LaunchData
        {
            public string OwnerDir            { get; set; } = "";
            public string PersistentStorageRoot { get; set; } = "";
            public string UgcDirectory        { get; set; } = "";
        }

        /// <summary>
        /// Attempts to retrieve launch data exactly like vox-launcher:
        /// 1. Read it from cluster's Master/server_log.txt command line args.
        /// 2. Fall back to sibling cluster log files.
        /// 3. Fall back to auto-computed paths from registry/filesystem.
        /// </summary>
        private LaunchData GetLaunchData()
        {
            // Try reading from existing server_log.txt (like vox-launcher does)
            var data = TryReadLaunchDataFromLog(path_cluster_full);
            if (data != null) return data;

            // Try sibling clusters
            string parent = Path.GetDirectoryName(path_cluster_full) ?? "";
            if (!string.IsNullOrEmpty(parent))
            {
                foreach (string sibling in Directory.GetDirectories(parent))
                {
                    if (sibling == path_cluster_full) continue;
                    data = TryReadLaunchDataFromLog(sibling);
                    if (data != null) return data;
                }
            }

            // Fall back: compute from registry / filesystem
            return ComputeLaunchData();
        }

        private static LaunchData TryReadLaunchDataFromLog(string clusterPath)
        {
            string logPath = Path.Combine(clusterPath, "Master", "server_log.txt");
            if (!File.Exists(logPath)) return null;

            string text = File.ReadAllText(logPath, Encoding.UTF8);

            // vox-launcher checks for -backup_log_count to confirm the log is from a real run
            string backupCount = FindArg(text, "backup_log_count");
            if (string.IsNullOrEmpty(backupCount)) return null;

            string ownerDir  = FindArg(text, "ownerdir");
            string storageRoot = FindArg(text, "persistent_storage_root");
            string ugcDir    = FindArg(text, "ugc_directory");

            if (string.IsNullOrEmpty(ownerDir) && string.IsNullOrEmpty(storageRoot))
                return null;

            return new LaunchData
            {
                OwnerDir             = ownerDir,
                PersistentStorageRoot = storageRoot,
                UgcDirectory         = ugcDir,
            };
        }

        /// <summary>
        /// Regex: extract value of -argName from DST server command line log.
        /// Pattern: -argName value -nextArg  (same as vox-launcher's _find_command_line_argument)
        /// </summary>
        private static string FindArg(string text, string argName)
        {
            // Try the vox-launcher pattern first: "-arg value -"
            var m = Regex.Match(text, $@"-{argName}\s+(.+?)\s+-");
            if (m.Success) return m.Groups[1].Value.Trim();

            // Fallback: "-arg value" at end of line
            m = Regex.Match(text, $@"-{argName}\s+([^\r\n]+)");
            return m.Success ? m.Groups[1].Value.Trim() : "";
        }

        /// <summary>
        /// Compute launch paths from filesystem/registry.
        /// DST server requires:
        ///   -persistent_storage_root  FULL path  (e.g. C:\Users\user\Documents\Klei)
        ///   -ownerdir                 RELATIVE    (e.g. DoNotStarveTogether  or  DoNotStarveTogether\440725477)
        ///   -cluster                  RELATIVE to ownerdir  (e.g. Cluster_1)
        /// </summary>
        private LaunchData ComputeLaunchData()
        {
            string docs          = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string persistentRoot = Path.Combine(docs, "Klei");        // -persistent_storage_root
            string dstFull       = Path.Combine(persistentRoot, "DoNotStarveTogether");

            // -ownerdir is RELATIVE to persistentRoot
            string ownerDir = "DoNotStarveTogether";  // default

            if (Directory.Exists(dstFull))
            {
                // Check if there's a numeric user-id subfolder (e.g. 440725477)
                foreach (string sub in Directory.GetDirectories(dstFull))
                {
                    string name = Path.GetFileName(sub);
                    if (name.All(char.IsDigit) && File.Exists(Path.Combine(sub, "client.ini")))
                    {
                        ownerDir = Path.Combine("DoNotStarveTogether", name);
                        break;
                    }
                }
            }

            return new LaunchData
            {
                OwnerDir             = ownerDir,        // RELATIVE
                PersistentStorageRoot = persistentRoot,  // FULL path
                UgcDirectory         = FindSteamWorkshopPath(),
            };
        }

        private string FindSteamWorkshopPath()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
                if (key == null) return "";
                string steamPath = (key.GetValue("SteamPath") as string)?.Replace("/", "\\");
                if (string.IsNullOrEmpty(steamPath)) return "";

                // Main library
                string main = Path.Combine(steamPath, "steamapps", "workshop");
                if (Directory.Exists(main)) return main;

                // Scan other libraries
                string vdf = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
                if (File.Exists(vdf))
                {
                    foreach (Match m in Regex.Matches(File.ReadAllText(vdf), "\"path\"\\s+\"(.+?)\""))
                    {
                        string libPath = m.Groups[1].Value.Replace("\\\\", "\\");
                        string workshop = Path.Combine(libPath, "steamapps", "workshop");
                        if (Directory.Exists(workshop)) return workshop;
                    }
                }
            }
            catch { }
            return "";
        }

        // ═══════════════════════════════════════════════════════════════
        // VALIDATION
        // ═══════════════════════════════════════════════════════════════

        private static readonly string[] SERVER_EXES = {
            @"bin64\dontstarve_dedicated_server_nullrenderer_x64.exe",
            @"bin64\dontstarve_dedicated_server_r_x64.exe"
        };

        private static bool ValidateGameDirectory(string dir)
        {
            if (!Directory.Exists(dir)) return false;
            return SERVER_EXES.Any(exe => File.Exists(Path.Combine(dir, exe)));
        }

        private static string GetServerExe(string gameDir)
            => SERVER_EXES.Select(e => Path.Combine(gameDir, e)).FirstOrDefault(File.Exists);

        private static List<string> GetShardNames(string clusterPath)
        {
            var list = new List<string>();
            if (!Directory.Exists(clusterPath)) return list;
            foreach (string d in Directory.GetDirectories(clusterPath))
                if (File.Exists(Path.Combine(d, "server.ini")))
                    list.Add(Path.GetFileName(d));
            list.Sort((a, b) =>
            {
                int ai = a == "Master" ? 0 : a == "Caves" ? 1 : 2;
                int bi = b == "Master" ? 0 : b == "Caves" ? 1 : 2;
                return ai.CompareTo(bi);
            });
            return list;
        }

        // ═══════════════════════════════════════════════════════════════
        // UI BUTTON HANDLERS
        // ═══════════════════════════════════════════════════════════════

        private void btnOpenFolderGame_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog
            {
                Description = "Chọn thư mục Don't Starve Together\n(steamapps\\common\\Don't Starve Together)"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;

            if (!ValidateGameDirectory(fbd.SelectedPath))
            {
                MessageBox.Show(
                    "Thư mục không hợp lệ!\nCần chứa: bin64\\dontstarve_dedicated_server_nullrenderer_x64.exe",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TrySetGameDirectory(fbd.SelectedPath);
        }

        private void btnOpenCluster_Click(object sender, EventArgs e)
        {
            string dstPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Klei", "DoNotStarveTogether");

            using var fbd = new FolderBrowserDialog
            {
                Description     = "Chọn thư mục Cluster (ví dụ: Cluster_1)",
                SelectedPath    = Directory.Exists(dstPath) ? dstPath
                                    : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;

            if (!File.Exists(Path.Combine(fbd.SelectedPath, "cluster.ini")))
            {
                MessageBox.Show("Không tìm thấy cluster.ini — đây không phải thư mục Cluster hợp lệ!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            path_cluster_full = fbd.SelectedPath;

            // Compute cluster path RELATIVE to ownerDir
            // DST server arg: -cluster <name>  where name is relative to ownerDir
            // e.g. ownerDir="DoNotStarveTogether\440725477"
            //      ownerDirFull="C:\...\Klei\DoNotStarveTogether\440725477"
            //      selected = "C:\...\Klei\DoNotStarveTogether\440725477\Cluster_1"
            //      path_cluster = "Cluster_1"  ✅
            var ld = ComputeLaunchData();
            string ownerDirFull = Path.Combine(ld.PersistentStorageRoot, ld.OwnerDir);

            if (fbd.SelectedPath.StartsWith(
                    ownerDirFull + Path.DirectorySeparatorChar,
                    StringComparison.OrdinalIgnoreCase))
            {
                path_cluster = fbd.SelectedPath.Substring(ownerDirFull.Length + 1);
            }
            else
            {
                // Fallback: try to extract just the Cluster_N name
                path_cluster = Path.GetFileName(fbd.SelectedPath);
            }

            lbFolderCluster.Text = fbd.SelectedPath;
            UpdateDot(picClusterStatus, true);

            // Load cluster info
            string ini = File.ReadAllText(Path.Combine(fbd.SelectedPath, "cluster.ini"));
            hostNameA    = ParseIni(ini, "cluster_name");
            descriptionA = ParseIni(ini, "cluster_description");
            passwordA    = ParseIni(ini, "cluster_password");
            maxPlayerA   = ParseIni(ini, "max_players");

            txtServerName.Text  = hostNameA;
            txtDescription.Text = descriptionA;
            txtPassword.Text    = passwordA;
            txtMaxPlayer.Text   = maxPlayerA;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path_cluster_full)) { ShowWarn("Vui lòng chọn Cluster trước!"); return; }

            string iniPath = Path.Combine(path_cluster_full, "cluster.ini");
            string ini = File.ReadAllText(iniPath);
            ini = ReplaceIni(ini, "cluster_name",        hostNameA,    txtServerName.Text);
            ini = ReplaceIni(ini, "cluster_description", descriptionA, txtDescription.Text);
            ini = ReplaceIni(ini, "cluster_password",    passwordA,    txtPassword.Text);
            ini = ReplaceIni(ini, "max_players",         maxPlayerA,   txtMaxPlayer.Text);
            File.WriteAllText(iniPath, ini);

            hostNameA = txtServerName.Text;
            descriptionA = txtDescription.Text;
            passwordA = txtPassword.Text;
            maxPlayerA = txtMaxPlayer.Text;

            SetStatus("✓ Đã lưu thông tin server!");
        }

        // ═══════════════════════════════════════════════════════════════
        // LAUNCH / STOP  (vox-launcher style: direct Process + args)
        // ═══════════════════════════════════════════════════════════════

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (processMaster != null || processCaves != null)
            {
                StopServer();
                return;
            }

            if (!ValidateReadyToRun()) return;

            // Save token if provided
            string tokenPath = Path.Combine(path_cluster_full, "cluster_token.txt");
            if (!string.IsNullOrWhiteSpace(txtToken.Text))
                File.WriteAllText(tokenPath, txtToken.Text.Trim());
            else if (!File.Exists(tokenPath))
            {
                ShowWarn("Thiếu Token! Nhập token hoặc đặt file cluster_token.txt.");
                return;
            }

            // Write mods setup file (server will handle downloading)
            var modIds = ReadModIds();
            if (modIds.Count > 0)
                WriteModsSetupFile(modIds);

            txtLogMaster.Clear();
            txtLogCaves.Clear();

            SetStatus("🔄 Đang lấy launch data...");

            Task.Run(() =>
            {
                var launchData = GetLaunchData();
                Invoke((MethodInvoker)(() => LaunchShards(launchData)));
            });
        }

        private bool ValidateReadyToRun()
        {
            if (!ValidateGameDirectory(folderGameDST))
            {
                ShowWarn("Vui lòng chọn thư mục game Don't Starve Together hợp lệ!");
                return false;
            }
            if (string.IsNullOrEmpty(path_cluster_full) ||
                !File.Exists(Path.Combine(path_cluster_full, "cluster.ini")))
            {
                ShowWarn("Vui lòng chọn thư mục Cluster hợp lệ!");
                return false;
            }
            return true;
        }

        private void LaunchShards(LaunchData launchData)
        {
            string exePath = GetServerExe(folderGameDST);
            if (exePath == null)
            {
                ShowErr("Không tìm thấy file thực thi server trong thư mục game!");
                return;
            }

            if (string.IsNullOrEmpty(launchData.OwnerDir))
            {
                AppendLog(txtLogMaster,
                    "[Warning] Không tìm thấy ownerdir. Hãy chạy server từ game ít nhất 1 lần,\n" +
                    "         hoặc tự điền đường dẫn vào cluster_token.txt trước.");
            }

            string cwd  = Path.GetDirectoryName(exePath);
            var shards  = GetShardNames(path_cluster_full);
            bool anyStarted = false;

            if (shards.Contains("Master"))
            {
                processMaster = LaunchOneShard(exePath, cwd, "Master", txtLogMaster, launchData);
                anyStarted |= processMaster != null;
            }
            if (shards.Contains("Caves"))
            {
                processCaves = LaunchOneShard(exePath, cwd, "Caves", txtLogCaves, launchData);
                anyStarted |= processCaves != null;
            }

            SetServerRunning(anyStarted);
            SetStatus(anyStarted ? "🟢 Server đang chạy!" : "⚠️ Không khởi động được shard nào");
        }

        private Process LaunchOneShard(string exePath, string cwd,
            string shardName, RichTextBox logBox, LaunchData launch)
        {
            try
            {
                // Build arguments exactly like vox-launcher
                var args = new StringBuilder();
                args.Append($"-cluster \"{path_cluster}\"");
                args.Append($" -shard {shardName}");

                if (!string.IsNullOrEmpty(launch.OwnerDir))
                    args.Append($" -ownerdir \"{launch.OwnerDir}\"");

                if (!string.IsNullOrEmpty(launch.PersistentStorageRoot))
                    args.Append($" -persistent_storage_root \"{launch.PersistentStorageRoot}\"");

                if (!string.IsNullOrEmpty(launch.UgcDirectory))
                    args.Append($" -ugc_directory \"{launch.UgcDirectory}\"");

                string token = txtToken.Text.Trim();
                if (!string.IsNullOrEmpty(token))
                    args.Append($" -token \"{token}\"");

                var psi = new ProcessStartInfo
                {
                    FileName               = exePath,
                    WorkingDirectory       = cwd,
                    Arguments              = args.ToString(),
                    UseShellExecute        = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    RedirectStandardInput  = true,
                    CreateNoWindow         = true,
                    StandardOutputEncoding = Encoding.UTF8,
                };

                var p = new Process { StartInfo = psi, EnableRaisingEvents = true };
                p.OutputDataReceived += (s, ev) => AppendLog(logBox, ev.Data);
                p.ErrorDataReceived  += (s, ev) => AppendLog(logBox, ev.Data);
                p.Exited             += (s, ev) => OnShardExited(shardName);
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();

                AppendLog(logBox, $"[{shardName}] Server started (PID {p.Id})");
                AppendLog(logBox, $"[{shardName}] Args: {args}");
                return p;
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)(() =>
                    ShowErr($"Không thể khởi động shard {shardName}:\n{ex.Message}")));
                return null;
            }
        }

        private void OnShardExited(string shardName)
        {
            Invoke((MethodInvoker)delegate
            {
                if (shardName == "Master") processMaster = null;
                else processCaves = null;

                if (processMaster == null && processCaves == null)
                {
                    SetServerRunning(false);
                    SetStatus("🔴 Server đã dừng");
                }
                else
                {
                    SetStatus($"⚠️ Shard {shardName} đã dừng");
                }
            });
        }

        private void StopServer()
        {
            SetStatus("Đang dừng server...");
            KillProcess(ref processMaster, "Master");
            KillProcess(ref processCaves,  "Caves");
            SetServerRunning(false);
            SetStatus("🔴 Server đã dừng");
        }

        private void KillProcess(ref Process p, string name)
        {
            if (p == null) return;
            try
            {
                if (!p.HasExited)
                {
                    try { p.StandardInput.WriteLine("c_shutdown()"); } catch { }
                    if (!p.WaitForExit(3000)) p.Kill();
                }
            }
            catch { }
            finally { p?.Dispose(); p = null; }
        }

        // ═══════════════════════════════════════════════════════════════
        // MODS SETUP  (vox-launcher approach: only write setup.lua)
        // ═══════════════════════════════════════════════════════════════

        private List<string> ReadModIds()
        {
            string path = Path.Combine(path_cluster_full, "Master", "modoverrides.lua");
            if (!File.Exists(path)) return new List<string>();

            var matches = Regex.Matches(File.ReadAllText(path),
                @"workshop-(\d+)");
            return matches.Cast<Match>()
                          .Select(m => m.Groups[1].Value)
                          .Distinct()
                          .ToList();
        }

        /// <summary>
        /// Writes dedicated_server_mods_setup.lua into the game's mods folder,
        /// exactly as vox-launcher does — only lists mods not already present locally.
        /// The dedicated server executable reads this file and downloads missing mods
        /// automatically on startup.
        /// </summary>
        private void WriteModsSetupFile(List<string> modIds)
        {
            string modsFolder = Path.Combine(folderGameDST, "mods");
            try { Directory.CreateDirectory(modsFolder); } catch { }

            string setupFile = Path.Combine(modsFolder, "dedicated_server_mods_setup.lua");
            var sb = new StringBuilder();
            sb.AppendLine("-- Auto-generated by DST Launcher - Khoa.ga");
            sb.AppendLine("-- The dedicated server will download these mods on startup.");
            sb.AppendLine();

            foreach (string id in modIds)
            {
                if (string.IsNullOrWhiteSpace(id)) continue;
                // Only add mods not already present locally (same logic as vox-launcher)
                string modPath = Path.Combine(modsFolder, "workshop-" + id);
                if (!Directory.Exists(modPath))
                    sb.AppendLine($"ServerModSetup(\"{id}\")");
                else
                    sb.AppendLine($"-- workshop-{id} (already local)");
            }

            File.WriteAllText(setupFile, sb.ToString(), new UTF8Encoding(false));
        }

        // ═══════════════════════════════════════════════════════════════
        // CONSOLE INPUT
        // ═══════════════════════════════════════════════════════════════

        private void btnSendMaster_Click(object sender, EventArgs e) =>
            SendCommand(processMaster, txtConsoleMaster, txtLogMaster);
        private void btnSendCaves_Click(object sender, EventArgs e) =>
            SendCommand(processCaves,  txtConsoleCaves,  txtLogCaves);

        private void txtConsoleMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { SendCommand(processMaster, txtConsoleMaster, txtLogMaster); e.SuppressKeyPress = true; }
        }
        private void txtConsoleCaves_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { SendCommand(processCaves, txtConsoleCaves, txtLogCaves); e.SuppressKeyPress = true; }
        }

        private void SendCommand(Process p, TextBox input, RichTextBox log)
        {
            if (p == null || p.HasExited) return;
            string cmd = input.Text.Trim();
            if (string.IsNullOrEmpty(cmd)) return;
            try
            {
                p.StandardInput.WriteLine(cmd);
                AppendLog(log, "> " + cmd);
                input.Clear();
            }
            catch (Exception ex) { AppendLog(log, "[Error] " + ex.Message); }
        }

        // ═══════════════════════════════════════════════════════════════
        // LOG HELPER
        // ═══════════════════════════════════════════════════════════════

        private void AppendLog(RichTextBox box, string text)
        {
            if (string.IsNullOrEmpty(text) || box.IsDisposed) return;
            if (box.InvokeRequired) { box.Invoke((MethodInvoker)(() => AppendLog(box, text))); return; }

            Color c = Color.FromArgb(210, 210, 210);
            if (text.Contains("[Error]") || text.Contains("error:") || text.Contains("LUA ERROR"))
                c = Color.FromArgb(255, 100, 100);
            else if (text.Contains("uploads added") || text.Contains("Server started"))
                c = Color.FromArgb(100, 255, 150);
            else if (text.Contains("[Warning]") || text.Contains("warning"))
                c = Color.FromArgb(255, 220, 80);
            else if (text.StartsWith(">"))
                c = Color.FromArgb(100, 195, 255);

            box.SelectionStart  = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor  = c;
            box.AppendText(text + "\n");
            box.ScrollToCaret();
        }

        // ═══════════════════════════════════════════════════════════════
        // UI STATE HELPERS
        // ═══════════════════════════════════════════════════════════════

        private void SetServerRunning(bool running)
        {
            if (InvokeRequired) { Invoke((MethodInvoker)(() => SetServerRunning(running))); return; }
            btnLaunch.Text      = running ? "⏹ STOP SERVER" : "🚀 LAUNCH SERVER";
            btnLaunch.BackColor = running ? Color.FromArgb(239, 68, 68) : Color.FromArgb(16, 185, 129);
            btnSave.Enabled = !running;
        }

        private void SetStatus(string msg)
        {
            if (InvokeRequired) { Invoke((MethodInvoker)(() => SetStatus(msg))); return; }
            lbStatus.Text = msg;
            if (msg.Contains("🟢") || msg.Contains("✓")) lbStatus.ForeColor = Color.FromArgb(16, 185, 129);
            else if (msg.Contains("🔴") || msg.Contains("⚠️") || msg.Contains("Lỗi")) lbStatus.ForeColor = Color.FromArgb(239, 68, 68);
            else lbStatus.ForeColor = Color.FromArgb(245, 158, 11);
        }

        private static void UpdateDot(PictureBox p, bool ok)
        {
            p.BackColor = ok ? Color.FromArgb(60, 200, 80) : Color.FromArgb(180, 60, 60);
        }

        // ═══════════════════════════════════════════════════════════════
        // UTILITIES
        // ═══════════════════════════════════════════════════════════════

        private static string ParseIni(string content, string key)
        {
            // Regex match the line: key = value
            var m = Regex.Match(content, $@"{key}\s*=\s*(.+)");
            if (!m.Success) return "";

            string val = m.Groups[1].Value.Trim();

            // Nếu giá trị vẫn chứa "key =", chỉ lấy phần sau '='
            if (val.Contains("="))
            {
                var parts = val.Split('=');
                return parts[parts.Length - 1].Trim();
            }

            return val;
        }

        private static string ReplaceIni(string ini, string key, string oldVal, string newVal)
        {
            if (oldVal == newVal) return ini;
            return ini.Replace($"{key} = {oldVal}", $"{key} = {newVal}");
        }

        private void ShowWarn(string msg) =>
            MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void ShowErr(string msg)
        {
            if (InvokeRequired) { Invoke((MethodInvoker)(() => ShowErr(msg))); return; }
            MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ═══════════════════════════════════════════════════════════════
        // FORM CLOSE
        // ═══════════════════════════════════════════════════════════════

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (processMaster != null || processCaves != null)
            {
                var r = MessageBox.Show("Server đang chạy! Dừng server và thoát?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.No) { e.Cancel = true; return; }
                StopServer();
            }
            base.OnFormClosing(e);
        }

        // ─── About ────────────────────────────────────────────────────────
        private void btnInfo_Click(object sender, EventArgs e) => new About().Show();
    }
}
