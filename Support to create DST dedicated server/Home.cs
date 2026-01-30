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

namespace Support_to_create_DST_dedicated_server
{
    public partial class Home : Form
    {
        String cluster_number = "";// 440725477\Cluster_1
        String path_cluster_full = "";// C:\Users\user\Documents\Klei\DoNotStarveTogether\440725477\Cluster_1
        String path_cluster = "";// 440725477\Cluster_1
        String folderSteamApp = "";
        String folderSteamAppDST = ""; // Thư mục steamapps chứa game DST (có thể khác ổ với Dedicated Server)
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

            //get list mod //["workshop-374550642;workshop-374550642"]
            string wk = "[\"workshop-";
            var output = String.Join(";", Regex.Matches(fileModOverride, @"\"+wk+"(.+?)\"]")
                                    .Cast<Match>()
                                    .Select(m => m.Groups[1].Value));
            listMods = output;

            //copy mod
            lbStatus.Text = "Copying mod";
            copyMod();

            //create .bat file
            lbStatus.Text = "Creating .bat file";
            createBatFile();
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
            string wk = "[\"workshop-";
            var output = String.Join(";", Regex.Matches(fileModOverride, @"\" + wk + "(.+?)\"]")
                                    .Cast<Match>()
                                    .Select(m => m.Groups[1].Value));
            listMods = output;

            if (string.IsNullOrEmpty(listMods))
            {
                MessageBox.Show("No workshop mods found in modoverrides.lua", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Chạy hàm copyMod (hàm này đã có logic so sánh LastWriteTime)
            copyMod();
            
            lbStatus.Text = "Mod check & update complete!";
            MessageBox.Show("Mod check & update process finished.\nQuá trình kiểm tra và cập nhật mod hoàn tất.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // folderSteamApp bây giờ là nơi chứa Dedicated Server
            // folderSteamAppDST là nơi chứa game DST (có thể ở ổ đĩa khác)
            string folderModsDST = Path.Combine(folderSteamAppDST, "common", "Don't Starve Together", "mods");
            string folderModWorkshop = Path.Combine(folderSteamAppDST, "workshop", "content", "322330");
            string folderModsDediDST = Path.Combine(folderSteamApp, "common", "Don't Starve Together Dedicated Server", "mods");

            if (!Directory.Exists(folderModsDediDST))
            {
                try { Directory.CreateDirectory(folderModsDediDST); } catch { }
            }

            string[] split = listMods.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in split)
            {
                string modId = s.Trim();
                if (string.IsNullOrEmpty(modId)) continue;

                string sourcePath = "";
                string targetPath = Path.Combine(folderModsDediDST, "workshop-" + modId);

                // 1. Tìm trong thư mục Workshop Content (Ưu tiên vì đây là nơi Steam tải mặc định)
                string workshopPath = Path.Combine(folderModWorkshop, modId);
                if (Directory.Exists(workshopPath))
                {
                    sourcePath = workshopPath;
                }
                // 2. Tìm trong thư mục mods của game DST (nếu có cài game)
                else
                {
                    string dstModPath = Path.Combine(folderModsDST, "workshop-" + modId);
                    if (Directory.Exists(dstModPath))
                    {
                        sourcePath = dstModPath;
                    }
                }

                if (!string.IsNullOrEmpty(sourcePath))
                {
                    try
                    {
                        // Kiểm tra xem có bản cập nhật mới không
                        if (Directory.Exists(targetPath))
                        {
                            DateTime sourceTime = GetLastWriteTimeRecursive(sourcePath);
                            DateTime targetTime = GetLastWriteTimeRecursive(targetPath);

                            if (sourceTime > targetTime)
                            {
                                lbStatus.Text = $"Updating mod {modId}...";
                            }
                        }

                        Copy(sourcePath, targetPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error copying mod {modId}: {ex.Message}", "Error / Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Nếu không tìm thấy ở đâu cả
                    string msg = $"Mod workshop-{modId} was not found on your computer.\n" +
                                 $"Mod workshop-{modId} không tìm thấy trên máy tính.\n\n" +
                                 "Do you want to open Steam Workshop to download it?\n" +
                                 "Bạn có muốn mở Steam Workshop để tải mod này không?";
                    
                    DialogResult result = MessageBox.Show(msg, "Mod Missing / Thiếu Mod", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string url = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + modId;
                        try
                        {
                            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
                        }
                        catch { }
                    }
                }
            }
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
            string nameBat = "MyDedicatedServer";
            string bin64Path = Path.Combine(folderSteamApp, "common", "Don't Starve Together Dedicated Server", "bin64");
            string batPath = Path.Combine(bin64Path, nameBat + ".bat");

            // Đảm bảo thư mục bin64 tồn tại (tránh lỗi nếu user chưa bao giờ chạy server)
            if (!Directory.Exists(bin64Path))
            {
                try { Directory.CreateDirectory(bin64Path); } catch { }
            }

            if (!System.IO.File.Exists(batPath))
            {
                System.IO.File.Create(batPath).Dispose();
                TextWriter tw = new StreamWriter(batPath);
                tw.WriteLine("cd /D \"%~dp0\"\nstart \"DST Server Master\" dontstarve_dedicated_server_nullrenderer_x64.exe -cluster " + path_cluster + " -shard Master \nstart \"DST Server Caves\" dontstarve_dedicated_server_nullrenderer_x64.exe -cluster " + path_cluster + " -shard Caves");
                tw.Close();
                CreateShortcut(nameBat);
                lbStatus.Text = "Done!!! Check " + nameBat + ".bat shortcut in your desktop";
            }
            else if (System.IO.File.Exists(batPath))
            {
                System.IO.File.Delete(batPath);
                using (var tw = new StreamWriter(batPath, true))
                {
                    tw.WriteLine("cd /D \"%~dp0\"\nstart \"DST Server Master\" dontstarve_dedicated_server_nullrenderer_x64.exe -cluster " + path_cluster + " -shard Master \nstart \"DST Server Caves\" dontstarve_dedicated_server_nullrenderer_x64.exe -cluster " + path_cluster + " -shard Caves");
                    CreateShortcut(nameBat);
                    lbStatus.Text = "Done!!! Check " + nameBat + ".bat shortcut in your Desktop";
                }
            }
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
