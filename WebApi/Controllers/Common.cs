﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApi.Controllers
{
    public class Common
    {
        public static string PoleImagePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["PoleImagePath"];
            }
        }
    }
}