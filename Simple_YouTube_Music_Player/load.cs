using System;
using System.IO;
using System.Windows.Forms;
using Simple_YouTube_Music_Player.Forms;
using Simple_YouTube_Music_Player.Classes;
using Un4seen.Bass;

namespace Simple_YouTube_Music_Player
{
    public partial class load : Form
    {
        private bool reverseFlag = false;
        private bool delayLogo = false;
        private bool checkVersion = false;
        private int delay = 100; // Задержка логотипа (10*100=1000мс)

        public bool work = true;

        public load()
        {
            TopMost = true;
            InitializeComponent();
            delayDebugText.Text = "Delay: " + delay.ToString();
        }

        private void opacityTimer_Tick(object sender, EventArgs e)
        {
            if (!delayLogo)
            {
                if (!reverseFlag && this.Opacity <= 1)
                {
                    if (this.Opacity == 1)
                    {
                        reverseFlag = !reverseFlag;
                        delayLogo = !delayLogo;
                    }
                    this.Opacity = this.Opacity + 0.01;
                }
                else if (reverseFlag && this.Opacity <= 1)
                {
                    this.Opacity = this.Opacity - 0.01;
                    if(this.Opacity == 0)
                    {
                        var t = (Timer)sender;
                        t.Enabled = false;
                        main Form = new main();
                        Form.Text = this.Text;
                        Form.StartPosition = FormStartPosition.CenterScreen;
                        TopMost = false;
                        this.Hide();
                        Form.Show();
                    }
                }
                opacityDebugText.Text = "Opacity: " + this.Opacity.ToString();
            }
            else
            {
                if (!checkVersion)
                {
                    var verCore = Core.Init(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName));
                    Functions.verCore = verCore;
                    checkVersion = !checkVersion;
                    BassNet.Registration("undex.project@gmail.com", "2X8141823152222");
                    byte[] byteBassVer = new byte[4];
                    Int32 ver = Bass.BASS_GetVersion();
                    byteBassVer[0] = BitConverter.GetBytes(ver)[0];
                    byteBassVer[1] = BitConverter.GetBytes(ver)[1];
                    byteBassVer[2] = BitConverter.GetBytes(ver)[2];
                    byteBassVer[3] = BitConverter.GetBytes(ver)[3];
                    Functions.verBass = byteBassVer[3].ToString()+"."+ byteBassVer[2].ToString() + "."+ byteBassVer[1].ToString() + "."+ byteBassVer[0].ToString();
                    string[] arg = Environment.GetCommandLineArgs();
                    if (arg.Length > 1)
                    {
                        if(arg[1] != "-noDiscord")
                            Discord.Init();
                    }
                }
                if (delay == 0) delayLogo = !delayLogo;
                if (delay > 0) delay--;
                delayDebugText.Text = "Delay: " + delay.ToString();
            }
        }
    }
}
