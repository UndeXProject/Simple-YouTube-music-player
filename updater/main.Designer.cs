namespace updater
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelUpdaterVer = new System.Windows.Forms.Label();
            this.labelLastVer = new System.Windows.Forms.Label();
            this.labelNewVer = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxUpdate = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.delayToUpdate = new System.Windows.Forms.Timer(this.components);
            this.labelNote = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelUpdaterVer);
            this.groupBox1.Controls.Add(this.labelLastVer);
            this.groupBox1.Controls.Add(this.labelNewVer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация";
            // 
            // labelUpdaterVer
            // 
            this.labelUpdaterVer.AutoSize = true;
            this.labelUpdaterVer.Location = new System.Drawing.Point(6, 42);
            this.labelUpdaterVer.Name = "labelUpdaterVer";
            this.labelUpdaterVer.Size = new System.Drawing.Size(84, 13);
            this.labelUpdaterVer.TabIndex = 1;
            this.labelUpdaterVer.Text = "Новая версия: ";
            // 
            // labelLastVer
            // 
            this.labelLastVer.AutoSize = true;
            this.labelLastVer.Location = new System.Drawing.Point(6, 29);
            this.labelLastVer.Name = "labelLastVer";
            this.labelLastVer.Size = new System.Drawing.Size(84, 13);
            this.labelLastVer.TabIndex = 1;
            this.labelLastVer.Text = "Новая версия: ";
            // 
            // labelNewVer
            // 
            this.labelNewVer.AutoSize = true;
            this.labelNewVer.Location = new System.Drawing.Point(6, 16);
            this.labelNewVer.Name = "labelNewVer";
            this.labelNewVer.Size = new System.Drawing.Size(84, 13);
            this.labelNewVer.TabIndex = 0;
            this.labelNewVer.Text = "Новая версия: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBoxUpdate);
            this.groupBox2.Location = new System.Drawing.Point(12, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 181);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Что нового";
            // 
            // richTextBoxUpdate
            // 
            this.richTextBoxUpdate.BackColor = System.Drawing.Color.White;
            this.richTextBoxUpdate.Location = new System.Drawing.Point(6, 19);
            this.richTextBoxUpdate.Name = "richTextBoxUpdate";
            this.richTextBoxUpdate.ReadOnly = true;
            this.richTextBoxUpdate.Size = new System.Drawing.Size(337, 156);
            this.richTextBoxUpdate.TabIndex = 0;
            this.richTextBoxUpdate.Text = "";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 283);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(349, 11);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(200, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // delayToUpdate
            // 
            this.delayToUpdate.Enabled = true;
            this.delayToUpdate.Interval = 2000;
            this.delayToUpdate.Tick += new System.EventHandler(this.delayToUpdate_Tick);
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.BackColor = System.Drawing.Color.Transparent;
            this.labelNote.Location = new System.Drawing.Point(12, 267);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(150, 13);
            this.labelNote.TabIndex = 5;
            this.labelNote.Text = "Подготовка к обновлению...";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 301);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.main_FormClosed);
            this.Shown += new System.EventHandler(this.main_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.RichTextBox richTextBoxUpdate;
        public System.Windows.Forms.Label labelUpdaterVer;
        public System.Windows.Forms.Label labelLastVer;
        public System.Windows.Forms.Label labelNewVer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer delayToUpdate;
        public System.Windows.Forms.Label labelNote;
    }
}

