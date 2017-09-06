using System.IO;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Perka.Apply.Client.Adapters;
using Perka.Apply.Client.Helpers;

namespace Perka.Apply.Client.Tests.Adapters
{
    [TestFixture]
    public class FileSystemAdapterTests
    {
        [SetUp]
        public void SetUp()
        {
            _fileHelperMoq = new Mock<IFileHelper>();

            _fileHelperMoq.Setup(x => x.ValidateWritePermissions(It.IsAny<string>())).Returns(true);
            _fileHelperMoq.Setup(x => x.GetFileInfo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new FileInfo("test"));
            _fileHelperMoq.Setup(x => x.ReadFileContentsAsBytes(It.IsAny<string>())).Returns(new[] {new byte(), new byte()});

            _fileSystemAdapter = new FileSystemAdapter(_fileHelperMoq.Object);
        }

        private IFileSystemAdapter _fileSystemAdapter;
        private Mock<IFileHelper> _fileHelperMoq;

        [Test]
        public void ShouldReturnBase64EncryptedString()
        {
            var encryptedContents = _fileSystemAdapter.GetBase64EncodedResume();

            encryptedContents.Should().NotBeNullOrEmpty();
        }
    }
}