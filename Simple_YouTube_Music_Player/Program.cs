using System;
using System.IO;
using System.Windows.Forms;
using Simple_YouTube_Music_Player.Forms;

namespace Simple_YouTube_Music_Player
{
    static class Program
    {
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string appName = Application.ProductName, appVer = Application.ProductVersion;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            load Loader = new load();
            main Form = new main();

            /// <summary>
            /// Установка параметров формы load.cs 
            /// </summary>
            Loader.Text = appName + " (" + appVer + ")";
            Loader.StartPosition = FormStartPosition.CenterScreen;
            Loader.FormBorderStyle = FormBorderStyle.None;
            Loader.pictureBoxLogo.Image = Properties.Resources.logo;
            Loader.Width = Properties.Resources.logo.Width;
            Loader.Height = Properties.Resources.logo.Height;
            Loader.Opacity = 0;

            #if DEBUG
            Loader.opacityDebugText.Visible = true;
            Loader.delayDebugText.Visible = true;
            #endif

            /// <summary>
            /// Проверка/создание дирректории приложения в AppData
            /// </summary>
            if(!Directory.Exists(Path.Combine(AppData, appName)))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(AppData, appName));
                }catch(Exception e)
                {
                    var dialog = MessageBox.Show(e.Source + "\r\n" + e.Message, "Fatal error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if(dialog == DialogResult.Retry)
                    {
                        Application.Restart();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }

            Application.Run(Loader);
        }
    }
}
