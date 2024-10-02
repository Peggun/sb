using Moq;
using sb.core;
using sb.core.interfaces;
using sb.core.models;

namespace sb.tests
{
    public class ConfigServiceTests
    {
        private readonly Mock<IFileSystem> _fileSystemMock;
        private readonly ConfigService _configService;

        public ConfigServiceTests()
        {
            _fileSystemMock = new Mock<IFileSystem>();
            _configService = new ConfigService(_fileSystemMock.Object);
        }

        [Fact]
        public void LoadConfig_ShouldReturnConfig_WhenConfigExists()
        {
            string configJson = "{\"DestinationPath\": \"/path/to/destination\"}";
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);
            _fileSystemMock.Setup(fs => fs.ReadAllText(It.IsAny<string>())).Returns(configJson);

            var config = _configService.LoadConfig();

            Assert.NotNull(config);
            Assert.Equal("/path/to/destination", config.DestinationPath);
        }

        [Fact]
        public void LoadConfig_ShouldCreateNewConfig_WhenConfigDoesNotExist()
        {
            _fileSystemMock.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(false);

            var config = _configService.LoadConfig();

            _fileSystemMock.Verify(fs => fs.CreateFile(It.IsAny<string>()), Times.Once);
            _fileSystemMock.Verify(fs => fs.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.NotNull(config);
        }

        [Fact]
        public void SaveConfig_ShouldWriteToFile()
        {
            var config = new ConfigModel { DestinationPath = "/new/path" };

            _configService.SaveConfig(config);

            _fileSystemMock.Verify(fs => fs.WriteAllText(It.IsAny<string>(), It.Is<string>(json => json.Contains("/new/path"))), Times.Once);
        }
    }
}
