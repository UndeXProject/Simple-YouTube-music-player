using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using WMPLib;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Net;

namespace WindowsFormsApp2
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        WMPLib.WindowsMediaPlayer player = new WindowsMediaPlayer();
        string[] FileData = { "", "", "", "" };
        Boolean mute = true;
        Boolean load = false;
        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = Properties.Resources.noImg;
            pictureBox2.Image = Properties.Resources.save;
            player.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange);

            player.settings.volume = Properties.Settings.Default.volume;
            trackBar1.Value = Properties.Settings.Default.volume;

            var skinManager = MaterialSkin.MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
        }

        private string NormalizeURL(string text)
        {
            Regex regex = new Regex(@"list=(.+)");
            foreach (Match match in regex.Matches(text))
            {
                text = text.Replace("list=" + match.Groups[1].Value, "");
                //MessageBox.Show(match.Groups[1].Value);
            }
            return text;
        }

        private void GetDataFromUrl(string url)
        {
            url = NormalizeURL(url);
            //MessageBox.Show(url);
            
            if (!System.IO.File.Exists(Application.StartupPath + "\\cache.tmp"))
            {
                System.IO.File.WriteAllBytes(Application.StartupPath + "\\cache.tmp", Properties.Resources.youtube_dl);
                System.IO.File.SetAttributes(Application.StartupPath + "\\cache.tmp", FileAttributes.Hidden);
            }
            int i = 0;
            string[] arg = { "-e", "--get-thumbnail", "--get-filename", "-f bestaudio[ext = m4a]", "-g", url };
            File.Run(delegate (string s) { FileData[i] = s; i++; }, Console.In, Application.StartupPath + "\\cache.tmp", arg);
            System.IO.File.Delete(Application.StartupPath + "\\cache.tmp");
            SetData();
        }

        public void SetData()
        {
            pictureBox1.Load(FileData[2]);
            player.URL = FileData[1]; player.controls.stop();
            saveFileDialog1.FileName = FileData[3].Replace(".mp4",".mp3").Replace(".m4a",".mp3").Replace(".webm",".mp3");
            label3.Invoke((MethodInvoker)(() => label3.Text = FileData[0]));
            button1.Invoke((MethodInvoker)(() => button1.Enabled = true));
            label5.Invoke((MethodInvoker)(() => label5.Visible = false));
            button2.Invoke((MethodInvoker)(() => button2.Enabled = true));
            button5.Invoke((MethodInvoker)(() => button5.Visible = true));
            load = true;
            /*
             * label3.Text = FileData[1];
             * button1.Enabled = true;
             * label5.Visible = false;
             * button2.Enabled = true;
             * button5.Visible = true;
             */
        }

            private void button1_Click(object sender, EventArgs e)
            {
            string url = textBox1.Text;
            Thread mythread = new Thread(delegate () { GetDataFromUrl(url); });
            mythread.Start();
            pictureBox1.Image = Properties.Resources.loader;
            button1.Enabled = false;
            label5.Visible = true;

            player.controls.currentPosition = 0;
            player.controls.stop();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.controls.play();
            label4.Text = player.controls.currentPositionString;
            timer2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = false;
            button3.Focus();
            button6.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            player.controls.pause();
            button2.Enabled = true;
            button3.Enabled = false;
            button2.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            button3.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!textBox1.Focused)
            {
                String Youtube_URL = Clipboard.GetText();
                Regex regex = new Regex(@"youtu.be(\w*)");
                MatchCollection matches = regex.Matches(Youtube_URL);
                if (matches.Count > 0)
                {
                    textBox1.Text = Youtube_URL;
                }

                regex = new Regex(@"youtube.com\/watch(\w*)");
                matches = regex.Matches(Youtube_URL);
                if (matches.Count > 0)
                {
                    textBox1.Text = Youtube_URL;
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            button1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Файл mp3|*.mp3|Все файлы|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string file = saveFileDialog1.FileName;
            // сохраняем текст в файл
            button5.Enabled = false;

            progressBar1.Visible = true;
            //MessageBox.Show(@file);
            using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(
                        // Param1 = Link of file
                        new System.Uri(FileData[1]),
                        // Param2 = Path to save
                        @file
                    );
                }
        }
        void wc_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100) { progressBar1.Visible = false; button5.Enabled = true; }
        }

        void wplayer_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                player.controls.currentPosition = 0;
                player.controls.stop();
                button2.Enabled = true;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label4.Text = player.controls.currentPositionString;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (mute) {
                button6.Text = "Unmute";
                player.settings.volume = 0;
                mute = !mute;
                trackBar1.Enabled = false;
            }
            else
            {
                button6.Text = "Mute";
                player.settings.volume = trackBar1.Value;
                mute = !mute;
                trackBar1.Enabled = true;
            }
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            player.settings.volume = trackBar1.Value;
            Properties.Settings.Default.volume = trackBar1.Value;
            Properties.Settings.Default.Save();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //File.Delete(Application.StartupPath + "\\Newtonsoft.Json.dll");
            //File.Delete(Application.StartupPath + "\\Newtonsoft.Json.xml");
            //File.Delete(Application.StartupPath + "\\MaterialSkin.dll");
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            String ImgName = saveFileDialog1.FileName.Replace(".mp3",".jpeg");
            saveFileDialog1.FileName = ImgName;
            saveFileDialog1.Filter = "Файл jpeg|*.jpeg|Все файлы|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string file = saveFileDialog1.FileName;
            pictureBox1.Image.Save(file, ImageFormat.Jpeg);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (load)
            {
                pictureBox2.Visible = true;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //pictureBox2.Visible = false;
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }
    }
}
