using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.DisplayImages
{
    public interface IImageLoaderNotify
    {
        Action<ImageObj> ImageLoadedCompletedNotify { get; set; }

        Action<Exception, ImageObj> ImageLoaedExceptionNotify { get; set; }
    }
}
