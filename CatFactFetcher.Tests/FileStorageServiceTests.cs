using Castle.Core.Logging;
using CatFactFetcher.ConsoleApp.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatFactFetcher.Tests
{
    public class FileStorageServiceTests
    {
        [Fact]
        public async Task AppendFactToFileAsync_SavesFactToDisk()
        {
            var loggerMock = new Mock<ILogger<FileStorageService>>();
            var service = new FileStorageService(loggerMock.Object);
            var testFact = "Testowa ciekawostka do zapisu na dysku.";
            var expectedFilePath = "cat_facts.txt";

            if (File.Exists(expectedFilePath))
            {
                File.Delete(expectedFilePath);
            }

            await service.AppendFactToFileAsync(testFact);

            Assert.True(File.Exists(expectedFilePath));
            var fileContent = await File.ReadAllTextAsync(expectedFilePath);
            Assert.Contains(testFact, fileContent);

            if (File.Exists(expectedFilePath))
            {
                File.Delete(expectedFilePath);
            }
        }
    }
}
