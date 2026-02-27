using System.Drawing;
using System.Windows.Forms;

namespace Support_to_create_DST_dedicated_server
{
    partial class Home
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            panelTop = new Panel();
            lblTitle = new Label();
            btnInfo = new Button();
            panelConfig = new Panel();
            picGameStatus = new PictureBox();
            lblGame = new Label();
            lbFolderGame = new Label();
            btnOpenFolderGame = new Button();
            picClusterStatus = new PictureBox();
            lblCluster = new Label();
            lbFolderCluster = new Label();
            btnOpenCluster = new Button();
            lblToken = new Label();
            txtToken = new TextBox();
            grpServerInfo = new GroupBox();
            label1 = new Label();
            txtServerName = new TextBox();
            label6 = new Label();
            txtDescription = new TextBox();
            label7 = new Label();
            txtPassword = new TextBox();
            label8 = new Label();
            txtMaxPlayer = new TextBox();
            btnSave = new Button();
            btnLaunch = new Button();
            lbStatus = new Label();
            splitLogs = new SplitContainer();
            grpMaster = new GroupBox();
            txtLogMaster = new RichTextBox();
            panelConsoleMaster = new Panel();
            txtConsoleMaster = new TextBox();
            btnSendMaster = new Button();
            grpCaves = new GroupBox();
            txtLogCaves = new RichTextBox();
            panelConsoleCaves = new Panel();
            txtConsoleCaves = new TextBox();
            btnSendCaves = new Button();
            toolTip1 = new ToolTip(components);
            panelTop.SuspendLayout();
            panelConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGameStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picClusterStatus).BeginInit();
            grpServerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitLogs).BeginInit();
            splitLogs.Panel1.SuspendLayout();
            splitLogs.Panel2.SuspendLayout();
            splitLogs.SuspendLayout();
            grpMaster.SuspendLayout();
            panelConsoleMaster.SuspendLayout();
            grpCaves.SuspendLayout();
            panelConsoleCaves.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(15, 23, 42);
            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnInfo);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1100, 60);
            panelTop.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(248, 250, 252);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(20, 0, 0, 0);
            lblTitle.Size = new Size(1040, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🎮 DST Dedicated Server Dashboard";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnInfo
            // 
            btnInfo.BackColor = Color.Transparent;
            btnInfo.Cursor = Cursors.Hand;
            btnInfo.Dock = DockStyle.Right;
            btnInfo.FlatAppearance.BorderSize = 0;
            btnInfo.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 41, 59);
            btnInfo.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            btnInfo.FlatStyle = FlatStyle.Flat;
            btnInfo.Font = new Font("Segoe UI Semibold", 14F);
            btnInfo.ForeColor = Color.FromArgb(148, 163, 184);
            btnInfo.Location = new Point(1040, 0);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(60, 60);
            btnInfo.TabIndex = 1;
            btnInfo.Text = "ℹ";
            btnInfo.UseVisualStyleBackColor = false;
            btnInfo.Click += btnInfo_Click;
            // 
            // panelConfig
            // 
            panelConfig.BackColor = Color.FromArgb(30, 41, 59);
            panelConfig.Controls.Add(picGameStatus);
            panelConfig.Controls.Add(lblGame);
            panelConfig.Controls.Add(lbFolderGame);
            panelConfig.Controls.Add(btnOpenFolderGame);
            panelConfig.Controls.Add(picClusterStatus);
            panelConfig.Controls.Add(lblCluster);
            panelConfig.Controls.Add(lbFolderCluster);
            panelConfig.Controls.Add(btnOpenCluster);
            panelConfig.Controls.Add(lblToken);
            panelConfig.Controls.Add(txtToken);
            panelConfig.Controls.Add(grpServerInfo);
            panelConfig.Controls.Add(btnLaunch);
            panelConfig.Controls.Add(lbStatus);
            panelConfig.Dock = DockStyle.Top;
            panelConfig.Location = new Point(0, 60);
            panelConfig.Name = "panelConfig";
            panelConfig.Padding = new Padding(15);
            panelConfig.Size = new Size(1100, 290);
            panelConfig.TabIndex = 1;
            // 
            // picGameStatus
            // 
            picGameStatus.BackColor = Color.FromArgb(239, 68, 68);
            picGameStatus.Location = new Point(205, 29);
            picGameStatus.Name = "picGameStatus";
            picGameStatus.Size = new Size(10, 10);
            picGameStatus.TabIndex = 0;
            picGameStatus.TabStop = false;
            // 
            // lblGame
            // 
            lblGame.AutoSize = true;
            lblGame.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblGame.ForeColor = Color.FromArgb(148, 163, 184);
            lblGame.Location = new Point(225, 25);
            lblGame.Name = "lblGame";
            lblGame.Size = new Size(46, 15);
            lblGame.TabIndex = 1;
            lblGame.Text = "GAME";
            // 
            // lbFolderGame
            // 
            lbFolderGame.AutoEllipsis = true;
            lbFolderGame.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lbFolderGame.ForeColor = Color.FromArgb(203, 213, 225);
            lbFolderGame.Location = new Point(275, 25);
            lbFolderGame.Name = "lbFolderGame";
            lbFolderGame.Size = new Size(700, 16);
            lbFolderGame.TabIndex = 2;
            lbFolderGame.Text = "Chưa chọn thư mục game...";
            // 
            // btnOpenFolderGame
            // 
            btnOpenFolderGame.BackColor = Color.FromArgb(59, 130, 246);
            btnOpenFolderGame.Cursor = Cursors.Hand;
            btnOpenFolderGame.FlatAppearance.BorderSize = 0;
            btnOpenFolderGame.FlatStyle = FlatStyle.Flat;
            btnOpenFolderGame.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnOpenFolderGame.ForeColor = Color.White;
            btnOpenFolderGame.Location = new Point(20, 20);
            btnOpenFolderGame.Name = "btnOpenFolderGame";
            btnOpenFolderGame.Size = new Size(170, 32);
            btnOpenFolderGame.TabIndex = 3;
            btnOpenFolderGame.Text = "📂 BROWSE GAME";
            btnOpenFolderGame.UseVisualStyleBackColor = false;
            btnOpenFolderGame.Click += btnOpenFolderGame_Click;
            // 
            // picClusterStatus
            // 
            picClusterStatus.BackColor = Color.FromArgb(239, 68, 68);
            picClusterStatus.Location = new Point(205, 74);
            picClusterStatus.Name = "picClusterStatus";
            picClusterStatus.Size = new Size(10, 10);
            picClusterStatus.TabIndex = 4;
            picClusterStatus.TabStop = false;
            // 
            // lblCluster
            // 
            lblCluster.AutoSize = true;
            lblCluster.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblCluster.ForeColor = Color.FromArgb(148, 163, 184);
            lblCluster.Location = new Point(225, 70);
            lblCluster.Name = "lblCluster";
            lblCluster.Size = new Size(70, 15);
            lblCluster.TabIndex = 5;
            lblCluster.Text = "CLUSTER";
            // 
            // lbFolderCluster
            // 
            lbFolderCluster.AutoEllipsis = true;
            lbFolderCluster.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lbFolderCluster.ForeColor = Color.FromArgb(203, 213, 225);
            lbFolderCluster.Location = new Point(295, 70);
            lbFolderCluster.Name = "lbFolderCluster";
            lbFolderCluster.Size = new Size(680, 16);
            lbFolderCluster.TabIndex = 6;
            lbFolderCluster.Text = "Chưa chọn thư mục cluster...";
            // 
            // btnOpenCluster
            // 
            btnOpenCluster.BackColor = Color.FromArgb(59, 130, 246);
            btnOpenCluster.Cursor = Cursors.Hand;
            btnOpenCluster.FlatAppearance.BorderSize = 0;
            btnOpenCluster.FlatStyle = FlatStyle.Flat;
            btnOpenCluster.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnOpenCluster.ForeColor = Color.White;
            btnOpenCluster.Location = new Point(20, 65);
            btnOpenCluster.Name = "btnOpenCluster";
            btnOpenCluster.Size = new Size(170, 32);
            btnOpenCluster.TabIndex = 7;
            btnOpenCluster.Text = "📁 SELECT CLUSTER";
            btnOpenCluster.UseVisualStyleBackColor = false;
            btnOpenCluster.Click += btnOpenCluster_Click;
            // 
            // lblToken
            // 
            lblToken.AutoSize = true;
            lblToken.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblToken.ForeColor = Color.FromArgb(148, 163, 184);
            lblToken.Location = new Point(20, 115);
            lblToken.Name = "lblToken";
            lblToken.Size = new Size(112, 15);
            lblToken.TabIndex = 8;
            lblToken.Text = "SERVER TOKEN";
            // 
            // txtToken
            // 
            txtToken.BackColor = Color.FromArgb(15, 23, 42);
            txtToken.BorderStyle = BorderStyle.FixedSingle;
            txtToken.Font = new Font("Consolas", 10F);
            txtToken.ForeColor = Color.FromArgb(56, 189, 248);
            txtToken.Location = new Point(138, 112);
            txtToken.Name = "txtToken";
            txtToken.PlaceholderText = "pds-g^K... (Để trống nếu đã có cluster_token.txt)";
            txtToken.Size = new Size(937, 23);
            txtToken.TabIndex = 9;
            // 
            // grpServerInfo
            // 
            grpServerInfo.Controls.Add(label1);
            grpServerInfo.Controls.Add(txtServerName);
            grpServerInfo.Controls.Add(label6);
            grpServerInfo.Controls.Add(txtDescription);
            grpServerInfo.Controls.Add(label7);
            grpServerInfo.Controls.Add(txtPassword);
            grpServerInfo.Controls.Add(label8);
            grpServerInfo.Controls.Add(txtMaxPlayer);
            grpServerInfo.Controls.Add(btnSave);
            grpServerInfo.FlatStyle = FlatStyle.Flat;
            grpServerInfo.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            grpServerInfo.ForeColor = Color.FromArgb(148, 163, 184);
            grpServerInfo.Location = new Point(20, 150);
            grpServerInfo.Name = "grpServerInfo";
            grpServerInfo.Size = new Size(650, 95);
            grpServerInfo.TabIndex = 10;
            grpServerInfo.TabStop = false;
            grpServerInfo.Text = "SERVER CONFIGURATION";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8.5F);
            label1.Location = new Point(15, 30);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 0;
            label1.Text = "Tên:";
            // 
            // txtServerName
            // 
            txtServerName.BackColor = Color.FromArgb(15, 23, 42);
            txtServerName.BorderStyle = BorderStyle.FixedSingle;
            txtServerName.ForeColor = Color.White;
            txtServerName.Location = new Point(55, 27);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(200, 21);
            txtServerName.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8.5F);
            label6.Location = new Point(275, 30);
            label6.Name = "label6";
            label6.Size = new Size(41, 15);
            label6.TabIndex = 2;
            label6.Text = "Mô tả:";
            // 
            // txtDescription
            // 
            txtDescription.BackColor = Color.FromArgb(15, 23, 42);
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            txtDescription.ForeColor = Color.White;
            txtDescription.Location = new Point(325, 27);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 21);
            txtDescription.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8.5F);
            label7.Location = new Point(15, 62);
            label7.Name = "label7";
            label7.Size = new Size(60, 15);
            label7.TabIndex = 4;
            label7.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(15, 23, 42);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.ForeColor = Color.White;
            txtPassword.Location = new Point(85, 59);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(170, 21);
            txtPassword.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8.5F);
            label8.Location = new Point(275, 62);
            label8.Name = "label8";
            label8.Size = new Size(73, 15);
            label8.TabIndex = 6;
            label8.Text = "Max players:";
            // 
            // txtMaxPlayer
            // 
            txtMaxPlayer.BackColor = Color.FromArgb(15, 23, 42);
            txtMaxPlayer.BorderStyle = BorderStyle.FixedSingle;
            txtMaxPlayer.ForeColor = Color.White;
            txtMaxPlayer.Location = new Point(360, 59);
            txtMaxPlayer.Name = "txtMaxPlayer";
            txtMaxPlayer.Size = new Size(60, 21);
            txtMaxPlayer.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(51, 65, 85);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(540, 27);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(95, 55);
            btnSave.TabIndex = 8;
            btnSave.Text = "💾 SAVE";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnLaunch
            // 
            btnLaunch.BackColor = Color.FromArgb(16, 185, 129);
            btnLaunch.Cursor = Cursors.Hand;
            btnLaunch.FlatAppearance.BorderSize = 0;
            btnLaunch.FlatAppearance.MouseDownBackColor = Color.FromArgb(5, 150, 105);
            btnLaunch.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 211, 153);
            btnLaunch.FlatStyle = FlatStyle.Flat;
            btnLaunch.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            btnLaunch.ForeColor = Color.White;
            btnLaunch.Location = new Point(690, 158);
            btnLaunch.Name = "btnLaunch";
            btnLaunch.Size = new Size(385, 87);
            btnLaunch.TabIndex = 12;
            btnLaunch.Text = "🚀 LAUNCH SERVER";
            btnLaunch.UseVisualStyleBackColor = false;
            btnLaunch.Click += btnLaunch_Click;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lbStatus.ForeColor = Color.FromArgb(245, 158, 11);
            lbStatus.Location = new Point(20, 255);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(55, 19);
            lbStatus.TabIndex = 13;
            lbStatus.Text = "Ready...";
            // 
            // splitLogs
            // 
            splitLogs.BackColor = Color.FromArgb(15, 23, 42);
            splitLogs.Dock = DockStyle.Fill;
            splitLogs.Location = new Point(0, 350);
            splitLogs.Name = "splitLogs";
            // 
            // splitLogs.Panel1
            // 
            splitLogs.Panel1.BackColor = Color.FromArgb(15, 23, 42);
            splitLogs.Panel1.Controls.Add(grpMaster);
            splitLogs.Panel1.Padding = new Padding(10);
            // 
            // splitLogs.Panel2
            // 
            splitLogs.Panel2.BackColor = Color.FromArgb(15, 23, 42);
            splitLogs.Panel2.Controls.Add(grpCaves);
            splitLogs.Panel2.Padding = new Padding(10);
            splitLogs.Size = new Size(1100, 350);
            splitLogs.SplitterDistance = 540;
            splitLogs.SplitterWidth = 10;
            splitLogs.TabIndex = 0;
            // 
            // grpMaster
            // 
            grpMaster.Controls.Add(txtLogMaster);
            grpMaster.Controls.Add(panelConsoleMaster);
            grpMaster.Dock = DockStyle.Fill;
            grpMaster.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grpMaster.ForeColor = Color.FromArgb(56, 189, 248);
            grpMaster.Location = new Point(10, 10);
            grpMaster.Name = "grpMaster";
            grpMaster.Size = new Size(520, 330);
            grpMaster.TabIndex = 0;
            grpMaster.TabStop = false;
            grpMaster.Text = "🌍  MASTER";
            // 
            // txtLogMaster
            // 
            txtLogMaster.BackColor = Color.FromArgb(2, 6, 23);
            txtLogMaster.BorderStyle = BorderStyle.None;
            txtLogMaster.Dock = DockStyle.Fill;
            txtLogMaster.Font = new Font("Consolas", 9F);
            txtLogMaster.ForeColor = Color.FromArgb(148, 163, 184);
            txtLogMaster.Location = new Point(3, 19);
            txtLogMaster.Name = "txtLogMaster";
            txtLogMaster.ReadOnly = true;
            txtLogMaster.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtLogMaster.Size = new Size(514, 273);
            txtLogMaster.TabIndex = 0;
            txtLogMaster.Text = "";
            txtLogMaster.WordWrap = false;
            // 
            // panelConsoleMaster
            // 
            panelConsoleMaster.BackColor = Color.FromArgb(30, 41, 59);
            panelConsoleMaster.Controls.Add(txtConsoleMaster);
            panelConsoleMaster.Controls.Add(btnSendMaster);
            panelConsoleMaster.Dock = DockStyle.Bottom;
            panelConsoleMaster.Location = new Point(3, 292);
            panelConsoleMaster.Name = "panelConsoleMaster";
            panelConsoleMaster.Size = new Size(514, 35);
            panelConsoleMaster.TabIndex = 1;
            // 
            // txtConsoleMaster
            // 
            txtConsoleMaster.BackColor = Color.FromArgb(15, 23, 42);
            txtConsoleMaster.BorderStyle = BorderStyle.None;
            txtConsoleMaster.Font = new Font("Consolas", 10F);
            txtConsoleMaster.ForeColor = Color.White;
            txtConsoleMaster.Location = new Point(5, 10);
            txtConsoleMaster.Name = "txtConsoleMaster";
            txtConsoleMaster.PlaceholderText = "Execute Master command...";
            txtConsoleMaster.Size = new Size(420, 16);
            txtConsoleMaster.TabIndex = 0;
            txtConsoleMaster.KeyDown += txtConsoleMaster_KeyDown;
            // 
            // btnSendMaster
            // 
            btnSendMaster.BackColor = Color.FromArgb(51, 65, 85);
            btnSendMaster.Cursor = Cursors.Hand;
            btnSendMaster.Dock = DockStyle.Right;
            btnSendMaster.FlatAppearance.BorderSize = 0;
            btnSendMaster.FlatStyle = FlatStyle.Flat;
            btnSendMaster.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnSendMaster.ForeColor = Color.White;
            btnSendMaster.Location = new Point(434, 0);
            btnSendMaster.Name = "btnSendMaster";
            btnSendMaster.Size = new Size(80, 35);
            btnSendMaster.TabIndex = 1;
            btnSendMaster.Text = "SEND";
            btnSendMaster.UseVisualStyleBackColor = false;
            btnSendMaster.Click += btnSendMaster_Click;
            // 
            // grpCaves
            // 
            grpCaves.Controls.Add(txtLogCaves);
            grpCaves.Controls.Add(panelConsoleCaves);
            grpCaves.Dock = DockStyle.Fill;
            grpCaves.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            grpCaves.ForeColor = Color.FromArgb(192, 132, 252);
            grpCaves.Location = new Point(10, 10);
            grpCaves.Name = "grpCaves";
            grpCaves.Size = new Size(530, 330);
            grpCaves.TabIndex = 0;
            grpCaves.TabStop = false;
            grpCaves.Text = "🕳  CAVES";
            // 
            // txtLogCaves
            // 
            txtLogCaves.BackColor = Color.FromArgb(2, 6, 23);
            txtLogCaves.BorderStyle = BorderStyle.None;
            txtLogCaves.Dock = DockStyle.Fill;
            txtLogCaves.Font = new Font("Consolas", 9F);
            txtLogCaves.ForeColor = Color.FromArgb(148, 163, 184);
            txtLogCaves.Location = new Point(3, 19);
            txtLogCaves.Name = "txtLogCaves";
            txtLogCaves.ReadOnly = true;
            txtLogCaves.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtLogCaves.Size = new Size(524, 273);
            txtLogCaves.TabIndex = 0;
            txtLogCaves.Text = "";
            txtLogCaves.WordWrap = false;
            // 
            // panelConsoleCaves
            // 
            panelConsoleCaves.BackColor = Color.FromArgb(30, 41, 59);
            panelConsoleCaves.Controls.Add(txtConsoleCaves);
            panelConsoleCaves.Controls.Add(btnSendCaves);
            panelConsoleCaves.Dock = DockStyle.Bottom;
            panelConsoleCaves.Location = new Point(3, 292);
            panelConsoleCaves.Name = "panelConsoleCaves";
            panelConsoleCaves.Size = new Size(524, 35);
            panelConsoleCaves.TabIndex = 1;
            // 
            // txtConsoleCaves
            // 
            txtConsoleCaves.BackColor = Color.FromArgb(15, 23, 42);
            txtConsoleCaves.BorderStyle = BorderStyle.None;
            txtConsoleCaves.Font = new Font("Consolas", 10F);
            txtConsoleCaves.ForeColor = Color.White;
            txtConsoleCaves.Location = new Point(5, 10);
            txtConsoleCaves.Name = "txtConsoleCaves";
            txtConsoleCaves.PlaceholderText = "Execute Caves command...";
            txtConsoleCaves.Size = new Size(430, 16);
            txtConsoleCaves.TabIndex = 0;
            txtConsoleCaves.KeyDown += txtConsoleCaves_KeyDown;
            // 
            // btnSendCaves
            // 
            btnSendCaves.BackColor = Color.FromArgb(51, 65, 85);
            btnSendCaves.Cursor = Cursors.Hand;
            btnSendCaves.Dock = DockStyle.Right;
            btnSendCaves.FlatAppearance.BorderSize = 0;
            btnSendCaves.FlatStyle = FlatStyle.Flat;
            btnSendCaves.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnSendCaves.ForeColor = Color.White;
            btnSendCaves.Location = new Point(444, 0);
            btnSendCaves.Name = "btnSendCaves";
            btnSendCaves.Size = new Size(80, 35);
            btnSendCaves.TabIndex = 1;
            btnSendCaves.Text = "SEND";
            btnSendCaves.UseVisualStyleBackColor = false;
            btnSendCaves.Click += btnSendCaves_Click;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(1100, 700);
            Controls.Add(splitLogs);
            Controls.Add(panelConfig);
            Controls.Add(panelTop);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1000, 600);
            Name = "Home";
            Text = "DST Dedicated Server Dashboard";
            panelTop.ResumeLayout(false);
            panelConfig.ResumeLayout(false);
            panelConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picGameStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)picClusterStatus).EndInit();
            grpServerInfo.ResumeLayout(false);
            grpServerInfo.PerformLayout();
            splitLogs.Panel1.ResumeLayout(false);
            splitLogs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitLogs).EndInit();
            splitLogs.ResumeLayout(false);
            grpMaster.ResumeLayout(false);
            panelConsoleMaster.ResumeLayout(false);
            panelConsoleMaster.PerformLayout();
            grpCaves.ResumeLayout(false);
            panelConsoleCaves.ResumeLayout(false);
            panelConsoleCaves.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblTitle;
        private Button btnInfo;
        private Panel panelConfig;
        private PictureBox picGameStatus;
        private Label lblGame;
        private Label lbFolderGame;
        private Button btnOpenFolderGame;
        private PictureBox picClusterStatus;
        private Label lblCluster;
        private Label lbFolderCluster;
        private Button btnOpenCluster;
        private Label lblToken;
        private TextBox txtToken;
        private GroupBox grpServerInfo;
        private Label label1;
        private TextBox txtServerName;
        private Label label6;
        private TextBox txtDescription;
        private Label label7;
        private TextBox txtPassword;
        private Label label8;
        private TextBox txtMaxPlayer;
        private Button btnSave;
        private Button btnLaunch;
        private Label lbStatus;
        private SplitContainer splitLogs;
        private GroupBox grpMaster;
        private RichTextBox txtLogMaster;
        private Panel panelConsoleMaster;
        private TextBox txtConsoleMaster;
        private Button btnSendMaster;
        private GroupBox grpCaves;
        private RichTextBox txtLogCaves;
        private Panel panelConsoleCaves;
        private TextBox txtConsoleCaves;
        private Button btnSendCaves;
        private ToolTip toolTip1;
    }
}
