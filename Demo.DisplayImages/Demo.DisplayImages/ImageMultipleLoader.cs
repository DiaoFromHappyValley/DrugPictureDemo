using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
namespace Demo.DisplayImages
{
    public class MultipleImageLoader : ILoaderNotify
    {
        private int imageCount;

        public ManualResetEvent finishedEvent = new ManualResetEvent(false);

        private ImageLoader imageLoader;

        private object _obj = new object();

        private List<ImageObj> imageObjs = new List<ImageObj>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageCount">the count of the pending download images</param>
        /// <param name="defaultImageSize">the size of the image Bytes</param>
        public MultipleImageLoader(int imageCount, int? defaultImageSize = null)
        {
            this.imageCount = imageCount;

            imageLoader = new ImageLoader(defaultImageSize);
        }

        public Action<ImageObj> ImageLoadedCompletedNotify
        {
            get { return imageLoader.ImageLoadedCompletedNotify; }
            set { imageLoader.ImageLoadedCompletedNotify = value; }
        }
        public Action<Exception, ImageObj> ImageLoaedExceptionNotify { get; set; }

        public void LoadImagesAsync(List<ImageObj> objs)
        {
            objs.ForEach(o => LoadImageAsync(o));
        }

        public void LoadImageAsync(ImageObj ware)
        {
            ThreadPool.QueueUserWorkItem(arg =>
            {
                try
                {
                    EndLoadImageAsync(ware);
                }
                catch (Exception ex)
                {
                    lock (_obj)
                    {
                        if (ImageLoaedExceptionNotify != null)
                        {
                            ImageLoaedExceptionNotify(ex, ware);
                        }
                    }
                }
                finally
                {
                    if (Interlocked.Decrement(ref imageCount) == 0)
                    {
                        finishedEvent.Set();

                        imageLoader.RegisterOrUpdateImage(imageObjs);
                    }
                }
            });
        }

        public void EndLoadImageAsync(Object obj)
        {
            var temp = obj as ImageObj;

            if (temp != null)
            {
                imageLoader.LoadImage(temp);

                if (imageLoader.IsValidLocalImage(temp))
                {
                    imageObjs.Add(temp);
                }

                //  imageLoader.OnImageFileNotifycation(temp);
            }

            Debug.WriteLine(string.Format("Thread {0} BillNumber {1}", Thread.CurrentThread.GetHashCode(), ((ImageObj)obj).ImageName));
        }
    }

    public class ImageObj
    {
        public string BillNumber { get; set; }

        public string ImageName { get; set; }
    }
}
