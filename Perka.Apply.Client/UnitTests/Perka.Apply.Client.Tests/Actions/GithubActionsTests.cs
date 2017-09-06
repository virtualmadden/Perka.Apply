using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Perka.Apply.Client.Actions;
using Perka.Apply.Client.Adapters;

namespace Perka.Apply.Client.Tests.Actions
{
    [TestFixture]
    public class GithubActionsTests
    {
        [SetUp]
        public void SetUp()
        {
            _githubAdapterMock = new Mock<IGithubApiAdapter>();

            _githubAdapterMock.Setup(x => x.GetOrdersAsync()).ReturnsAsync(GithubResponse);

            _githubActions = new GithubActions(_githubAdapterMock.Object);
        }

        private IGithubActions _githubActions;
        private Mock<IGithubApiAdapter> _githubAdapterMock;
        private const string GithubResponse = "[{ \"name\": \"test\", \"html_url\": \"http://www.test.com\" }]";

        [Test]
        public void ShouldReturnAListOfProjects()
        {
            var result = _githubActions.GetProjects().GetAwaiter().GetResult();

            result.Should().NotBeNullOrEmpty();
            result.First().Name.Should().Be("test");
            result.First().Uri.Should().Be("http://www.test.com");
        }
    }
}