using FluentAssertions;
using NUnit.Framework;
using Perka.Apply.Client.Adapters;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client.Tests.Adapters
{
    [TestFixture]
    public class ApplicationSettingsAdapterTests
    {
        [SetUp]
        public void SetUp()
        {
            _applicationSettings = ApplicationSettingsAdapter.ApplicationSettings;
        }

        private ApplicationSettings _applicationSettings;

        [Test]
        public void ShouldReturnANonEmptyApplicationSettings()
        {
            _applicationSettings.Should().NotBeNull();
            _applicationSettings.FirstName.Should().Be("first_name");
            _applicationSettings.LastName.Should().Be("last_name");
            _applicationSettings.Email.Should().Be("email");
            _applicationSettings.PositionId.Should().Be("GENERALIST");
            _applicationSettings.Explanation.Should().Be("explanation");
            _applicationSettings.Source.Should().Be("source");
            _applicationSettings.GithubApi.Name.Should().Be("Github");
            _applicationSettings.GithubApi.Uri.Should().Be("http://localhost");
            _applicationSettings.PerkaApi.Name.Should().Be("Perka");
            _applicationSettings.PerkaApi.Uri.Should().Be("http://localhost");
            _applicationSettings.Resume.Name.Should().Be("file_name");
            _applicationSettings.Resume.Location.Should().Be("file_location");
            _applicationSettings.Resume.Extension.Should().Be("file_extension");
        }
    }
}