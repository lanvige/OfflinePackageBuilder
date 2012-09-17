﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Biz.Services
{
    public class DownloadService : IDownloadService
    {
        public string DownloadFromPath(Uri url)
        {
            string courseContent = string.Empty;
            WebClient c = new WebClient();
            c.Headers.Add("Content-Type", "application/json; charset=utf-8");

            try
            {
                courseContent = c.DownloadString(url);
            }
            catch (WebException ex)
            {
                Logger.Write(url.ToString() + "\r\n" +  ex);
            }

            return courseContent;
        }

        public string DownloadFromPath(Uri url, string path)
        {
            var data = DownloadFromPath(url);
            SaveTo(data, path);
            return data;
        }


        public void MediaDownload(Uri url, string path)
        {
            WebClient c = new WebClient();

            try
            {
                c.DownloadFile(url, path);
            }
            catch (WebException ex)
            {
                Logger.Write(url.ToString() + "\r\n" + ex);
            }
        }

        public void SaveTo(string content, string path)
        {
            if (Directory.Exists(Path.GetDirectoryName(path)))
            {
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            try
            {
                // Save the content to some path use Async
                //c.DownloadFile(url, path);
                File.WriteAllText(path, content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}