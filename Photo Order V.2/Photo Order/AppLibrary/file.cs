using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AppLibrary
{
    public class File {
        public static string saveImage(string path, OpenFileDialog chooseImage = null, string name = "", bool copy = false) {
            string imageName = "";
            Values.logger.loggerFunction(() => {
                string newName()
                {
                    Random rnd = new Random();
                    string _newName = String.Format("{0}_{1}{2}", DateTime.Now.ToString("yyyyMMdd_hhmmssff"), rnd.Next(1, 999999).ToString(), Path.GetExtension(name));
                    if (System.IO.File.Exists(path + _newName)) {
                        _newName = newName();
                    }
                    return _newName;
                }

                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }

                name = chooseImage == null ? name : chooseImage.FileName;
                imageName = newName();
                var fileNameToSave = path + imageName;
                var imagePath = Path.Combine(fileNameToSave);
                if (!copy)
                {
                    new ImageResizing(name)
                        .Resize(0, 0)
                        .Quality(70)
                        .Save(imagePath);
                } else {
                    System.IO.File.Copy(name, imagePath, true);
                }
                /*using (Stream stream = new FileStream(name, FileMode.Open)) {
                    

                }*/
            });
            return imageName;
        }
        public static string[] saveImageMulti(string path, OpenFileDialog chooseImage)
        {
            string[] imageNames = chooseImage.FileNames;
            Values.logger.loggerFunction(() => {
                int index = 0;
                foreach (var name in chooseImage.FileNames)
                {
                    imageNames[index] = saveImage(path, name: name);
                    index++;
                }
            });
            return imageNames;
        }
        public static void rotateImage(string path, RotateFlipType rotate) {
            Values.logger.loggerFunction(() => {
                using (System.Drawing.Image image = System.Drawing.Image.FromFile(path)) {
                    var rawFormat = image.RawFormat;
                    image.RotateFlip(rotate);
                    image.Save(path, rawFormat);
                }
            });
        }
        public static ImageCodecInfo getImageFormatEncoder(ImageFormat format) {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs) {
                if (codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }
        public static void copyImagesToDesktop(string path,  string desktopFolderPath, string desktopFolderName, List<string> images) {
            Values.logger.loggerFunction(() => {
                string dateFolderName = Var.toStringDateFormatFolder(DateTime.Now);
                string pathFolderWithDate = String.Format("{0}/{1}", desktopFolderPath, dateFolderName);
                string pathFolderWithCustomer = String.Format("{0}/{1}", pathFolderWithDate, desktopFolderName);

                string newNameForSameFile(string image) {
                    Random rnd = new Random();
                    string _newName = String.Format(@"{0}\{1}_{2}{3}", pathFolderWithCustomer, Path.GetFileNameWithoutExtension(image), rnd.Next(1, 999999).ToString(), Path.GetExtension(image));
                    if (System.IO.File.Exists(_newName)) {
                        _newName = newNameForSameFile(image);
                    }
                    return _newName;
                }

                if (!Directory.Exists(desktopFolderPath)) {
                    Directory.CreateDirectory(desktopFolderPath);
                }

                if (!Directory.Exists(pathFolderWithDate)) {
                    Directory.CreateDirectory(pathFolderWithDate);
                }

                if (!Directory.Exists(pathFolderWithCustomer)){
                    Directory.CreateDirectory(pathFolderWithCustomer);
                }

                foreach (var image in images) {
                    if (!System.IO.File.Exists(path + image)) continue;
                    string fileNameToSave = String.Format(@"{0}\{1}", pathFolderWithCustomer, Path.GetFileName(image));
                    if (System.IO.File.Exists(fileNameToSave)) {
                        fileNameToSave = newNameForSameFile(image);
                    }
                    string imagePath = Path.Combine(fileNameToSave);
                    System.IO.File.Copy(path + image, imagePath, true);
                }
            });
        }
        public static void removeImage(string path, string imageName)
        {
            Values.logger.loggerFunction(() =>
            {
                string imagePath = path + imageName;
                if (System.IO.File.Exists(@imagePath)) {
                    System.IO.File.Delete(@imagePath);
                }
            });
        }
        public static void removeFolder(string path) {
            Values.logger.loggerFunction(() => {
                if (System.IO.Directory.Exists(path)) {
                    System.IO.Directory.Delete(path, true);
                }
            });
        }
    }
}
