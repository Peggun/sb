using Moq;
using sb.core.interfaces;
using sb.core.models;
using sb.core.settings;
using sb.shared;
using System.Runtime.InteropServices;

namespace sb.tests
{
    public class UpdateDestinationPathTests
    {
        private readonly Mock<IConfigService> _mockConfigService;

        public UpdateDestinationPathTests()
        {
            _mockConfigService = new Mock<IConfigService>();
        }

        [Fact]
        public void UpdateDestinationPath_ShouldPrintError_WhenPathIsNullOrEmpty()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            UpdateDestinationPathSetting.UpdateDestinationPath(_mockConfigService.Object, string.Empty);

            Assert.Contains("Please add a value for this setting.", stringWriter.ToString());
        }

        [Fact]
        public void UpdateDestinationPath_ShouldPrintError_WhenPathIsInvalid()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var invalidPath = "invalidPath";

            UpdateDestinationPathSetting.UpdateDestinationPath(_mockConfigService.Object, invalidPath);

            Assert.Contains($"{invalidPath} is not a valid path. Please make sure the folder exists and is valid.", stringWriter.ToString());
        }

        [Fact]
        public void UpdateDestinationPath_ShouldUpdatePath_WhenPathIsValid()
        {
            var validPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Constants.WindowsTestPath : Constants.LinuxTestPath;
            var config = new ConfigModel();
            _mockConfigService.Setup(cs => cs.LoadConfig()).Returns(config);

            UpdateDestinationPathSetting.UpdateDestinationPath(_mockConfigService.Object, validPath);

            _mockConfigService.Verify(cs => cs.SaveConfig(It.Is<ConfigModel>(c => c.DestinationPath == validPath)));
        }
    }
}
