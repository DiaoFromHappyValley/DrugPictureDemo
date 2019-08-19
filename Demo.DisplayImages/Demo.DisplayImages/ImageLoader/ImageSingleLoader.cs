using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Demo.DisplayImages
{
    public class ImageSingleLoader : IImageLoaderNotify
    {
        private ImageLoader imageLoader;

        private Thread singleThread = null;

        public ImageSingleLoader()
        {
            imageLoader = new ImageLoader();
        }

        public Action<ImageObj> ImageLoadedCompletedNotify
        {
            get { return imageLoader.ImageLoadedCompletedNotify; }
            set { imageLoader.ImageLoadedCompletedNotify = value; }
        }
        public Action<Exception, ImageObj> ImageLoaedExceptionNotify { get; set; }

        public void LoadImageAsync(ImageObj imageObj)
        {
            try
            {
                singleThread = new Thread((object obj) =>
                {
                    try
                    {
                        imageLoader.LoadImage(imageObj);
                    }
                    catch (Exception ex)
                    {
                        if (ImageLoaedExceptionNotify != null)
                        {
                            ImageLoaedExceptionNotify(ex, imageObj);
                        }
                    }

                });

                singleThread.Start();
            }
            finally
            {
                List<ImageObj> singleImageObj = new List<ImageObj>();
                singleImageObj.Add(imageObj);
                imageLoader.RegisterOrUpdateImage(imageObjs);
                //Dispose Resources
            }

        }
    }
}
