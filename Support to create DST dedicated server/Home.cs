using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO.Compression;
using System.Net;

namespace Support_to_create_DST_dedicated_server
{
    public partial class Home : Form
    {
        String cluster_number = "";// 440725477\Cluster_1
        String path_cluster_full = "";// C:\Users\user\Documents\Klei\DoNotStarveTogether\440725477\Cluster_1
        String path_cluster = "";// 440725477\Cluster_1
        String folderSteamApp = "";
        String folderSteamAppDST = ""; // Thư mục steamapps chứa game DST (có thể khác ổ với Dedicated Server)
        List<string> allLibraryPaths = new List<string>(); // Danh sách tất cả các library steamapps tìm thấy
        String listMods = "";
        String hostNameA = "";
        String descriptionA = "";
        String passwordA = "";
        String maxPlayerA = "";
        public Home()
        {
            InitializeComponent();
            AutoDetectSteamApps();
        }

        private void AutoDetectSteamApps()
        {
            try
            {
                string steamPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null) as string;
                if (string.IsNullOrEmpty(steamPath)) return;

                steamPath = steamPath.Replace("/", "\\");
                List<string> libraryPaths = new List<string>();
                string mainSteamApps = Path.Combine(steamPath, "steamapps");
                libraryPaths.Add(mainSteamApps);

                string libraryFoldersVdf = Path.Combine(mainSteamApps, "libraryfolders.vdf");
                if (File.Exists(libraryFoldersVdf))
                {
                    string content = File.ReadAllText(libraryFoldersVdf);
                    var matches = Regex.Matches(content, @"\""path\""\s+\""(.+?)\""");
                    foreach (Match match in matches)
                    {
                        string path = match.Groups[1].Value.Replace("\\\\", "\\");
                        string appsPath = Path.Combine(path, "steamapps");
                        if (!libraryPaths.Contains(appsPath))
                            libraryPaths.Add(appsPath);
                    }
                }

                // Lưu lại toàn bộ list để tìm mod sau này
                allLibraryPaths = libraryPaths;

                // 1. Tìm thư mục chứa Dedicated Server (Ưu tiên số 1)
                foreach (string path in libraryPaths)
                {
                    if (Directory.Exists(Path.Combine(path, "common", "Don't Starve Together Dedicated Server")))
                    {
                        folderSteamApp = path;
                        lbFolderSteamApp.Text = path;
                        break;
                    }
                }

                // 2. Tìm thư mục chứa Game DST (Để phục vụ việc copy mod)
                foreach (string path in libraryPaths)
                {
                    if (Directory.Exists(Path.Combine(path, "common", "Don't Starve Together")))
                    {
                        folderSteamAppDST = path;
                        break;
                    }
                }
                
                // Nếu không tìm thấy folder game riêng, mặc định dùng chung với folderSteamApp
                if (string.IsNullOrEmpty(folderSteamAppDST)) folderSteamAppDST = folderSteamApp;
            }
            catch { }
        }

        private void btnOpenFolderSteamApp_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose folder SteamApp containing Dedicated Server";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath;
                
                // Kiểm tra xem có folder Dedicated Server bên trong không
                string serverPath = Path.Combine(sSelectedPath, "common", "Don't Starve Together Dedicated Server");

                if (!Directory.Exists(serverPath))
                {
                    string message = "The selected folder does not contain 'Don't Starve Together Dedicated Server':\n" +
                                   "Thư mục đã chọn không chứa 'Don't Starve Together Dedicated Server':\n\n" + 
                                   "\nPlease check your steamapps path again.\n" +
                                   "Vui lòng kiểm tra lại đường dẫn steamapps.";

                    MessageBox.Show(message, "Warning / Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                lbFolderSteamApp.Text = sSelectedPath;
                folderSteamApp = sSelectedPath;
                
                // Cập nhật lại folderSteamAppDST nếu người dùng chọn thủ công (giả định mod nằm cùng ổ nếu chọn tay)
                if (string.IsNullOrEmpty(folderSteamAppDST)) folderSteamAppDST = sSelectedPath;
            }
        }

        private void btnOpenCluster_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose Folder Cluster";
            // Set initial directory to Documents\Klei\DoNotStarveTogether
            string dstPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Klei", "DoNotStarveTogether");
            
            if (Directory.Exists(dstPath))
            {
                fbd.SelectedPath = dstPath;
            }
            else
            {
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string sSelectedPath = fbd.SelectedPath; // C:\Users\user\Documents\Klei\DoNotStarveTogether\440725477\Cluster_1
                string cluster = sSelectedPath.Split("DoNotStarveTogether\\").Last(); // 440725477\Cluster_1
                cluster_number = cluster;// save cluster folder
                lbFolderCluster.Text = cluster;
                path_cluster_full = sSelectedPath;
                path_cluster = cluster;

                //show server info
                string pathCluster_ini = path_cluster_full + "\\cluster.ini";
                if (System.IO.File.Exists(pathCluster_ini))
                {
                    string fileCluster_ini = System.IO.File.ReadAllText(pathCluster_ini);
                    string hostName = FindTextBetween(fileCluster_ini, "cluster_name = ", "offline_cluster");
                    txtServerName.Text = hostName;
                    hostNameA = hostName;
                    string description = FindTextBetween(fileCluster_ini, "cluster_description = ", "cluster_name");
                    txtDescription.Text = description;
                    descriptionA = description;
                    string password = FindTextBetween(fileCluster_ini, "cluster_password = ", "cluster_description");
                    txtPassword.Text = password;
                    passwordA = password;
                    string maxPlayer = FindTextBetween(fileCluster_ini, "max_players = ", "pvp");
                    txtMaxPlayer.Text = maxPlayer;
                    maxPlayerA = maxPlayer;
                }
                else
                {
                    MessageBox.Show("Cluster empty", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // Kiểm tra version trước khi chạy
            if (!CheckVersion())
            {
                // Nếu người dùng chọn No (không muốn tiếp tục) thì dừng lại
                return;
            }

            // 1. Xử lý cluster_token.txt
            string pathToken = Path.Combine(path_cluster_full, "cluster_token.txt");
            bool tokenFileExists = File.Exists(pathToken);
            bool isInputEmpty = string.IsNullOrWhiteSpace(txtToken.Text);

            if (isInputEmpty)
            {
                if (!tokenFileExists)
                {
                    string msgToken = "Token is missing! Please enter your token.\n" +
                                     "Thiếu Token! Vui lòng nhập token của bạn.";
                    MessageBox.Show(msgToken, "Missing Token / Thiếu Token", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Dừng lại không chạy tiếp
                }
                // Nếu file đã tồn tại và để trống input -> Không làm gì (giữ nguyên file cũ)
            }
            else
            {
                // Nếu có nhập token -> Ghi đè hoặc tạo mới
                lbStatus.Text = "Saving token...";
                File.WriteAllText(pathToken, txtToken.Text.Trim());
            }
            //open file Cluster_test\Master\modoverrides.lua
            lbStatus.Text = "Read file modoverrides";
            string pathModoverrride = path_cluster_full + "\\Master\\modoverrides.lua";
            string fileModOverride = System.IO.File.ReadAllText(pathModoverrride);

            // 2. Trích xuất danh sách ID Mod (Regex chính xác hơn)
            var matches = Regex.Matches(fileModOverride, @"\[['""]workshop-(?<id>\d+)['""]\]");
            var ids = matches.Cast<Match>().Select(m => m.Groups["id"].Value).Distinct().ToList();
            listMods = string.Join(";", ids);

            if (ids.Count == 0)
            {
                MessageBox.Show("No mods found in modoverrides.lua!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // 3. Copy & Download mods (Chạy đa luồng)
            Task.Run(() => {
                copyMod();

                // 2. Sau khi xong mới tạo file .bat
                this.Invoke((MethodInvoker)delegate {
                    lbStatus.Text = "Creating .bat file";
                    createBatFile();
                });
            });
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path_cluster_full))
            {
                MessageBox.Show("Please select Cluster folder first!\nVui lòng chọn thư mục Cluster trước!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lbStatus.Text = "Checking mods...";
            
            // 1. Đọc list mod từ cluster
            string pathModoverrride = Path.Combine(path_cluster_full, "Master", "modoverrides.lua");
            if (!File.Exists(pathModoverrride))
            {
                MessageBox.Show("modoverrides.lua not found in Master folder!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileModOverride = File.ReadAllText(pathModoverrride);
            var matches = Regex.Matches(fileModOverride, @"\[['""]workshop-(?<id>\d+)['""]\]");
            var ids = matches.Cast<Match>().Select(m => m.Groups["id"].Value).Distinct().ToList();
            listMods = string.Join(";", ids);

            if (ids.Count == 0)
            {
                MessageBox.Show("No workshop mods found in modoverrides.lua", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            
            lbStatus.Invoke((MethodInvoker)delegate { lbStatus.Text = $"Detected {ids.Count} mods. Starting process..."; });

            // 2. Chạy hàm copyMod (Chạy đa luồng ngầm)
            Task.Run(() => {
                copyMod();
                this.Invoke((MethodInvoker)delegate {
                    lbStatus.Text = "Mod check & update complete!";
                    MessageBox.Show("Mod check & update process finished.\nQuá trình kiểm tra và cập nhật mod hoàn tất.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            });
        }

        private bool CheckVersion()
        {
            try
            {
                // Nếu không có folder game DST thì bỏ qua theo yêu cầu
                string gameVersionPath = Path.Combine(folderSteamAppDST, "common", "Don't Starve Together", "version.txt");
                string serverVersionPath = Path.Combine(folderSteamApp, "common", "Don't Starve Together Dedicated Server", "version.txt");

                if (!File.Exists(gameVersionPath)) return true; // Không có folder game DST -> bỏ qua theo yêu cầu
                if (!File.Exists(serverVersionPath)) return true; // Không có server version -> bỏ qua

                string gameVerStr = File.ReadAllText(gameVersionPath).Trim();
                string serverVerStr = File.ReadAllText(serverVersionPath).Trim();

                // Lấy con số đầu tiên trong chuỗi (ví dụ: "586324") để so sánh
                long gameVer = 0, serverVer = 0;
                long.TryParse(Regex.Match(gameVerStr, @"\d+").Value, out gameVer);
                long.TryParse(Regex.Match(serverVerStr, @"\d+").Value, out serverVer);

                if (serverVer < gameVer && serverVer > 0 && gameVer > 0)
                {
                    string msg = $"Warning: Your Dedicated Server version ({serverVer}) is older than Game version ({gameVer}).\n" +
                                 $"Cảnh báo: Phiên bản Dedicated Server ({serverVer}) đang thấp hơn phiên bản Game ({gameVer}).\n\n" +
                                 "This may cause errors. Do you want to continue?\n" +
                                 "Điều này có thể gây lỗi không tìm thấy host. Hãy cập nhật DST Dedicated Server ở STEAM trước khi chạy. Bạn có muốn tiếp tục không?";

                    DialogResult result = MessageBox.Show(msg, "Version Mismatch / Lệch phiên bản", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return result == DialogResult.Yes;
                }
            }
            catch { }
            return true;
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        private void copyMod()
        {
            string folderModsDediDST = Path.Combine(folderSteamApp, "common", "Don't Starve Together Dedicated Server", "mods");
            string rootApp = AppDomain.CurrentDomain.BaseDirectory;
            string steamCmdDir = Path.Combine(rootApp, "SteamCMD");

            // 1. Chuẩn bị môi trường
            try
            {
                if (!Directory.Exists(folderModsDediDST)) Directory.CreateDirectory(folderModsDediDST);
                // Tạo sẵn folder download trong SteamCMD để ổn định
                string steamCmdContentDir = Path.Combine(steamCmdDir, "steamapps", "workshop", "content", "322330");
                if (!Directory.Exists(steamCmdContentDir)) Directory.CreateDirectory(steamCmdContentDir);
            }
            catch { }

            string[] split = listMods.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            int totalMods = split.Length;
            int processedCount = 0;
            List<string> modsToDownload = new List<string>();
            
            // Danh sách thư mục tìm kiếm (Bổ sung thêm chính thư mục Dedi nếu có mod cũ)
            List<string> searchPaths = new List<string>(allLibraryPaths);
            if (!searchPaths.Contains(folderSteamApp)) searchPaths.Add(folderSteamApp);

            // 2. ĐA LUỒNG: Quét và Copy mod cục bộ
            Parallel.ForEach(split, (modId) =>
            {
                modId = modId.Trim();
                string sourcePath = "";
                string targetPath = Path.Combine(folderModsDediDST, "workshop-" + modId);

                foreach (string libraryPath in searchPaths)
                {
                    // Kiểm tra ở folder game mods
                    string dstModPath = Path.Combine(libraryPath, "common", "Don't Starve Together", "mods", "workshop-" + modId);
                    if (Directory.Exists(dstModPath)) { sourcePath = dstModPath; break; }

                    // Kiểm tra ở folder workshop content
                    string workshopContentPath = Path.Combine(libraryPath, "workshop", "content", "322330", modId);
                    if (Directory.Exists(workshopContentPath)) { sourcePath = workshopContentPath; break; }
                }

                if (!string.IsNullOrEmpty(sourcePath))
                {
                    try
                    {
                        if (Directory.Exists(targetPath))
                        {
                            if (GetLastWriteTimeRecursive(sourcePath) <= GetLastWriteTimeRecursive(targetPath)) 
                            {
                                System.Threading.Interlocked.Increment(ref processedCount);
                                return; 
                            }
                        }
                        Copy(sourcePath, targetPath);
                        System.Threading.Interlocked.Increment(ref processedCount);
                        this.Invoke((MethodInvoker)delegate { lbStatus.Text = $"[{processedCount}/{totalMods}] Copied {modId}"; });
                    }
                    catch { }
                }
                else
                {
                    lock (modsToDownload) { modsToDownload.Add(modId); }
                }
            });

            // 3. TẢI NGAY (Batch Mode): Tải toàn bộ mod thiếu và copy ngay
            if (modsToDownload.Count > 0)
            {
                this.Invoke((MethodInvoker)delegate { lbStatus.Text = $"Downloading {modsToDownload.Count} missing mods..."; });
                if (DownloadModsBatchWithSteamCMD(modsToDownload))
                {
                    foreach (var modId in modsToDownload)
                    {
                        string downloadedPath = Path.Combine(steamCmdDir, "steamapps", "workshop", "content", "322330", modId);
                        string targetPath = Path.Combine(folderModsDediDST, "workshop-" + modId);
                        if (Directory.Exists(downloadedPath))
                        {
                            try 
                            { 
                                Copy(downloadedPath, targetPath); 
                                System.Threading.Interlocked.Increment(ref processedCount);
                                this.Invoke((MethodInvoker)delegate { lbStatus.Text = $"[{processedCount}/{totalMods}] Downloaded {modId}"; });
                            } catch { }
                        }
                    }
                }
            } 

            this.Invoke((MethodInvoker)delegate { lbStatus.Text = "Mods logic bypassed. Server will handle via setup file."; });
            UpdateDedicatedServerModsSetup(split);
        }

        private bool DownloadModsBatchWithSteamCMD(List<string> modIds)
        {
            try
            {
                string rootApp = AppDomain.CurrentDomain.BaseDirectory;
                string steamCmdDir = Path.Combine(rootApp, "SteamCMD");
                string steamCmdExe = Path.Combine(steamCmdDir, "steamcmd.exe");

                if (!File.Exists(steamCmdExe))
                {
                    this.Invoke((MethodInvoker)delegate { lbStatus.Text = "Installing SteamCMD tool..."; });
                    if (!Directory.Exists(steamCmdDir)) Directory.CreateDirectory(steamCmdDir);
                    string zipPath = Path.Combine(steamCmdDir, "steamcmd.zip");
                    using (WebClient wc = new WebClient()) { wc.DownloadFile("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip", zipPath); }
                    ZipFile.ExtractToDirectory(zipPath, steamCmdDir);
                    File.Delete(zipPath);
                }

                // Lệnh Batch tải mod (Ép tải vào thư mục SteamCMD để dễ quản lý)
                StringBuilder args = new StringBuilder();
                args.Append("+login anonymous ");
                foreach (var id in modIds)
                {
                    args.Append($"+workshop_download_item 322330 {id} ");
                }
                args.Append("+quit");

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = steamCmdExe,
                    Arguments = args.ToString(),
                    WorkingDirectory = steamCmdDir, // Quan trọng: Chạy ngay tại folder SteamCMD
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                    return true; // Trả về true để tiến hành copy các mod đã tải được
                }
            }
            catch { return false; }
        }

        private void UpdateDedicatedServerModsSetup(string[] modIds)
        {
            try
            {
                string folderModsDediDST = Path.Combine(folderSteamApp, "common", "Don't Starve Together Dedicated Server", "mods");
                if (!Directory.Exists(folderModsDediDST)) return;

                string setupFilePath = Path.Combine(folderModsDediDST, "dedicated_server_mods_setup.lua");
                
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("-- Auto-generated by Support Tool by Khoa.ga");
                sb.AppendLine("-- IMPORTANT: We ONLY add mods if they are NOT in the local mods folder.");
                sb.AppendLine("-- This prevents the Dedicated Server from deleting your copied/downloaded mods.");
                
                foreach (string id in modIds)
                {
                    if (string.IsNullOrWhiteSpace(id)) continue;

                    // Nếu mod đã tồn tại trong folder mods -> KHÔNG ghi vào file setup
                    // Server sẽ load nó như một "Local mod", tránh bị xóa sạch folder.
                    string modPath = Path.Combine(folderModsDediDST, "workshop-" + id);
                    if (!Directory.Exists(modPath))
                    {
                        sb.AppendLine($"ServerModSetup(\"{id}\")");
                    }
                }
                
                File.WriteAllText(setupFilePath, sb.ToString());
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate { lbStatus.Text = "Error updating setup file"; });
            }
        }

        private void ExportModLinksToDesktop()
        {
            try
            {
                string[] split = listMods.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 0) return;

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "Link_Mod_DST.txt");

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < split.Length; i++)
                {
                    sb.AppendLine($"{i + 1}. https://steamcommunity.com/sharedfiles/filedetails/?id={split[i].Trim()}");
                }

                File.WriteAllText(filePath, sb.ToString());
                MessageBox.Show("Exported Link_Mod_DST.txt to Desktop!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch { }
        }
        private DateTime GetLastWriteTimeRecursive(string path)
        {
            DateTime lastWriteTime = Directory.GetLastWriteTime(path);
            try
            {
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    DateTime fileWriteTime = File.GetLastWriteTime(file);
                    if (fileWriteTime > lastWriteTime)
                        lastWriteTime = fileWriteTime;
                }
            }
            catch { }
            return lastWriteTime;
        }

        private void CreateShortcut(string name)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutAddress = Path.Combine(desktopPath, name + ".lnk");

                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                
                dynamic shortcut = shell.CreateShortcut(shortcutAddress);
                string targetBat = Path.Combine(folderSteamApp, "common", "Don't Starve Together Dedicated Server", "bin64", name + ".bat");
                shortcut.TargetPath = targetBat;
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetBat); // Quan trọng để file bat tìm thấy file exe
                shortcut.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not create shortcut: " + ex.Message);
            }
        }
        public string FindTextBetween(string text, string left, string right)
        {
            // TODO: Validate input arguments
            int beginIndex = text.IndexOf(left); // find occurence of left delimiter
            if (beginIndex == -1)
                return string.Empty; // or throw exception?
            beginIndex += left.Length;
            int endIndex = text.IndexOf(right, beginIndex); // find occurence of right delimiter
            if (endIndex == -1)
                return string.Empty; // or throw exception?
            return text.Substring(beginIndex, endIndex - beginIndex).Trim();
        }
        private void createBatFile()
        {
            string bin64Path = Path.Combine(folderSteamApp, "common", "Don't Starve Together Dedicated Server", "bin64");
            if (!Directory.Exists(bin64Path))
            {
                try { Directory.CreateDirectory(bin64Path); } catch { }
            }

            string serverName = txtServerName.Text.Trim();
            if (string.IsNullOrEmpty(serverName)) serverName = "Khoa.ga's World";

            string fileName = "MyDedicatedServer";
            string batPath = Path.Combine(bin64Path, fileName + ".bat");

            // Format cluster path for BAT file (giữ nguyên gạch chéo ngược như snippet của bạn)
            string localPathCluster = path_cluster;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("chcp 65001");
            sb.AppendLine("IF \"%JUSTTERMINATE%\" NEQ \"OKAY\" SET \"JUSTTERMINATE=OKAY\" & CALL %0 %* <NUL & SET \"JUSTTERMINATE=\" & GOTO :eof");
            sb.AppendLine("");
            sb.AppendLine(":: Chế độ Launcher: Tự động bật 2 cửa sổ Master và Caves");
            sb.AppendLine("if \"%1\"==\"master\" goto master_shard");
            sb.AppendLine("if \"%1\"==\"caves\" goto caves_shard");
            sb.AppendLine("");
            sb.AppendLine($"start \"Launcher_{serverName}\" \"%~f0\" master");
            sb.AppendLine($"start \"Launcher_{serverName}\" \"%~f0\" caves");
            sb.AppendLine("exit");
            sb.AppendLine("");
            sb.AppendLine(":master_shard");
            sb.AppendLine($"Title MT_Lock_Master_{serverName}");
            sb.AppendLine(":loop_master");
            sb.AppendLine($"start \"Server_Master_{serverName}\" /D \"%~dp0\" dontstarve_dedicated_server_nullrenderer_x64.exe -cluster \"{localPathCluster}\" -shard Master");
            sb.AppendLine(":: Sau 2 phut (120 giay) thi cua so MT_Lock nay tu dong dong lai");
            sb.AppendLine("choice /t 120 /d y > nul");
            sb.AppendLine("exit");
            sb.AppendLine("");
            sb.AppendLine(":caves_shard");
            sb.AppendLine($"Title MT_Lock_Caves_{serverName}");
            sb.AppendLine(":loop_caves");
            sb.AppendLine($"start \"Server_Caves_{serverName}\" /D \"%~dp0\" dontstarve_dedicated_server_nullrenderer_x64.exe -cluster \"{localPathCluster}\" -shard Caves");
            sb.AppendLine(":: Sau 2 phut (120 giay) thi cua so MT_Lock nay tu dong dong lai");
            sb.AppendLine("choice /t 120 /d y > nul");
            sb.AppendLine("exit");

            File.WriteAllText(batPath, sb.ToString(), new UTF8Encoding(false));
            CreateShortcut(fileName);

            this.Invoke((MethodInvoker)delegate { 
                lbStatus.Text = "Done!!! BAT file created."; 
                MessageBox.Show("MyDedicatedServer.bat has been created using your requested structure!\n" +
                                "File khởi chạy đã được tạo!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string pathCluster_ini = System.IO.File.ReadAllText(path_cluster_full + "\\cluster.ini");

            pathCluster_ini = pathCluster_ini.Replace("cluster_name = " + hostNameA, "cluster_name = " + txtServerName.Text);
            System.IO.File.WriteAllText(path_cluster_full + "\\cluster.ini", pathCluster_ini);

            pathCluster_ini = pathCluster_ini.Replace("cluster_description = " + descriptionA, "cluster_description = " + txtDescription.Text);
            System.IO.File.WriteAllText(path_cluster_full + "\\cluster.ini", pathCluster_ini);

            pathCluster_ini = pathCluster_ini.Replace("cluster_password = " + passwordA, "cluster_password = " + txtPassword.Text);
            System.IO.File.WriteAllText(path_cluster_full + "\\cluster.ini", pathCluster_ini);

            pathCluster_ini = pathCluster_ini.Replace("max_players = " + maxPlayerA, "max_players = " + txtMaxPlayer.Text);
            System.IO.File.WriteAllText(path_cluster_full + "\\cluster.ini", pathCluster_ini);

            lbStatus.Text = "Saved!!!";
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}
