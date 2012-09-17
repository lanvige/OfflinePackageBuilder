﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Biz.Models;

namespace Biz.Services
{
    public class LevelContentService : IContentServcie
    {
        private const string levelLink = "/services/school/courseware/GetActivityXml.ashx?actvityId={0}&cultureCode={1}&siteVersion={2}&partnerCode={3}&showBlurbs=0&consistentCacheSvr=true&jsoncallback=_jsonp_";
        private readonly Uri fullContentLink;

        public IBaseModule BaseModule { get; set; }
        public string Content { get; set; }

        public LevelContentService(IBaseModule module, IConstants constants)
        {
            this.BaseModule = module as Level;

            // Get all course content.
            this.fullContentLink = new Uri(constants.ServicePrefix + string.Format(levelLink, this.BaseModule.Id, constants.SiteVersion, constants.CultureCode, constants.PartnerCode));
        }

        //public void DownloadTo(string path)
        //{
        //    WebClient c = new WebClient();
        //    c.Headers.Add("Content-Type", "application/json; charset=utf-8");

        //    // Save the content to some path use Async
        //    c.DownloadFileAsync(this.fullContentLink, path);
        //}
    }
}
