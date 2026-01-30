
namespace Support_to_create_DST_dedicated_server
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            groupBox1 = new System.Windows.Forms.GroupBox();
            llbSteamKhoa = new System.Windows.Forms.LinkLabel();
            llbFacebookKhoa = new System.Windows.Forms.LinkLabel();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            btnClose = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(llbSteamKhoa);
            groupBox1.Controls.Add(llbFacebookKhoa);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new System.Drawing.Point(64, 89);
            groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            groupBox1.Size = new System.Drawing.Size(545, 168);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Contributors";
            // 
            // llbSteamKhoa
            // 
            llbSteamKhoa.AutoSize = true;
            llbSteamKhoa.Location = new System.Drawing.Point(135, 32);
            llbSteamKhoa.Name = "llbSteamKhoa";
            llbSteamKhoa.Size = new System.Drawing.Size(40, 15);
            llbSteamKhoa.TabIndex = 2;
            llbSteamKhoa.TabStop = true;
            llbSteamKhoa.Text = "Steam";
            llbSteamKhoa.LinkClicked += llbSteamKhoa_LinkClicked;
            // 
            // llbFacebookKhoa
            // 
            llbFacebookKhoa.AutoSize = true;
            llbFacebookKhoa.Location = new System.Drawing.Point(66, 32);
            llbFacebookKhoa.Name = "llbFacebookKhoa";
            llbFacebookKhoa.Size = new System.Drawing.Size(58, 15);
            llbFacebookKhoa.TabIndex = 1;
            llbFacebookKhoa.TabStop = true;
            llbFacebookKhoa.Text = "Facebook";
            llbFacebookKhoa.LinkClicked += llbFacebookKhoa_LinkClicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold);
            label3.Location = new System.Drawing.Point(16, 32);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(43, 17);
            label3.TabIndex = 0;
            label3.Text = "Khoa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(294, 55);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(73, 15);
            label2.TabIndex = 6;
            label2.Text = "Version: 1.2";
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(588, 297);
            btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(82, 22);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(64, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(526, 18);
            label1.TabIndex = 4;
            label1.Text = "Support to create Don't Starve Together Dedicated Servers For Windows";
            // 
            // About
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(700, 338);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(btnClose);
            Controls.Add(label1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "About";
            Text = "About";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llbSteamKhoa;
        private System.Windows.Forms.LinkLabel llbFacebookKhoa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}