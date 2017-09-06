using System;
using Perka.Apply.Client.Helpers;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client.Adapters
{
    public interface IFileSystemAdapter
    {
        string GetBase64EncodedResume();
    }

    public class FileSystemAdapter : IFileSystemAdapter
    {
        private readonly FileConfiguration _fileConfiguration = new FileConfiguration
        {
            Name = ApplicationSettingsAdapter.ApplicationSettings.Resume.Name,
            Location = ApplicationSettingsAdapter.ApplicationSettings.Resume.Location,
            Extension = ApplicationSettingsAdapter.ApplicationSettings.Resume.Extension
        };

        private readonly IFileHelper _fileHelper;

        public FileSystemAdapter(IFileHelper fileHelper = null)
        {
            _fileHelper = fileHelper ?? new FileHelper();
        }

        public string GetBase64EncodedResume()
        {
            try
            {
                if (!_fileHelper.ValidateWritePermissions(_fileConfiguration.Location))
                {
                    throw new Exception("Cannot access file.");
                }

                var fileInfo = _fileHelper.GetFileInfo(_fileConfiguration.Location, _fileConfiguration.Name, _fileConfiguration.Extension);

                var contents = ReadFileContentsAsBytes(fileInfo.FullName);

                return Convert.ToBase64String(contents);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private byte[] ReadFileContentsAsBytes(string fileFullName)
        {
            return _fileHelper.ReadFileContentsAsBytes(fileFullName);
        }
    }
}