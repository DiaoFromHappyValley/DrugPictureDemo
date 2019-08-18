using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Demo.DisplayImages
{
    public sealed class ImageLoader
    {
        private static string drugImageUrl;
        private static string imageLocalDir;
        private static string registeredXmlFileFullName;

        /// <summary>
        /// the size of the image Bytes
        /// </summary>
        private int? defaultImageSize;

        internal Action<ImageObj> ImageLoadedCompletedNotify;

        static ImageLoader()
        {
            registeredXmlFileFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageFilesRegistor.xml");
            if (!File.Exists(registeredXmlFileFullName))
            {
                throw new Exception("ImageFilesRegistor.xml don't exists!");
            }

            imageLocalDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            if (!Directory.Exists(imageLocalDir))
            {
                Directory.CreateDirectory(imageLocalDir);
            }

            drugImageUrl = "https://img.infinitynewtab.com/wallpaper";
        }

        public ImageLoader(int? defaultImageSize = null)
        {
            this.defaultImageSize = defaultImageSize;
        }
        public void LoadImage(ImageObj imageObj)
        {
            if (imageObj == null)
            {
                throw new ArgumentException("Invalidate imageObj!");
            }

            if (string.IsNullOrEmpty(imageObj.BillNumber) || string.IsNullOrEmpty(imageObj.BillNumber.Trim()))
            {
                throw new ArgumentException("Invalidate billNumber!");
            }

            EnsureImageToLocal(imageObj.BillNumber, imageObj.ImageName);
        }

        public void RegisterOrUpdateImage(List<ImageObj> imageObjs)
        {
            if (imageObjs == null)
            {
                throw new ArgumentException("Invalidate imageObjs!");
            }

            XElement xml = XElement.Load(registeredXmlFileFullName);
            foreach (var imageObj in imageObjs)
            {
                var image = xml.Descendants("DrugImage").FirstOrDefault(item => item.Attribute("name").Value == imageObj.ImageName);

                if (image != null)
                {
                    image.SetAttributeValue("ExpireDate", DateTime.Now.ToString("yyyy-MM-dd"));
                }
                else
                {
                    var el = new XElement("DrugImage");
                    el.SetAttributeValue("billNum", imageObj.ImageName.ToString().Split('.')[0]);
                    el.SetAttributeValue("name", imageObj.ImageName);
                    el.SetAttributeValue("ExpireDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    xml.Add(el);
                }
            }

            xml.Save(registeredXmlFileFullName);
        }

        internal bool IsValidLocalImage(ImageObj temp)
        {
            if (IsSupportedDownloadedDefaultImage())
            {
                var localImageName = Path.Combine(imageLocalDir, temp.ImageName);

                using (FileStream fs = new FileStream(localImageName, FileMode.Open, FileAccess.Read))
                {
                    if (defaultImageSize.HasValue)
                    {
                        var actualValue = defaultImageSize.Value;
                        var length = fs.Length;
                        if (Math.Abs((actualValue - 500)) < length && length < (actualValue + 500))
                        {
                            return false;
                        }
                    }

                }
            }

            return true;
        }

        private bool IsSupportedDownloadedDefaultImage()
        {
            //CHECK WHETHER THE DEFALT IMAGES WILL BE DOWNLOAD.
            return defaultImageSize.HasValue;
        }

        private void EnsureImageToLocal(string billNumber, string imageName)
        {
            EnsureImageRegistered(imageName);

            EnsureImageInLocalDisk(billNumber, imageName);

        }

        private void EnsureImageRegistered(string imageName)
        {
            if (!IsImageRegirsted(imageName))
            {
                DownloadImageFile(imageName);
            }
        }

        private void EnsureImageInLocalDisk(string billnumber, string imageName)
        {
            var localImageName = Path.Combine(imageLocalDir, imageName);

            if (!File.Exists(Path.Combine(imageLocalDir, imageName)))
            {
                DownloadImageFile(imageName);
            }

            OnImageFileNotifycation(new ImageObj
            {
                BillNumber = billnumber,
                ImageName = imageName
            });
        }

        private static bool IsImageRegirsted(string imageName)
        {
            var xml = XElement.Load(registeredXmlFileFullName);
            var image = xml.Descendants("DrugImage")
                             .FirstOrDefault(item => item.Attribute("name").Value == imageName);

            if (image == null)
            {
                return false;
            }

            return true;
        }

        private void DownloadImageFile(string imageName)
        {
            if (imageName.Contains("2.") || imageName.Contains("6."))
            {
                throw new NotImplementedException();
            }

            using (WebClient webClient = new WebClient())
            {
                var localImageName = Path.Combine(imageLocalDir, imageName);
                var remoteImageName = Path.Combine(drugImageUrl, imageName);

                webClient.DownloadFile(new Uri(remoteImageName), localImageName);
            }
        }

        public void OnImageFileNotifycation(ImageObj obj)
        {
            var temp = ImageLoadedCompletedNotify;

            if (temp != null)
            {
                temp(obj);
            }
        }
    }
}
