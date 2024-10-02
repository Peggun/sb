using Moq;
using sb.core.interfaces;
using sb.core.models;
using sb.core.settings;
using sb.shared;
using System.Runtime.InteropServices;
using System.IO;

namespace sb.tests
{
    public class UpdateDestinationPathTests
    {
        [Fact]
        public void UpdateDestinationPath_ShouldPrintError_WhenPathIsInvalid()
        {
            var mockConfigService = new Mock<IConfigService>(); // Fresh mock
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            var invalidPath = "invalidPath";

            try
            {
                Console.SetOut(stringWriter);

                // Test with an invalid path
                UpdateDestinationPathSetting.UpdateDestinationPath(mockConfigService.Object, invalidPath);

                Assert.Contains($"{invalidPath} is not a valid path. Please make sure the folder exists and is valid.", stringWriter.ToString());
            }
            finally
            {
                Console.SetOut(originalOut); // Ensure console output is reset
            }
        }

        [Fact]
        public void UpdateDestinationPath_ShouldUpdatePath_WhenPathIsValid()
        {
            var mockConfigService = new Mock<IConfigService>(); // Fresh mock
            var validPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Constants.WindowsTestPath : Constants.LinuxTestPath;
            var config = new ConfigModel();
            mockConfigService.Setup(cs => cs.LoadConfig()).Returns(config);

            // Test with a valid path
            UpdateDestinationPathSetting.UpdateDestinationPath(mockConfigService.Object, validPath);

            mockConfigService.Verify(cs => cs.SaveConfig(It.Is<ConfigModel>(c => c.DestinationPath == validPath)));
        }
    }
}
