using Castle.Core.Logging;
using CatFactFetcher.ConsoleApp.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatFactFetcher.Tests
{
    public class CatFactServiceTests
    {
        [Fact]
        public async Task GetRandomCatFactAsync_ReturnsFact_WhenApiCallIsSuccessful()
        {
            var expectedJson = "{\"fact\":\"Testowa ciekawostka mock\",\"length\":24}";

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(expectedJson)
                });

            var httpClient = new HttpClient(handlerMock.Object);
            var loggerMock = new Mock<ILogger<CatFactService>>();

            var service = new CatFactService(httpClient, loggerMock.Object);

            var result = await service.GetRandomCatFactAsync();

            Assert.NotNull(result);
            Assert.Equal("Testowa ciekawostka mock", result);
        }
    }
}
