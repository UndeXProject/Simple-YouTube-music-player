using Simple_YouTube_Music_Player.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.Misc;

namespace Simple_YouTube_Music_Player.Classes
{
    public class Functions
    {
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string appName = Application.ProductName, appVer = Application.ProductVersion;
        public static string AppDataSoft = Path.Combine(AppData, appName);
        public static string verCore { get; set; }
        public static string verBass { get; set; }
        public static string JoinID { get; set; }
        public static int updateDownloadProgress { get; internal set; }

        public static List<string[]> playlist = new List<string[]>();
        public static int PlaylistPosition = 0;
        public static int SpectrumType = Properties.Settings.Default.SpectrumMode;
        public static int Volume = Properties.Settings.Default.volume;
        public static int PlayMode = Properties.Settings.Default.PlayMode;

        public static Color SpectrumColor1 = Properties.Settings.Default.SpectrumColor1;
        public static Color SpectrumColor2 = Properties.Settings.Default.SpectrumColor2;
        public static void OnCloseApp(EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("cache.tmp"))
            {
                process.Kill();
            }
            Bass.BASS_Stop();

            // Saving parameters
            Properties.Settings.Default.SpectrumColor1 = SpectrumColor1;
            Properties.Settings.Default.SpectrumColor2 = SpectrumColor2;
            Properties.Settings.Default.volume = Volume;
            Properties.Settings.Default.SpectrumMode = SpectrumType;
            Properties.Settings.Default.PlayMode = PlayMode;
            Properties.Settings.Default.Save();
            ///

            Application.Exit();
        }

        public static void Init()
        {
            if (verCore.Length > 15) { MessageBox.Show(verCore, "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            Core.CheckUpdates(UpdateAvalibe, appVer);
        }

        public static void UpdateAvalibe(string verApp)
        {
            if (!String.IsNullOrEmpty(verApp))
            {
                var message = MessageBox.Show(
                    "Текущая версия: " + appVer + "\r\nНовая версия: " + verApp + "\r\nОбновить сейчас?",
                    "Доступна новая версия!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                string[] ver = { verApp, appVer };
                if (message == DialogResult.Yes) { Core.UpdateSetup(Path.Combine(AppDataSoft, "update.pkg"), updateDownloadProgress, ver); } else { MessageBox.Show("На старых версиях работоспособность программы не гарантируется!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        public static string[] NextTrack()
        {
            var count = playlist.Count;
            string[] _result = new string[4];
            if (PlaylistPosition < (count-1))
            {
                PlaylistPosition++;
                _result = playlist[PlaylistPosition];
            }
            return _result;
        }

        public static string[] PrevTrack()
        {
            var count = playlist.Count;
            string[] _result = new string[4];
            if (PlaylistPosition < count)
            {
                PlaylistPosition--;
                _result = playlist[PlaylistPosition];
            }
            return _result;
        }

        public static string[] RandomTrack()
        {
            var count = playlist.Count;
            string[] _result = new string[4];
            Random rand = new Random();
            var i = rand.Next(0, count - 1);
            PlaylistPosition = i;
            _result = playlist[i];
            return _result;
        }

        public static string[] LoopTrack()
        {
            var _result = playlist[PlaylistPosition];
            return _result;
        }

        public static Bitmap SpectrumPaint(int stream, int width, int height)
        {
            Bitmap Spectrum = new Bitmap(1,1);
            Visuals SpectrumCreater = new Visuals();

            switch (SpectrumType)
            {
                case 0:
                    Spectrum = SpectrumCreater.CreateSpectrum(stream, width, height,
                                SpectrumColor1, SpectrumColor2, Color.Transparent,
                                  false, false, true); 
                break;
                case 1:
                    Spectrum = SpectrumCreater.CreateSpectrumDot(stream, width, height,
                                SpectrumColor1, SpectrumColor2, Color.Transparent,2,2,
                                  false, false, true);
                break;
                case 2:
                    Spectrum = SpectrumCreater.CreateSpectrumBean(stream, width, height,
                                SpectrumColor1, SpectrumColor2, Color.Transparent, 2,
                                  false, false, true);
                break;
                case 3:
                    Spectrum = SpectrumCreater.CreateSpectrumEllipse(stream, width, height,
                                SpectrumColor1, SpectrumColor2, Color.Transparent, 2, 2,
                                  false, false, true);
                break;
                case 4:
                    Spectrum = SpectrumCreater.CreateSpectrumLine(stream, width, height,
                                SpectrumColor1, SpectrumColor2, Color.Transparent, 2, 2,
                                  false, false, true);
                break;
                case 5:
                    Spectrum = SpectrumCreater.CreateSpectrumLinePeak(stream, width, height,
                                SpectrumColor1, SpectrumColor2, Color.DarkBlue, Color.Transparent, 3, 3, 2, 4,
                                  false, false, true);
                break;
                case 6:
                    Spectrum = SpectrumCreater.CreateSpectrumWave(stream, width, height,
                                SpectrumColor1, SpectrumColor2, Color.Transparent, 2,
                                  false, false, true);
                break;
            }
            return Spectrum;
        }

        public static void LoadTrack(DownloadProgressChangedEventHandler downloaderChange)
        {
            var data = playlist[PlaylistPosition];
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файл mp4|*.mp4|Все файлы|*.*";
            saveFileDialog.Title = "Сохранить "+data[0];
            saveFileDialog.FileName = data[3].Replace("-" + data[5], "");
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string file = saveFileDialog.FileName;

            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += downloaderChange;
                wc.DownloadFileAsync(
                    new System.Uri(data[1]),
                    @file
                );
            }
        }
    }
}
