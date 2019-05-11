using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace Simple_YouTube_Music_Player
{
    public class Core
    {
        /// <summary>
        /// Версия YouTube-dl
        /// </summary>
        const string Version_YouTubeDl = "2019.04.30";
        private static string AppData = "";

        public static string Init(string AppDataDir)
        {
            string VersionYoutubeDL = "";
            try
            {
                System.IO.File.WriteAllBytes(Path.Combine(AppDataDir, "cache.tmp"), Properties.Resources.youtube_dl);
                string[] arg = { "--version" };
                File.File.Run(delegate (string s) { VersionYoutubeDL = s; }, Console.In, Path.Combine(AppDataDir, "cache.tmp"), arg);
                System.IO.File.Delete(Path.Combine(AppDataDir, "cache.tmp"));
                AppData = AppDataDir;
                return VersionYoutubeDL;
            }catch(Exception e)
            {
                return e.Source + "\r\n"+e.Message;
            }
        }

        private static readonly HttpClient client = new HttpClient();
        public async static void CheckUpdates(Action<string> s, string CurrenVersion)
        {
            var getVer = await client.GetStringAsync("https://raw.githubusercontent.com/UndeXProject/Simple-YouTube-music-player/master/.version");
            if (getVer.Trim() != CurrenVersion) s(getVer.Trim()); else s("");            
        }

        public static void UpdateSetup(string UpdateFileName, int pos, string[] ver)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e) { pos = e.ProgressPercentage; };
                wc.DownloadFileCompleted += delegate (object sender, AsyncCompletedEventArgs e) {
                    //System.IO.File.SetAttributes(UpdateFileName, FileAttributes.Hidden);
                    var zip = ZipFile.OpenRead(UpdateFileName);
                    var path = Path.Combine(Path.GetDirectoryName(UpdateFileName), "update_setup.exe");
                    foreach (var file in zip.Entries)
                    {
                        if (file.Name == "_update")
                        {
                            file.ExtractToFile(path,true);
                            ProcessStartInfo psi = new ProcessStartInfo();
                            string[] args = { Environment.CurrentDirectory, ver[0], ver[1] };
                            psi.UseShellExecute = false;
                            psi.RedirectStandardError = true;
                            psi.RedirectStandardOutput = true;
                            psi.RedirectStandardInput = true;
                            psi.WindowStyle = ProcessWindowStyle.Normal;
                            psi.CreateNoWindow = false;
                            psi.ErrorDialog = false;
                            psi.WorkingDirectory = Path.GetDirectoryName(UpdateFileName);
                            psi.FileName = path;
                            psi.Arguments = File.File.EscapeArguments(args); // see [url]http://csharptest.net/?p=529[/url]
                            Process.Start(psi);
                        }
                    }
                    if (!System.IO.File.Exists(path))
                    {
                        throw new Exception("Не удалось выгрузить установщик обновления");
                    }
                };
                if (System.IO.File.Exists(UpdateFileName))
                {
                    System.IO.File.Delete(UpdateFileName);
                }
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://raw.githubusercontent.com/UndeXProject/Simple-YouTube-music-player/master/build/update.pkg"),
                    // Param2 = Path to save
                    UpdateFileName
                );
            }
        }

        public static void GetYoutubeData(List<string[]> result, Action<string[]> First, Action Last, Action One, string url, string AppDataDir)
        {
            System.IO.File.WriteAllBytes(Path.Combine(AppDataDir, "cache.tmp"), Properties.Resources.youtube_dl);
            string[] _response = {"","","","","",""};
            bool first = true;
            string urls = NormalizeURL(url);
            string[] argPlaylist = { "-e", "--get-thumbnail", "--get-filename", "-f [ext = mp4]", "-g", "--get-duration", "--get-id", url };
            int i = 0;
            File.File.Run(delegate (string s) {
                Regex regex = new Regex(@"ERROR: (.*)");
                Match match = regex.Match(s);
                if (match.Groups[1].Length > 0)
                {
                    MessageBox.Show("Ошибка получения данных!\r\n" + match.Groups[1].Value, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _response[i] = null;
                }
                else
                {
                    Regex reg = new Regex(@"WARNING: (.*)");
                    Match math = reg.Match(s);
                    if (math.Groups[1].Length > 0)
                    {

                    }
                    else
                    {
                        _response[i] = s; i++;
                    }
                    if (i == 6)
                    {
                        i = 0;
                        // Смена значений
                        var _tmpName = _response[0];
                        var _tmpID = _response[1];
                        var _tmpURL = _response[2];
                        var _tmpThumb = _response[3];
                        var _tmpFile = _response[4];
                        var _tmpDur = _response[5];

                        _response[1] = _tmpURL;
                        _response[2] = _tmpThumb;
                        _response[3] = _tmpFile;
                        _response[4] = _tmpDur;
                        _response[5] = _tmpID;
                        ///////////////////

                        // Получить имя с поддержкой Unicode
                        var youTube = YouTube.Default;
                        try
                        {
                            YouTubeVideo YouTubeURLClass = youTube.GetVideo("https://youtu.be/" + _tmpID);
                            _response[0] = YouTubeURLClass.Title.Replace("- YouTube", "");
                        }
                        catch
                        {
                            _response[0] = _tmpName;
                        }

                        if (first)
                        {
                            First(_response);
                            first = false;
                        }

                        result.Add(_response);
                        One();
                        _response = new string[6];
                    }
                }
            }, Console.In, Path.Combine(AppDataDir, "cache.tmp"), argPlaylist);
            System.IO.File.Delete(Path.Combine(AppDataDir, "cache.tmp"));
            Last();
        }

        /// <summary>
        /// Нормализация URL Youtube
        /// </summary>
        /// <param name="text">url youtube</param>
        /// <returns>normalizated url</returns>
        private static string NormalizeURL(string text)
        {
            Regex regex = new Regex(@"list=(.+)");
            foreach (Match match in regex.Matches(text))
            {
                text = "PlayList";
                //MessageBox.Show(match.Groups[1].Value);
            }

            regex = new Regex(@"\?v=([\w\-]+)");
            foreach (Match match in regex.Matches(text))
            {
                text = match.Groups[1].Value;
                //MessageBox.Show(match.Groups[1].Value);
            }
            regex = new Regex(@"\.be\/([\w\-]+)");
            foreach (Match match in regex.Matches(text))
            {
                text = match.Groups[1].Value;
                //MessageBox.Show(match.Groups[1].Value);
            }

            return text;
        }
    }
}
