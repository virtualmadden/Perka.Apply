using FluentAssertions;
using Moq;
using NUnit.Framework;
using Perka.Apply.Client.Actions;
using Perka.Apply.Client.Adapters;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client.UnitTests.Actions
{
    [TestFixture]
    public class PerkaActionTests
    {
        [SetUp]
        public void SetUp()
        {
            _perkaApiAdapterMock = new Mock<IPerkaApiAdapter>();

            _perkaApiAdapterMock.Setup(x => x.PostApplicationAsync(It.IsAny<string>())).ReturnsAsync(PerkaResponse);

            _perkaActions = new PerkaActions(_perkaApiAdapterMock.Object);
        }

        private IPerkaActions _perkaActions;
        private Mock<IPerkaApiAdapter> _perkaApiAdapterMock;
        private const string PerkaResponse = "{\"response\": \"Test\"}";

        [Test]
        public void ShouldReturnASuccessfulResponse()
        {
            var result = _perkaActions.PostApplication(new PerkaApplicationRequest()).GetAwaiter().GetResult();

            result.Should().NotBeNull();
            result.Response.Equals("Test");
        }
    }
}