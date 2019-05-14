using System.Windows.Forms;
using System.Drawing;
using MetroFramework.Forms;
using Simple_YouTube_Music_Player.Classes;
using System;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using System.Collections.Generic;
using MetroFramework.Controls;
using System.Net;
using System.Threading;
using System.IO;
namespace Simple_YouTube_Music_Player.Forms
{
    public partial class main : MetroForm
    {
        bool backSpiner = false;
        bool ready = false;
        bool Next = false;
        int trackLabelLeft = 0;
        public static int _stream;
        public static Bitmap screen = new Bitmap(1,1);
        private SYNCPROC _mySync;
        
        public main()
        {           
            InitializeComponent();
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            _mySync = new SYNCPROC(EndSync);
            metroToolTip.SetToolTip(loaderPic, "Идет загрузка плейлиста");
            var mode = "";
            switch (Functions.PlayMode)
            {
                case 0: mode = "Повтор всего плейлиста"; break;
                case 1: mode = "Повтор текущего трека"; break;
                case 2: mode = "Случайный трек"; break;
            }
            modeToolTip.SetToolTip(modeBtn, "Режим: "+mode);
            this.KeyPreview = true;
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Functions.OnCloseApp(e);
        }

        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            MetroTextBox edit = (MetroTextBox)sender;
            if (e.KeyCode == Keys.Enter && !String.IsNullOrWhiteSpace(edit.Text))
            {
                edit.Enabled = false;
                Functions.playlist.Clear();
                if (edit.Text.IndexOf("&list") > 0 || edit.Text.IndexOf("?list") > 0)
                {
                    loaderPic.Visible = true;
                }
                ready = false;
                metroProgressSpinner1.Visible = true;
                spinerTimer.Enabled = true;
                Thread general = new Thread(new ParameterizedThreadStart(generalThread));
                general.Start(edit);
            }
        }

        private void generalThread(object s)
        {
            MetroTextBox edit = (MetroTextBox)s;
            Core.GetYoutubeData(Functions.playlist, SetPlayerData, SetLastData, PlayListAdd, edit.Text, Functions.AppData);
        }

