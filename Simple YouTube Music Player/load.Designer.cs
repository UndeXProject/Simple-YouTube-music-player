namespace Simple_YouTube_Music_Player
{
    partial class load
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.opacityTimer = new System.Windows.Forms.Timer(this.components);
            this.opacityDebugText = new System.Windows.Forms.Label();
            this.delayDebugText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(356, 192);
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // opacityTimer
            // 
            this.opacityTimer.Enabled = true;
            this.opacityTimer.Interval = 1;
            this.opacityTimer.Tick += new System.EventHandler(this.opacityTimer_Tick);
            // 
            // opacityDebugText
            // 
            this.opacityDebugText.AutoSize = true;
            this.opacityDebugText.BackColor = System.Drawing.Color.Gray;
            this.opacityDebugText.ForeColor = System.Drawing.SystemColors.Control;
            this.opacityDebugText.Location = new System.Drawing.Point(3, 0);
            this.opacityDebugText.Name = "opacityDebugText";
            this.opacityDebugText.Size = new System.Drawing.Size(83, 13);
            this.opacityDebugText.TabIndex = 1;
            this.opacityDebugText.Text = "{DebugOpacity}";
            this.opacityDebugText.Visible = false;
            // 
            // delayDebugText
            // 
            this.delayDebugText.AutoSize = true;
            this.delayDebugText.BackColor = System.Drawing.Color.Gray;
            this.delayDebugText.ForeColor = System.Drawing.SystemColors.Control;
            this.delayDebugText.Location = new System.Drawing.Point(3, 13);
            this.delayDebugText.Name = "delayDebugText";
            this.delayDebugText.Size = new System.Drawing.Size(74, 13);
            this.delayDebugText.TabIndex = 2;
            this.delayDebugText.Text = "{DebugDelay}";
            this.delayDebugText.Visible = false;
            // 
            // load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(356, 192);
            this.Controls.Add(this.delayDebugText);
            this.Controls.Add(this.opacityDebugText);
            this.Controls.Add(this.pictureBoxLogo);
            this.Name = "load";
            this.Opacity = 0D;
            this.TransparencyKey = System.Drawing.Color.Lime;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Timer opacityTimer;
        public System.Windows.Forms.Label opacityDebugText;
        public System.Windows.Forms.Label delayDebugText;
    }
}

