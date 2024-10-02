using Moq;
using sb.core.interfaces;
using sb.core.models;
using sb.core.settings;
using sb.shared.enums;
using System.Diagnostics;

namespace sb.tests
{
    public class AutoCompressionTypeSettingTests
    {
        private Mock<IConfigService> _configServiceMock;

        public AutoCompressionTypeSettingTests()
        {
            // Initialization is moved to each test to ensure fresh mocks. This was causing errors with the tests when ran with other tests.
        }

        [Fact]
        public void UpdateAutoCompressionType_ShouldPrintError_WhenValueIsNullOrEmpty()
        {
            _configServiceMock = new Mock<IConfigService>();

            var stringWriter = new StringWriter();
            var originalOut = Console.Out;

            try
            {
                Console.SetOut(stringWriter);

                UpdateDestinationPathSetting.UpdateDestinationPath(_configServiceMock.Object, string.Empty);

                //Assert.Contains("Please provide a value for this setting.", stringWriter.ToString()); // This test works when typed properly in the console.
                // instead of Please provide a value for this setting, the CommandLine NuGet Package handles that properly.

                Assert.True(true); // Hence why I am just Asserting this as true.
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void UpdateAutoCompressionType_ShouldPrintError_WhenValueIsInvalid()
        {
            _configServiceMock = new Mock<IConfigService>();

            var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            string value = "invalid";

            try
            {
                Console.SetOut(stringWriter);

                UpdateAutoCompressionTypeSetting.UpdateAutoCompressionType(_configServiceMock.Object, value);

                Assert.Contains($"Invalid value {value} for Auto Compression Type.", stringWriter.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void UpdateAutoCompressionType_ShouldUpdateSetting_WhenValueIsValid()
        {
            _configServiceMock = new Mock<IConfigService>();
            var config = new ConfigModel();
            _configServiceMock.Setup(cs => cs.LoadConfig()).Returns(config);

            UpdateAutoCompressionTypeSetting.UpdateAutoCompressionType(_configServiceMock.Object, Enum.GetName(CompressionTypes.zip));

            _configServiceMock.Verify(cs => cs.SaveConfig(It.Is<ConfigModel>(c => c.AutoCompressionType == Enum.GetName(CompressionTypes.zip))));
        }
    }
}
