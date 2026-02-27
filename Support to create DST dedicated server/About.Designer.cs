using System.Drawing;
using System.Windows.Forms;

namespace Support_to_create_DST_dedicated_server
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            lblAppName = new Label();
            lblVersion = new Label();
            btnClose = new Button();
            panelCard = new Panel();
            btnSteam = new Button();
            btnFacebook = new Button();
            lblAuthor = new Label();
            lblDevBy = new Label();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // lblAppName
            // 
            lblAppName.Font = new Font("Segoe UI Variable Display", 14F, FontStyle.Bold);
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(20, 20);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(460, 60);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "Don't Starve Together\r\nDedicated Server Dashboard";
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVersion.ForeColor = Color.FromArgb(148, 163, 184);
            lblVersion.Location = new Point(215, 85);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(69, 15);
            lblVersion.TabIndex = 1;
            lblVersion.Text = "Version";
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(51, 65, 85);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(380, 285);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 35);
            btnClose.TabIndex = 2;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // panelCard
            // 
            panelCard.BackColor = Color.FromArgb(30, 41, 59);
            panelCard.Controls.Add(btnSteam);
            panelCard.Controls.Add(btnFacebook);
            panelCard.Controls.Add(lblAuthor);
            panelCard.Controls.Add(lblDevBy);
            panelCard.Location = new Point(40, 120);
            panelCard.Name = "panelCard";
            panelCard.Size = new Size(420, 140);
            panelCard.TabIndex = 3;
            // 
            // btnSteam
            // 
            btnSteam.BackColor = Color.FromArgb(15, 23, 42);
            btnSteam.Cursor = Cursors.Hand;
            btnSteam.FlatAppearance.BorderSize = 0;
            btnSteam.FlatStyle = FlatStyle.Flat;
            btnSteam.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSteam.ForeColor = Color.FromArgb(56, 189, 248);
            btnSteam.Location = new Point(220, 80);
            btnSteam.Name = "btnSteam";
            btnSteam.Size = new Size(120, 35);
            btnSteam.TabIndex = 3;
            btnSteam.Text = "STEAM";
            btnSteam.UseVisualStyleBackColor = false;
            btnSteam.Click += llbSteamKhoa_LinkClicked;
            // 
            // btnFacebook
            // 
            btnFacebook.BackColor = Color.FromArgb(15, 23, 42);
            btnFacebook.Cursor = Cursors.Hand;
            btnFacebook.FlatAppearance.BorderSize = 0;
            btnFacebook.FlatStyle = FlatStyle.Flat;
            btnFacebook.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFacebook.ForeColor = Color.FromArgb(59, 130, 246);
            btnFacebook.Location = new Point(80, 80);
            btnFacebook.Name = "btnFacebook";
            btnFacebook.Size = new Size(120, 35);
            btnFacebook.TabIndex = 2;
            btnFacebook.Text = "FACEBOOK";
            btnFacebook.UseVisualStyleBackColor = false;
            btnFacebook.Click += llbFacebookKhoa_LinkClicked;
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Segoe UI Variable Display", 18F, FontStyle.Bold);
            lblAuthor.ForeColor = Color.White;
            lblAuthor.Location = new Point(165, 30);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(84, 32);
            lblAuthor.TabIndex = 1;
            lblAuthor.Text = "Khoa";
            // 
            // lblDevBy
            // 
            lblDevBy.AutoSize = true;
            lblDevBy.Font = new Font("Segoe UI", 9F);
            lblDevBy.ForeColor = Color.FromArgb(148, 163, 184);
            lblDevBy.Location = new Point(167, 15);
            lblDevBy.Name = "lblDevBy";
            lblDevBy.Size = new Size(82, 15);
            lblDevBy.TabIndex = 0;
            lblDevBy.Text = "Developed by";
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(500, 340);
            Controls.Add(panelCard);
            Controls.Add(btnClose);
            Controls.Add(lblVersion);
            Controls.Add(lblAppName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About Dashboard";
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAppName;
        private Label lblVersion;
        private Button btnClose;
        private Panel panelCard;
        private Label lblAuthor;
        private Label lblDevBy;
        private Button btnSteam;
        private Button btnFacebook;
    }
}