using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Support_to_create_DST_dedicated_server
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llbFacebookKhoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.facebook.com/100065415882613") { UseShellExecute = true });
        }

        private void llbSteamKhoa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://steamcommunity.com/profiles/76561198400991205") { UseShellExecute = true });
        }
    }
}
