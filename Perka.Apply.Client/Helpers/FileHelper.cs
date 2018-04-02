using System;
using System.IO;
using System.Linq;

namespace Perka.Apply.Client.Helpers
{
    public interface IFileHelper
    {
        bool ValidateWritePermissions(string filePath);

        FileInfo GetFileInfo(string filePath, string fileName, string extension);

        byte[] ReadFileContentsAsBytes(string fullFilePath);
    }

    public class FileHelper : IFileHelper
    {
        public bool ValidateWritePermissions(string filePath)
        {
            try
            {
                Directory.CreateDirectory(filePath);
                var fileName = Path.Combine(filePath, Guid.NewGuid().ToString());

                using (var fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
                {
                    fs.WriteByte(0xff);
                    fs.Close();
                }

                if (!File.Exists(fileName))
                    return false;

                File.Delete(fileName);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public FileInfo GetFileInfo(string filePath, string fileName, string extension)
        {
            return Directory.GetFiles(filePath, $"{fileName}{extension}").Select(x => new FileInfo(x)).FirstOrDefault();
        }

        public byte[] ReadFileContentsAsBytes(string fullFilePath)
        {
            return File.ReadAllBytes(fullFilePath);
        }
    }
}