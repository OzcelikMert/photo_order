using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace AppLibrary
{
    public class Image {
        public static void setImage(System.Windows.Controls.Image image, string path, bool isDecodePixel = false, int decodePixel = 0) {
            Values.logger.loggerFunction(() => {
                if (!System.IO.File.Exists(path)) return;
                var bi = new BitmapImage();
                Rotation rotation = autoRotate(path);
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = stream;
                    bi.Rotation = rotation;
                    if(isDecodePixel) {
                        if (bi.DecodePixelWidth > bi.DecodePixelHeight) bi.DecodePixelWidth = decodePixel;
                        else bi.DecodePixelHeight = decodePixel;
                    }
                    bi.EndInit();
                }
                image.Source = bi;
                bi.StreamSource = null;
                bi.Freeze();
            });
        }
        public static Rotation autoRotate(String path) {
            string _orientationQuery = "System.Photo.Orientation";
            Rotation rotation = Rotation.Rotate0;
            using (Stream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                BitmapFrame bitmapFrame = BitmapFrame.Create(fileStream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                BitmapMetadata bitmapMetadata = bitmapFrame.Metadata as BitmapMetadata;

                if ((bitmapMetadata != null) && (bitmapMetadata.ContainsQuery(_orientationQuery)))
                {
                    object o = bitmapMetadata.GetQuery(_orientationQuery);

                    if (o != null)
                    {
                        switch ((ushort)o)
                        {
                            case 6:
                                {
                                    rotation = Rotation.Rotate90;
                                }
                                break;
                            case 3:
                                {
                                    rotation = Rotation.Rotate180;
                                }
                                break;
                            case 8:
                                {
                                    rotation = Rotation.Rotate270;
                                }
                                break;
                        }
                    }
                }
            }

            return rotation;
        }
        public static RotateFlipType convertToFlipRotate(int rotate) {
            RotateFlipType result = RotateFlipType.RotateNoneFlipNone;
            switch (rotate) {
                case 90:
                case -270: result = RotateFlipType.Rotate90FlipNone; break;
                case 180:
                case -180: result = RotateFlipType.Rotate180FlipNone; break;
                case 270:
                case -90: result = RotateFlipType.Rotate270FlipNone; break;
            }
            return result;
        }
    }
}
