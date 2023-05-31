using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary
{
    /// <summary>
    /// Represents an image resizer.
    /// </summary>
    public interface IImageResizing
    {
        ImageResizing Resize(int width, int height);
        ImageResizing Quality(int quality);
        void Save(string path, bool dispose);
        MemoryStream ToStream();
    }
}
