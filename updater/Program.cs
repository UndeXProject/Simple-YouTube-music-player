using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace updater
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var Form = new main();
            string[] arg = Environment.GetCommandLineArgs();
            Form.labelLastVer.Text = "Новая версия: " + arg[2];
            Form.labelNewVer.Text = "Текущая версия: " + arg[3];
            Form.AppDir = arg[1];
            Form.labelUpdaterVer.Text = "Версия ПО обновления: " + Application.ProductVersion;
            Form.Text = "Обновление до версии " + arg[2];
            Application.Run(Form);
        }
    }
}
