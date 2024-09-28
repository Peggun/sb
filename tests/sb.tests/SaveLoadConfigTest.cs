using Moq;
using sb.core;
using sb.core.models;
using sb.core.interfaces;

public class ConfigServiceTests
{
    private readonly Mock<IFileSystem> _fileSystemMock;

    public ConfigServiceTests()
    {
        _fileSystemMock = new Mock<IFileSystem>();
        ConfigService.SetFileSystem(_fileSystemMock.Object);
    }

    [Fact]
    public void LoadConfig_ShouldReturnConfig_WhenConfigExists()
    {
        string configJson = "{\"DestinationPath\": \"/path/to/destination\"}";
        _fileSystemMock.Setup(fs => fs.Exists(It.IsAny<string>())).Returns(true);
        _fileSystemMock.Setup(fs => fs.ReadAllText(It.IsAny<string>())).Returns(configJson);

        var config = ConfigService.LoadConfig();

        Assert.NotNull(config);
        Assert.Equal("/path/to/destination", config.DestinationPath);
    }

    [Fact]
    public void LoadConfig_ShouldCreateNewConfig_WhenConfigDoesNotExist()
    {
        _fileSystemMock.Setup(fs => fs.Exists(It.IsAny<string>())).Returns(false);

        var config = ConfigService.LoadConfig();

        _fileSystemMock.Verify(fs => fs.CreateFile(It.IsAny<string>()), Times.Once);
        _fileSystemMock.Verify(fs => fs.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        Assert.NotNull(config);
    }

    [Fact]
    public void SaveConfig_ShouldWriteToFile()
    {
        var config = new ConfigModel { DestinationPath = "/new/path" };

        ConfigService.SaveConfig(config);

        _fileSystemMock.Verify(fs => fs.WriteAllText(It.IsAny<string>(), It.Is<string>(json => json.Contains("/new/path"))), Times.Once);
    }
}