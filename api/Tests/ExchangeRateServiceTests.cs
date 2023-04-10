using Lib;
using Lib.Models;
using Moq;
using System.Net;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using Moq.Protected;

namespace Tests
{
    public class ExchangeRateServiceTests
    {
        [Fact]
        public async Task GetDayRangeForRate_ReturnsExchangeRatesForSpecifiedTimeRange()
        {
            // Arrange
            var startDate = new DateTime(2022, 1, 1);
            var endDate = new DateTime(2022, 1, 3);
            var expectedRates = new Dictionary<string, decimal>
            {
                { "2022-01-01", 0.2533m },
                { "2022-01-02", 0.2433m },
                { "2022-01-03", 0.2333m }
            };
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var dto = new TimeseriesServiceResponse
            {
                Rates = new Dictionary<string, Dictionary<string, decimal>> {
                    { "2022-01-01", new Dictionary<string, decimal> { { "USD", 0.2533m } } },
                    { "2022-01-02", new Dictionary<string, decimal> { { "USD", 0.2433m } } },
                    { "2022-01-03", new Dictionary<string, decimal> { { "USD", 0.2333m } } }
                }
            };
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(dto)
            };
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);
            var httpClient = new HttpClient(mockHandler.Object) { BaseAddress = new Uri("http://test.test")};
            var sut = new ExchangeRateService(httpClient);

            // Act
            var actualRates = await sut.GetDayRangeForRate(startDate, endDate, Constants.USD_ILS);

            // Assert
            Assert.Equal(expectedRates, actualRates);
        }

        [Theory]
        [InlineData("2022-01-03", "2022-01-01")]
        [InlineData("2022-01-02", "2022-01-01")]
        [InlineData("2023-01-02", "2022-01-02")]
        public async Task GetDayRangeForRate_ThrowsExceptionWhenStartDateIsAfterEndDate(string startDateString, string endDateString)
        {
            // Arrange
            var startDate = DateTime.Parse(startDateString);
            var endDate = DateTime.Parse(endDateString);
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var httpClient = new HttpClient(mockHandler.Object);
            var sut = new ExchangeRateService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetDayRangeForRate(startDate, endDate, Constants.USD_ILS));
        }
    }

}