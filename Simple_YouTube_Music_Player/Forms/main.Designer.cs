namespace Simple_YouTube_Music_Player.Forms
{
    partial class main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.urlTextBox = new MetroFramework.Controls.MetroTextBox();
            this.generalPanel = new MetroFramework.Controls.MetroPanel();
            this.pictureBoxSpectrum = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelProgressTrack = new System.Windows.Forms.Panel();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.spinerTimer = new System.Windows.Forms.Timer(this.components);
            this.playBtn = new System.Windows.Forms.PictureBox();
            this.stopBtn = new System.Windows.Forms.PictureBox();
            this.prevBtn = new System.Windows.Forms.PictureBox();
            this.nextBtn = new System.Windows.Forms.PictureBox();
            this.downBtn = new System.Windows.Forms.PictureBox();
            this.volumeTrackBar = new MetroFramework.Controls.MetroTrackBar();
            this.muteBtn = new System.Windows.Forms.PictureBox();
            this.modeBtn = new System.Windows.Forms.PictureBox();
            this.spectrumTimer = new System.Windows.Forms.Timer(this.components);
            this.labelCurrentTrack = new MetroFramework.Controls.MetroLabel();
            this.positionTrackTimer = new System.Windows.Forms.Timer(this.components);
            this.debugPositionLabel = new System.Windows.Forms.Label();
            this.loaderPic = new System.Windows.Forms.PictureBox();
            this.metroToolTip = new MetroFramework.Components.MetroToolTip();
            this.MovieCurrentTrack = new System.Windows.Forms.Timer(this.components);
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.nextTrackWD = new System.Windows.Forms.Timer(this.components);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.modeToolTip = new MetroFramework.Components.MetroToolTip();
            this.label1 = new System.Windows.Forms.Label();
            this.generalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prevBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.muteBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loaderPic)).BeginInit();
            this.SuspendLayout();
            // 
            // urlTextBox
            // 
            this.urlTextBox.CustomBackground = true;
            this.urlTextBox.Location = new System.Drawing.Point(23, 53);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.PromptText = "Введите URL видео или плейлиста";
            this.urlTextBox.Size = new System.Drawing.Size(462, 23);
            this.urlTextBox.Style = MetroFramework.MetroColorStyle.Blue;
            this.urlTextBox.TabIndex = 0;
            this.urlTextBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.urlTextBox.UseStyleColors = true;
            this.urlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.urlTextBox_KeyDown);
            // 
            // generalPanel
            // 
            this.generalPanel.Controls.Add(this.pictureBoxSpectrum);
            this.generalPanel.Controls.Add(this.pictureBox1);
            this.generalPanel.Controls.Add(this.panelProgressTrack);
            this.generalPanel.HorizontalScrollbarBarColor = true;
            this.generalPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.generalPanel.HorizontalScrollbarSize = 10;
            this.generalPanel.Location = new System.Drawing.Point(23, 92);
            this.generalPanel.Name = "generalPanel";
            this.generalPanel.Size = new System.Drawing.Size(462, 159);
            this.generalPanel.TabIndex = 1;
            this.generalPanel.VerticalScrollbarBarColor = true;
            this.generalPanel.VerticalScrollbarHighlightOnWheel = false;
            this.generalPanel.VerticalScrollbarSize = 10;
            // 
            // pictureBoxSpectrum
            // 
            this.pictureBoxSpectrum.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSpectrum.Location = new System.Drawing.Point(3, 106);
            this.pictureBoxSpectrum.Name = "pictureBoxSpectrum";
            this.pictureBoxSpectrum.Size = new System.Drawing.Size(456, 50);
            this.pictureBoxSpectrum.TabIndex = 3;
            this.pictureBoxSpectrum.TabStop = false;
            this.pictureBoxSpectrum.Click += new System.EventHandler(this.PictureBoxSpectrum_Click);
            this.pictureBoxSpectrum.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxSpectrum_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Simple_YouTube_Music_Player.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(456, 153);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // panelProgressTrack
            // 
            this.panelProgressTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panelProgressTrack.Location = new System.Drawing.Point(3, 59);
            this.panelProgressTrack.Name = "panelProgressTrack";
            this.panelProgressTrack.Size = new System.Drawing.Size(456, 100);
            this.panelProgressTrack.TabIndex = 7;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(237, 254);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(38, 38);
            this.metroProgressSpinner1.Speed = 3F;
            this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroProgressSpinner1.TabIndex = 2;
            this.metroProgressSpinner1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroProgressSpinner1.Visible = false;
            // 
            // spinerTimer
            // 
            this.spinerTimer.Interval = 1;
            this.spinerTimer.Tick += new System.EventHandler(this.spinerTimer_Tick);
            // 
            // playBtn
            // 
            this.playBtn.Image = global::Simple_YouTube_Music_Player.Properties.Resources._013_play;
            this.playBtn.Location = new System.Drawing.Point(23, 257);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(32, 32);
            this.playBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playBtn.TabIndex = 2;
            this.playBtn.TabStop = false;
            this.playBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Image = global::Simple_YouTube_Music_Player.Properties.Resources._003_stop;
            this.stopBtn.Location = new System.Drawing.Point(61, 257);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(32, 32);
            this.stopBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stopBtn.TabIndex = 3;
            this.stopBtn.TabStop = false;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // prevBtn
            // 
            this.prevBtn.Image = global::Simple_YouTube_Music_Player.Properties.Resources._029_previous;
            this.prevBtn.Location = new System.Drawing.Point(99, 257);
            this.prevBtn.Name = "prevBtn";
            this.prevBtn.Size = new System.Drawing.Size(32, 32);
            this.prevBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.prevBtn.TabIndex = 4;
            this.prevBtn.TabStop = false;
            this.prevBtn.Click += new System.EventHandler(this.prevBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Image = global::Simple_YouTube_Music_Player.Properties.Resources._033_forwards_1;
            this.nextBtn.Location = new System.Drawing.Point(137, 257);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(32, 32);
            this.nextBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.nextBtn.TabIndex = 4;
            this.nextBtn.TabStop = false;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // downBtn
            // 
            this.downBtn.Image = global::Simple_YouTube_Music_Player.Properties.Resources._012_upload;
            this.downBtn.Location = new System.Drawing.Point(307, 257);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(32, 32);
            this.downBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.downBtn.TabIndex = 4;
            this.downBtn.TabStop = false;
            this.downBtn.Click += new System.EventHandler(this.DownBtn_Click);
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.volumeTrackBar.Location = new System.Drawing.Point(384, 257);
            this.volumeTrackBar.MouseWheelBarPartitions = 2;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(101, 32);
            this.volumeTrackBar.TabIndex = 5;
            this.volumeTrackBar.Text = "volumeTrackBar";
            this.volumeTrackBar.ValueChanged += new System.EventHandler(this.VolumeTrackBar_ValueChanged);
            // 
            // muteBtn
            // 
            this.muteBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.muteBtn.Image = global::Simple_YouTube_Music_Player.Properties.Resources._034_volume_adjustment;
            this.muteBtn.Location = new System.Drawing.Point(345, 257);
            this.muteBtn.Name = "muteBtn";
            this.muteBtn.Size = new System.Drawing.Size(32, 32);
            this.muteBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.muteBtn.TabIndex = 4;
            this.muteBtn.TabStop = false;
            this.muteBtn.Click += new System.EventHandler(this.MuteBtn_Click);
            // 
            // modeBtn
            // 
            this.modeBtn.Image = global::Simple_YouTube_Music_Player.Properties.Resources._006_loop_1;
            this.modeBtn.Location = new System.Drawing.Point(175, 257);
            this.modeBtn.Name = "modeBtn";
            this.modeBtn.Size = new System.Drawing.Size(32, 32);
            this.modeBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.modeBtn.TabIndex = 4;
            this.modeBtn.TabStop = false;
            this.modeBtn.Click += new System.EventHandler(this.ModeBtn_Click);
            // 
            // spectrumTimer
            // 
            this.spectrumTimer.Interval = 10;
            this.spectrumTimer.Tick += new System.EventHandler(this.spectrumTimer_Tick);
            // 
            // labelCurrentTrack
            // 
            this.labelCurrentTrack.FontSize = MetroFramework.MetroLabelSize.Small;
            this.labelCurrentTrack.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.labelCurrentTrack.Location = new System.Drawing.Point(23, 77);
            this.labelCurrentTrack.Name = "labelCurrentTrack";
            this.labelCurrentTrack.Size = new System.Drawing.Size(462, 15);
            this.labelCurrentTrack.TabIndex = 6;
            this.labelCurrentTrack.Text = "      ";
            this.labelCurrentTrack.Visible = false;
            this.labelCurrentTrack.Click += new System.EventHandler(this.LabelCurrentTrack_Click);
            // 
            // positionTrackTimer
            // 
            this.positionTrackTimer.Interval = 1;
            this.positionTrackTimer.Tick += new System.EventHandler(this.positionTrackTimer_Tick);
            // 
            // debugPositionLabel
            // 
            this.debugPositionLabel.AutoSize = true;
            this.debugPositionLabel.BackColor = System.Drawing.Color.Gray;
            this.debugPositionLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.debugPositionLabel.Location = new System.Drawing.Point(1, 6);
            this.debugPositionLabel.Name = "debugPositionLabel";
            this.debugPositionLabel.Size = new System.Drawing.Size(74, 13);
            this.debugPositionLabel.TabIndex = 7;
            this.debugPositionLabel.Text = "debugPosition";
            this.debugPositionLabel.Visible = false;
            // 
            // loaderPic
            // 
            this.loaderPic.BackColor = System.Drawing.Color.Transparent;
            this.loaderPic.Image = ((System.Drawing.Image)(resources.GetObject("loaderPic.Image")));
            this.loaderPic.Location = new System.Drawing.Point(463, 54);
            this.loaderPic.Name = "loaderPic";
            this.loaderPic.Size = new System.Drawing.Size(20, 20);
            this.loaderPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loaderPic.TabIndex = 8;
            this.loaderPic.TabStop = false;
            this.loaderPic.Visible = false;
            // 
            // metroToolTip
            // 
            this.metroToolTip.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // MovieCurrentTrack
            // 
            this.MovieCurrentTrack.Enabled = true;
            this.MovieCurrentTrack.Interval = 10;
            this.MovieCurrentTrack.Tick += new System.EventHandler(this.MovieCurrentTrack_Tick);
            // 
            // metroPanel1
            // 
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(-1, 69);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(24, 100);
            this.metroPanel1.TabIndex = 9;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel2
            // 
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(485, 69);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(24, 100);
            this.metroPanel2.TabIndex = 9;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // nextTrackWD
            // 
            this.nextTrackWD.Enabled = true;
            this.nextTrackWD.Interval = 10;
            this.nextTrackWD.Tick += new System.EventHandler(this.NextTrackWD_Tick);
            // 
            // modeToolTip
            // 
            this.modeToolTip.Style = MetroFramework.MetroColorStyle.Blue;
            this.modeToolTip.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(430, 5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(1);
            this.label1.Size = new System.Drawing.Size(23, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "☰";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label1_MouseDown);
            this.label1.MouseEnter += new System.EventHandler(this.Label1_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.Label1_MouseLeave);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Label1_MouseUp);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 292);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.debugPositionLabel);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.loaderPic);
            this.Controls.Add(this.labelCurrentTrack);
            this.Controls.Add(this.volumeTrackBar);
            this.Controls.Add(this.muteBtn);
            this.Controls.Add(this.downBtn);
            this.Controls.Add(this.modeBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.prevBtn);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.generalPanel);
            this.Controls.Add(this.urlTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main";
            this.Resizable = false;
            this.Text = "main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.main_FormClosed);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.generalPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSpectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prevBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.muteBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loaderPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroPanel generalPanel;
        public MetroFramework.Controls.MetroTextBox urlTextBox;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.Timer spinerTimer;
        private System.Windows.Forms.PictureBox playBtn;
        private System.Windows.Forms.PictureBox stopBtn;
        private System.Windows.Forms.PictureBox prevBtn;
        private System.Windows.Forms.PictureBox nextBtn;
        private System.Windows.Forms.PictureBox downBtn;
        private MetroFramework.Controls.MetroTrackBar volumeTrackBar;
        private System.Windows.Forms.PictureBox muteBtn;
        private System.Windows.Forms.PictureBox modeBtn;
        public System.Windows.Forms.Timer spectrumTimer;
        public System.Windows.Forms.PictureBox pictureBoxSpectrum;
        private System.Windows.Forms.PictureBox pictureBox1;
        public MetroFramework.Controls.MetroLabel labelCurrentTrack;
        public System.Windows.Forms.Timer positionTrackTimer;
        private System.Windows.Forms.Panel panelProgressTrack;
        private System.Windows.Forms.Label debugPositionLabel;
        public System.Windows.Forms.PictureBox loaderPic;
        private MetroFramework.Components.MetroToolTip metroToolTip;
        private System.Windows.Forms.Timer MovieCurrentTrack;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private System.Windows.Forms.Timer nextTrackWD;
        private System.Windows.Forms.ColorDialog colorDialog;
        public MetroFramework.Components.MetroToolTip modeToolTip;
        private System.Windows.Forms.Label label1;
    }
}