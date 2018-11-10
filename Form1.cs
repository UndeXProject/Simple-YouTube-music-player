using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using WMPLib;
using System.Text.RegularExpressions;
using System.Reflection;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer player = new WindowsMediaPlayer();
        String filename;
        String url_video;
        public Form1()
        {
            InitializeComponent();

        }

        public void GetData(string url)
        {
            label5.Visible = true;
            button1.Enabled = false;

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create("http://18.222.157.10/api.php?act=getTitle&url=" + url);
            WebResponse response = wrGETURL.GetResponse();
            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                label3.Text = responseText;
            }


            wrGETURL = WebRequest.Create("http://18.222.157.10/api.php?act=getLink&url=" + url);
            response = wrGETURL.GetResponse();

            encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                player.URL = responseText;
                url_video = responseText;
            }

            wrGETURL = WebRequest.Create("http://18.222.157.10/api.php?act=getPic&url=" + url);
            response = wrGETURL.GetResponse();

            encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                pictureBox1.Load(responseText);
            }

            wrGETURL = WebRequest.Create("http://18.222.157.10/api.php?act=getFilename&url=" + url);
            response = wrGETURL.GetResponse();

            encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                string responseText = reader.ReadToEnd();
                filename = responseText.Replace(".mp4", ".mp3");
                saveFileDialog1.FileName = responseText.Replace(".mp4", ".mp3");
            }

            button2.Enabled = true;
            player.controls.stop();
            label5.Visible = false;
            button1.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button2.Focus();
            button5.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            player.controls.play();
            button3.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = false;
            button3.Focus();
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
                //GetData(Youtube_URL);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            button1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
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
                        new System.Uri(url_video),
                        // Param2 = Path to save
                        @file
                    );
                }
        }
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100) { progressBar1.Visible = false; button5.Enabled = true; }
        }
    }
}
