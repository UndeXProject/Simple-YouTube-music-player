using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;
using DiscordRPC;
using Simple_YouTube_Music_Player.Classes;
using System.Net;
using System.Threading;

namespace Simple_YouTube_Music_Player.Forms
{
    public partial class settings : MetroForm
    {
        public int menu = 0;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public settings()
        {
            InitializeComponent();
        }

        private void Settings_Shown(object sender, EventArgs e)
        {
            /*var url = DiscordUserInfo.User.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x128);
            Bitmap bmp = new Bitmap((new WebClient()).OpenRead(url));
            var Resize = new Bitmap(bmp, DiscordAvatar.Width, DiscordAvatar.Height);
            DiscordAvatar.Image = Resize;
            DiscordLogin.Text = DiscordUserInfo.User.Username;
            DiscordAvatar.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, metroPanel8.Width, metroPanel8.Height, 20, 20));

            DiscordEnabled.Checked = Properties.Settings.Default.DiscordEnable;
            */
            int pos = 3;

            var c = tablessTabControl1.TabCount;
            for(int i = 0; i < c; i++)
            {
                Panel p = new Panel();
                Control tab = tablessTabControl1.GetControl(i);
                p.Location = new Point(3, pos);
                p.Size = new Size(174, 40);

                MetroLabel t = new MetroLabel();
                if(menu == i)
                {
                    t.ForeColor = Color.White;
                    t.BackColor = Color.FromArgb(0, 174, 219);
                }
                t.Name = "LAB_"+i.ToString();
                t.Text = tab.Text;
                t.TextAlign = ContentAlignment.MiddleCenter;
                t.AutoSize = false;
                t.Dock = DockStyle.Fill;
                t.Location = new Point(0,0);
                t.CustomBackground = true;
                t.CustomForeColor = true;

                t.MouseEnter += Label_MouseEnter;
                t.MouseLeave += Label_MouseLeave;
                t.MouseClick += Label_MouseClick;

                p.Controls.Add(t);
                metroPanel1.Controls.Add(p);
                pos += 42;
            }
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            MetroLabel self = (MetroLabel)sender;
            var ID = Convert.ToInt32(self.Name.Split('_')[1]);
            if (menu != ID)
                self.BackColor = SystemColors.Control;
        }
        private void Label_MouseLeave(object sender, EventArgs e)
        {
            MetroLabel self = (MetroLabel)sender;
            var ID = Convert.ToInt32(self.Name.Split('_')[1]);
            if (menu != ID)
                self.BackColor = SystemColors.Window;
        }
        private void Label_MouseClick(object sender, MouseEventArgs e)
        {
            MetroLabel self = (MetroLabel)sender;
            //MessageBox.Show(self.Name);
            if (e.Button == MouseButtons.Left)
            {
                var ID = Convert.ToInt32(self.Name.Split('_')[1]);
                tablessTabControl1.SelectTab(ID);
                
                self.ForeColor = Color.White;
                self.BackColor = Color.FromArgb(0, 174, 219);

                Control ctn = this.Controls.Find("LAB_" + menu,true)[0];
                ctn.ForeColor = Color.Black;
                ctn.BackColor = Color.White;
                menu = ID;
            }
        }

        private void DiscordEnabled_MouseEnter(object sender, EventArgs e)
        {
            ToolTip.SetToolTip(DiscordEnabled, "Включить/выключить интеграцию с Discord");
        }

        private void DiscordEnabled_CheckedChanged(object sender, EventArgs e)
        {
            MetroToggle self = (MetroToggle)sender;
            Properties.Settings.Default.DiscordEnable = self.Checked;
            if (self.Checked != true)
            {
                Discord.DisableClient();
            }
            else
            {
                Discord.client = new DiscordRpcClient(Discord.DisctordAppID);
                Discord.Init(Discord.client);
            }
            Properties.Settings.Default.Save();
        }
    }
}