        private void PlayListAdd()
        {
            try
            {
                var count = Functions.playlist.Count;
                loaderPic.Invoke((MethodInvoker)(() => metroToolTip.SetToolTip(loaderPic, "Идет загрузка плейлиста (" + count.ToString() + ")")));
            }
            catch
            {
                Functions.OnCloseApp(new EventArgs());
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            Functions.Init();
            //_stream = Bass.BASS_StreamCreateURL("http://wargaming.fm/1", 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
            //Bass.BASS_ChannelPlay(_stream, false);
            //playerControl.playerControl.Init(_stream);
            trackLabelLeft = labelCurrentTrack.Left;
            spectrumTimer.Enabled = true;
            screen = new Bitmap((Bitmap)pictureBox1.Image, pictureBox1.Width,pictureBox1.Height);
            pictureBox1.Image = screen;
            volumeTrackBar.Value = Functions.Volume;
            switch (Functions.PlayMode)
            {
                case 0: modeBtn.Image = Properties.Resources._006_loop_1; break;
                case 1: modeBtn.Image = Properties.Resources._005_loop; break;
                case 2: modeBtn.Image = Properties.Resources._025_shuffle; break;
            }

        }

        private void spinerTimer_Tick(object sender, EventArgs e)
        {
            if (metroProgressSpinner1.Value < 100 && backSpiner == false)
            {
                metroProgressSpinner1.Value = metroProgressSpinner1.Value + 2;
            }
            else if(metroProgressSpinner1.Value <= 100 && backSpiner == true)
            {
                metroProgressSpinner1.Value = metroProgressSpinner1.Value - 2;
            }

            if(metroProgressSpinner1.Value==100 && backSpiner == false)
            {
                backSpiner = true;
            }
            else if(metroProgressSpinner1.Value == 0 && backSpiner == true)
            {
                backSpiner = false;
            }
        }

        public void SetPlayerData(string[] data)
        {
            Bass.BASS_ChannelStop(_stream);
            Bitmap bmp = new Bitmap((new WebClient()).OpenRead(data[2]));
            screen = new Bitmap(bmp, 456, 153);
            pictureBox1.Image = screen;
            _stream = Bass.BASS_StreamCreateURL(data[1], 0, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
            Bass.BASS_ChannelSetSync(_stream, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_MIXTIME,0, _mySync, IntPtr.Zero);
            labelCurrentTrack.Invoke((MethodInvoker)(() => labelCurrentTrack.Text = data[0]));
            playerControl.playerControl.Init(_stream);
            ready = true;
            metroProgressSpinner1.Invoke((MethodInvoker)(() => metroProgressSpinner1.Visible = false));
            metroProgressSpinner1.Invoke((MethodInvoker)(() => spinerTimer.Enabled = false));
            metroProgressSpinner1.Invoke((MethodInvoker)(() => metroToolTip.SetToolTip(labelCurrentTrack, "Кликните для копирования ссылки на видео")));
            metroProgressSpinner1.Invoke((MethodInvoker)(() => metroToolTip.SetToolTip(pictureBox1, "Двойной клик для сохранения")));
            metroProgressSpinner1.Invoke((MethodInvoker)(() => metroToolTip.SetToolTip(pictureBoxSpectrum, "Клик - смена визуализации, Двойной клик - настройка цвета")));
            playerControl.playerControl.SetVolume(Functions.Volume);
        }

        public void SetLastData()
        {
            try
            {
                loaderPic.Invoke((MethodInvoker)(() => loaderPic.Visible = false));
                urlTextBox.Invoke((MethodInvoker)(() => urlTextBox.Enabled = true));
            }
            catch
            {
                Functions.OnCloseApp(new EventArgs());
            }
        }

        public void spectrumTimer_Tick(object sender, EventArgs e)
        {
            labelCurrentTrack.Visible = true;
            Rectangle section = new Rectangle(new Point(0, pictureBox1.Height - pictureBoxSpectrum.Height), new Size(this.pictureBoxSpectrum.Width, this.pictureBoxSpectrum.Height));
            Bitmap bg = Functions.CropImage(screen, section);
            Bitmap total = new Bitmap(this.pictureBoxSpectrum.Width,this.pictureBoxSpectrum.Height);
            List<Bitmap> images = new List<Bitmap>();
            Bitmap spectrum = Functions.SpectrumPaint(_stream, this.pictureBoxSpectrum.Width, this.pictureBoxSpectrum.Height);

            images.Add(bg);
            images.Add(spectrum);

            using (Graphics g = Graphics.FromImage(total))
            {
                g.Clear(Color.Aqua);
                int offset = 0;
                foreach (Bitmap image in images)
                {
                    if (image != null)
                    {
                        g.DrawImage(image, new Rectangle(offset, 0, image.Width, image.Height));
                    }
                }
            }

            this.pictureBoxSpectrum.Image = total;
        }

        private void VolumeTrackBar_Scroll(object sender, ScrollEventArgs e)
        {
 
        }

        private void MuteBtn_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            bool mute = playerControl.playerControl.MuteUnMute(volumeTrackBar.Value);
            if (mute)
            {
                pic.Image = Properties.Resources._030_mute;
            }
            else
            {
                pic.Image = Properties.Resources._034_volume_adjustment;
            }
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (ready)
            {
                positionTrackTimer.Enabled = true;
                bool control = playerControl.playerControl.PlayPause(_stream);
                if (control)
                {
                    pic.Image = Properties.Resources._021_pause;
                }
                else
                {
                    pic.Image = Properties.Resources._013_play;
                }
            }
        }

        private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            MetroTrackBar item = (MetroTrackBar)sender;
            playerControl.playerControl.SetVolume(item.Value);
            Functions.Volume = item.Value;
        }

        private void EndSync(int handle, int channel, int data, IntPtr user)
        {
            if (Functions.playlist.Count > 1)
            {
                Next = true;
            }
            else
            {
                playBtn.Image = Properties.Resources._013_play;
                playerControl.playerControl.PlayerSetControl(false);
                positionTrackTimer.Enabled = false;
            }
        }

