
namespace Support_to_create_DST_dedicated_server
{
    partial class Home
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.lbStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbFolderCluster = new System.Windows.Forms.Label();
            this.lbFolderSteamApp = new System.Windows.Forms.Label();
            this.btnOpenCluster = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenFolderSteamApp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMaxPlayer = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.BackColor = System.Drawing.Color.Yellow;
            this.lbStatus.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbStatus.Location = new System.Drawing.Point(98, 278);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(18, 20);
            this.lbStatus.TabIndex = 24;
            this.lbStatus.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 278);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Status:";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(697, 265);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(116, 46);
            this.btnRun.TabIndex = 22;
            this.btnRun.Text = "Create";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtToken
            // 
            this.txtToken.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtToken.Location = new System.Drawing.Point(295, 201);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(518, 30);
            this.txtToken.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(25, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 26);
            this.label3.TabIndex = 20;
            this.label3.Text = "Enter Token:";
            // 
            // lbFolderCluster
            // 
            this.lbFolderCluster.AutoSize = true;
            this.lbFolderCluster.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lbFolderCluster.Location = new System.Drawing.Point(295, 142);
            this.lbFolderCluster.Name = "lbFolderCluster";
            this.lbFolderCluster.Size = new System.Drawing.Size(462, 16);
            this.lbFolderCluster.TabIndex = 19;
            this.lbFolderCluster.Text = "ex: C:\\Users\\Khoa\\Documents\\Klei\\DoNotStarveTogether\\440725477\\Cluster_1";
            // 
            // lbFolderSteamApp
            // 
            this.lbFolderSteamApp.AutoSize = true;
            this.lbFolderSteamApp.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lbFolderSteamApp.Location = new System.Drawing.Point(295, 59);
            this.lbFolderSteamApp.Name = "lbFolderSteamApp";
            this.lbFolderSteamApp.Size = new System.Drawing.Size(268, 16);
            this.lbFolderSteamApp.TabIndex = 18;
            this.lbFolderSteamApp.Text = "ex: C:\\Program Files (x86)\\Steam\\steamapps";
            // 
            // btnOpenCluster
            // 
            this.btnOpenCluster.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOpenCluster.Location = new System.Drawing.Point(295, 100);
            this.btnOpenCluster.Name = "btnOpenCluster";
            this.btnOpenCluster.Size = new System.Drawing.Size(518, 39);
            this.btnOpenCluster.TabIndex = 17;
            this.btnOpenCluster.Text = "Select folder save game";
            this.btnOpenCluster.UseVisualStyleBackColor = true;
            this.btnOpenCluster.Click += new System.EventHandler(this.btnOpenCluster_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(25, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 26);
            this.label2.TabIndex = 16;
            this.label2.Text = "Select folder \'Cluster_...\':";
            // 
            // btnOpenFolderSteamApp
            // 
            this.btnOpenFolderSteamApp.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOpenFolderSteamApp.Location = new System.Drawing.Point(295, 12);
            this.btnOpenFolderSteamApp.Name = "btnOpenFolderSteamApp";
            this.btnOpenFolderSteamApp.Size = new System.Drawing.Size(518, 45);
            this.btnOpenFolderSteamApp.TabIndex = 15;
            this.btnOpenFolderSteamApp.Text = "Select folder SteamApp";
            this.btnOpenFolderSteamApp.UseVisualStyleBackColor = true;
            this.btnOpenFolderSteamApp.Click += new System.EventHandler(this.btnOpenFolderSteamApp_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(25, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(257, 26);
            this.label5.TabIndex = 14;
            this.label5.Text = "Select folder \'steamapp\':";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaxPlayer);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(868, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 246);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server info";
            // 
            // txtMaxPlayer
            // 
            this.txtMaxPlayer.Location = new System.Drawing.Point(138, 147);
            this.txtMaxPlayer.Name = "txtMaxPlayer";
            this.txtMaxPlayer.Size = new System.Drawing.Size(214, 27);
            this.txtMaxPlayer.TabIndex = 8;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(138, 108);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(214, 27);
            this.txtPassword.TabIndex = 7;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(138, 68);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(214, 27);
            this.txtDescription.TabIndex = 6;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(138, 30);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(214, 27);
            this.txtServerName.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(258, 197);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Max player:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Password:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server name:";
            // 
            // btnInfo
            // 
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.Location = new System.Drawing.Point(1212, 292);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(31, 29);
            this.btnInfo.TabIndex = 26;
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(35, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(247, 17);
            this.label9.TabIndex = 27;
            this.label9.Text = "(folder contain game and dedicated server)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(35, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 17);
            this.label10.TabIndex = 28;
            this.label10.Text = "(folder contain save game)";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 340);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbFolderCluster);
            this.Controls.Add(this.lbFolderSteamApp);
            this.Controls.Add(this.btnOpenCluster);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenFolderSteamApp);
            this.Controls.Add(this.label5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Home";
            this.Text = "Support to create Don\'t Starve Together Dedicated Servers For Windows";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbFolderCluster;
        private System.Windows.Forms.Label lbFolderSteamApp;
        private System.Windows.Forms.Button btnOpenCluster;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenFolderSteamApp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMaxPlayer;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

