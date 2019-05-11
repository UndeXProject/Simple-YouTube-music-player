using System;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace updater
{
    public partial class main : Form
    {
        public string AppDir { internal get; set; }
        Thread check;
        public main()
        {
            InitializeComponent();
            GetRTFNotes();
        }

        public async void GetRTFNotes()
        {
            HttpClient client = new HttpClient();
            var getRtf = await client.GetStringAsync("https://raw.githubusercontent.com/UndeXProject/Simple-YouTube-music-player/master/UpdateNotes.rtf");
            richTextBoxUpdate.Rtf = getRtf;
        }

        private void main_Shown(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            Thread t = new Thread(new ParameterizedThreadStart(CheckProcess));
            t.Start(this);
            check = t;
        }

        private void delayToUpdate_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "update.pkg")))
            {
                this.ControlBox = false;
                check.Abort();
                Thread th = new Thread(new ParameterizedThreadStart(ThreadUnZip));
                th.Start(labelNote);
                System.Windows.Forms.Timer t = (System.Windows.Forms.Timer)sender;
                t.Enabled = false;
                progressBar.Visible = true;
            }
            else
            {
                System.Windows.Forms.Timer t = (System.Windows.Forms.Timer)sender;
                t.Enabled = false;
                MessageBox.Show("Файл обновления не найден. Обновление не возможно!", "Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void ThreadUnZip(object e)
        {
            bool errors = false;
            Label status = (Label)e;
            var updateFile = Path.Combine(Application.StartupPath, "update.pkg");
            var zip = ZipFile.OpenRead(updateFile);
            progressBar.Invoke((MethodInvoker)(() => progressBar.Maximum = zip.Entries.Count-1));
            foreach (var file in zip.Entries)
            {
                if (file.Name != "_update")
                {
                    try
                    {
                        file.ExtractToFile(Path.Combine(AppDir, file.Name), true);
                        status.Text = "Распаковка "+ file.Name;
                    }
                    catch(Exception s)
                    {
                        status.Text = "Ошибка распаковки " + file.Name;
                        errors = true;
                        //MessageBox.Show(s.Source+"\r\n"+s.Message,"Fatal error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    finally
                    {
                        progressBar.Invoke((MethodInvoker)(() => progressBar.Value++));
                    }
                }
            }
            if (errors)
            {
                MessageBox.Show("При установке обновления возникли ошибки. Повторите процесс обновления!", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                progressBar.Invoke((MethodInvoker)(() => UpdateComplite()));
            }
        }

        public void UpdateComplite()
        {
            this.ControlBox = true;
            labelNote.Text = "Установка обновлений завершена!";
        }

        private void CheckProcess(object e)
        {
            Form m = (Form)e;
            bool locked = false;
            while (true)
            {
                if (locked)
                {
                    progressBar.Invoke((MethodInvoker)(() => labelNote.Text = "Закройте Simple YouTube Music Player..."));
                    progressBar.Invoke((MethodInvoker)(() => delayToUpdate.Enabled = false));
                    //m.Enabled = false;
                }
                else
                {
                    progressBar.Invoke((MethodInvoker)(() => labelNote.Text = "Подготовка к обновлению..."));
                    progressBar.Invoke((MethodInvoker)(() => delayToUpdate.Enabled = true));
                    //m.Enabled = true;
                    //check.Abort();
                }
                foreach (var process in Process.GetProcesses())
                {
                    if(process.ProcessName == "Simple YouTube Music Player")
                    {
                        locked = true;
                        break;
                    }
                    else
                    {
                        locked = false;
                    }
                }
                Thread.Sleep(500);
            }
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            check.Abort();
        }
    }
}
