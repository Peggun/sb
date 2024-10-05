using Moq;
using sb.core.interfaces;
using sb.core.models;
using sb.core.settings;

namespace sb.tests
{
    public class AutoCompressionTests
    {
        [Fact]
        public void UpdateAutoCompression_ShouldPrintError_WhenValueIsInvalid()
        {
            var mockConfigService = new Mock<IConfigService>(); // Fresh mock for each test
            var stringWriter = new StringWriter();
            var originalOut = Console.Out;

            try
            {
                Console.SetOut(stringWriter);

                // Test with invalid value
                UpdateAutoCompressionSetting.UpdateAutoCompression(mockConfigService.Object, "invalid");
                //Console.WriteLine(stringWriter.ToString());
                Assert.Contains("Invalid value for this setting. Please use 'True' or 'False'", stringWriter.ToString());
            }
            finally
            {
                Console.SetOut(originalOut); // Ensure console output is reset
            }
        }

        [Fact]
        public void UpdateAutoCompression_ShouldUpdateSetting_WhenValueIsValid()
        {
            var mockConfigService = new Mock<IConfigService>(); // Fresh mock
            var config = new ConfigModel();
            mockConfigService.Setup(cs => cs.LoadConfig()).Returns(config);

            UpdateAutoCompressionSetting.UpdateAutoCompression(mockConfigService.Object, "true");

            mockConfigService.Verify(cs => cs.SaveConfig(It.Is<ConfigModel>(c => c.AutoCompression == true)));
        }
    }
}