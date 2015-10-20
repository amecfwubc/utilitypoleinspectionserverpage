using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PoleImageController : ApiController
    {
        // GET: api/PoleImage
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/PoleImage/5
        public HttpResponseMessage Get(string ImagePath)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            //string ImagePath = Common.PoleImagePath;
            //String ImagePath = ImagePath + FileName;
            if (File.Exists(ImagePath))
            {
                FileStream fileStream = new FileStream(ImagePath, FileMode.Open, FileAccess.Read);
                                
                MemoryStream memoryStream = new MemoryStream();
                Image image = Image.FromStream(fileStream);
                image.Save(memoryStream, ImageFormat.Jpeg);
                result.Content = new ByteArrayContent(memoryStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                fileStream.Close();
            }
            return result;
        }

        // POST: api/PoleImage
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PoleImage/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PoleImage/5
        public void Delete(int id)
        {
        }

        #region Sample for Api Image load code

        private async void LoadImage()
        {
            string FileName = "";
            string PoleImagePath = "http://desktop-9bmhfp2:88//api/PoleImage/Get?FileName=" + FileName;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(PoleImagePath))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var data = await response.Content.ReadAsStreamAsync();
                        Image image = Image.FromStream(data);

                        //string projectPath = AppDomain.CurrentDomain.BaseDirectory;
                        //projectPath = Application.StartupPath;
                        //string appPath = Application.StartupPath;
                        //string rootpath = appPath.Split(new string[] { "\\bin\\" }, StringSplitOptions.None)[0];
                        //string pathtosave = "E:/TestImage/fff.jpg";
                        //pathtosave = rootpath+@"\Image\" + "fff.jpg";
                        //image.Save(pathtosave, ImageFormat.Jpeg);

                        //pictureBox1.Image = image;
                        //var productJsonString = await response.Content.ReadAsStringAsync();
                        //var data = JsonConvert.DeserializeObject<PoelInfo[]>(productJsonString).ToList();

                    }
                }
            }
        }

        private async void LoadImage2()
        {
            string FileName = "";
            string PoleImagePath = "http://desktop-9bmhfp2:88//api/PoleImage/Get?FileName=" + FileName;
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(PoleImagePath))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var data = await response.Content.ReadAsStreamAsync();
                        //data.WriteAsync
                        Image image = Image.FromStream(data);
                        //Image image2 = data.WriteAsync(;
                    }
                }
            }
        }
        #endregion
    }
}
