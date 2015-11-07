﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PoleClass
    {

        public static Image Base64ToImage(string base64String)
        {
            base64String = base64String.Replace("data:image/png;base64,", "");
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            return image;
        }
    }

    public class PoleImageList
    {
        public int ID { get; set; }
        public int PoleInfoID { get; set; }
        
        public string PoleID { get; set; }
        public string Image64 { get; set; }
    }


}