        private void positionTrackTimer_Tick(object sender, EventArgs e)
        {
            int math = 0;
            var len = Bass.BASS_ChannelGetLength(_stream);
            var pos = Bass.BASS_ChannelGetPosition(_stream);
            if (pos != 0)
            {
                double dProgress = ((double)pos / len) * 100.0;
                math = (int)Math.Round(dProgress);
            }

            double dProgres = math * (456.0 / 100.0);

            int math2 = (int)Math.Round(dProgres);

            panelProgressTrack.Width = math2;
#if DEBUG
            debugPositionLabel.Visible = true;
            debugPositionLabel.Text = "Len: " + len.ToString() + "\r\nPos: " + pos.ToString()+"\r\nPosP: "+math.ToString()+"\r\nBarLen: "+
                barLen.ToString()+"\r\nPosBar: "+math2.ToString()+"\r\nSpectrum type: "+Functions.SpectrumType.ToString();
#endif
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            Bass.BASS_ChannelStop(_stream);
            playBtn.Image = Properties.Resources._013_play;
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            if (Functions.PlaylistPosition < Functions.playlist.Count-1)
            {
                if (Functions.playlist.Count > 1)
                {
                    Bass.BASS_ChannelStop(_stream);
                    var data = new string[6];
                    switch (Functions.PlayMode)
                    {
                        case 0:
                            data = Functions.NextTrack();
                            break;
                        case 1:
                            data = Functions.LoopTrack();
                            break;
                        case 2:
                            data = Functions.RandomTrack();
                            break;
                    }
                    SetPlayerData(data);
                    if(playerControl.playerControl.pause) Bass.BASS_ChannelPlay(_stream, false);
                    // Set volume
                    float Volume = (float)volumeTrackBar.Value / 100;
                    Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, Volume);
                    /////////////
                }
            }
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            if (Functions.PlaylistPosition > 0)
            {
                Bass.BASS_ChannelStop(_stream);
                var data = Functions.PrevTrack();
                SetPlayerData(data);
                if (playerControl.playerControl.pause) Bass.BASS_ChannelPlay(_stream, false);
                // Set volume
                float Volume = (float)volumeTrackBar.Value / 100;
                Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, Volume);
                /////////////
            }
        }

        private void MovieCurrentTrack_Tick(object sender, EventArgs e)
        {
            if (labelCurrentTrack.Text.Length > 60)
            {
                if (labelCurrentTrack.Left > -labelCurrentTrack.Width)
                {
                    labelCurrentTrack.Left -= 1;
                }
                else
                {
                    labelCurrentTrack.Left = labelCurrentTrack.Width;
                }
            }
            else
            {
                labelCurrentTrack.Left = trackLabelLeft;
            }
        }

        private void NextTrackWD_Tick(object sender, EventArgs e)
        {
            if (Next)
            {
                //Next track
                nextBtn_Click(sender, e);
                Next = false;
            }
        }

        private void PictureBoxSpectrum_Click(object sender, EventArgs e)
        {
            var mode = Functions.SpectrumType;
            if(mode>=0 & mode < 6)
            {
                Functions.SpectrumType += 1;
            }
            else
            {
                Functions.SpectrumType = 0;
            }
        }

        private void PictureBoxSpectrum_DoubleClick(object sender, EventArgs e)
        {
            var color = colorDialog.ShowDialog();
            if (color == DialogResult.OK)
            {
                Functions.SpectrumColor1 = colorDialog.Color;
                color = colorDialog.ShowDialog();
                if (color == DialogResult.OK)
                {
                    Functions.SpectrumColor2 = colorDialog.Color;
                }
            }
        }

        private void ModeBtn_Click(object sender, EventArgs e)
        {
            var mode = Functions.PlayMode;
            PictureBox item = (PictureBox)sender;
            if (mode < 2)
            {
                Functions.PlayMode += 1;
            }
            else
            {
                Functions.PlayMode = 0;
            }
            string modeT = "";
            switch (Functions.PlayMode)
            {
                case 0:
                    modeT = "Повтор всего плейлиста";
                    item.Image = Properties.Resources._006_loop_1;
                break;
                case 1:
                    modeT = "Повтор текущего трека";
                    item.Image = Properties.Resources._005_loop;
                break;
                case 2:
                    modeT = "Случайный трек";
                    item.Image = Properties.Resources._025_shuffle;
                break;
            }
            modeToolTip.SetToolTip(modeBtn, "Режим: " + modeT);
        }

        private void downloaderChange(object sender, DownloadProgressChangedEventArgs e)
        {
            metroProgressSpinner1.Visible = true;
            metroProgressSpinner1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100) metroProgressSpinner1.Visible = false;
        }

        private void DownBtn_Click(object sender, EventArgs e)
        {
            Functions.LoadTrack(downloaderChange);
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                var message = "Simple Youtube Music Player ver. "+Functions.appVer+"\r\n"+
                    "BASS version: "+Functions.verBass+"\r\n"+
                    "Core version: "+Functions.verCore+"\r\n"+
                    "=======================================\r\n"+
                    "Author: UndeX Project (undex.project@gmail.com)";
                MessageBox.Show(message, "Информация о программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LabelCurrentTrack_Click(object sender, EventArgs e)
        {
            if (ready)
            {
                var id = Functions.playlist[Functions.PlaylistPosition];
                Clipboard.SetText("https://youtu.be/" + id[5]);
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (ready)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.AddExtension = true;
                dialog.DefaultExt = ".jpg";
                dialog.Filter = "Изображение jpeg (*.jpg)|*.jpg|Все файлы|*.*";
                dialog.Title = "Сохранить превью \"" + Functions.playlist[Functions.PlaylistPosition][0] + "\"";
                dialog.FileName = Functions.playlist[Functions.PlaylistPosition][0];
                var result = dialog.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;
                Image img = new Bitmap((new WebClient()).OpenRead(Functions.playlist[Functions.PlaylistPosition][2])); ;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bytes = ms.ToArray();
                    System.IO.File.WriteAllBytes(dialog.FileName, bytes);
                }
            }
        }
    }
}
