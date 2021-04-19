using BusinessAdministration.Infrastructure.Transversal;
using BusinessAdministration.Infrastructure.Transversal.Configurator;
using BusinessAdministration.Infrastructure.Transversal.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._5.Infrastrucutre._5._2Transversal.HttpGenericBase
{

    public class TestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class GetTest
    {
        [Fact]
        [UnitTest]
        public async Task Get_Throw_Exception_When_Uri_is_Empty_or_null()
        {
            var service = new ServiceCollection();
            service.ConfigureHttpClientService(new HttpClientSettings()
            {
                ServiceProtocol = "http",
                Context = "AnyContext",
                Hostname = "AnyName",
                Port = 2345
            });
            var provider = service.BuildServiceProvider();
            var httpGenericBaseClient = provider.GetRequiredService<IHttpGenericBaseClient>();
            await Assert.ThrowsAnyAsync<UriIsNullOrEmptyException>(() =>
                httpGenericBaseClient.Get<TestDto>("")).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Get_Throw_UserUnauthorizedException_When_statusCode_is_Unauthorized()
        {
            var HandleMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent(string.Empty),
            };
            HandleMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.IsAny<HttpRequestMessage>(),
                 ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClient = new HttpClient(HandleMock.Object);
            var service = new ServiceCollection();

            service.AddTransient(_ => httpClient);
            service.ConfigureHttpClientService(new HttpClientSettings()
            {
                ServiceProtocol = "http",
                Context = "AnyContext",
                Hostname = "AnyName",
                Port = 2345
            });
            var provider = service.BuildServiceProvider();
            var serviceClienteGenerico = provider.GetRequiredService<IHttpGenericBaseClient>();

            _ = await Assert.ThrowsAsync<UserUnauthorizedException>(() =>
                 serviceClienteGenerico.Get<TestDto>("anyUri")).ConfigureAwait(false);
        }

        [Fact]
        [UnitTest]
        public async Task Get_Successfull()
        {
            var HandleMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{ ""Id"": 1, ""Title"": ""Cool post!""}"),
            };
            HandleMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.IsAny<HttpRequestMessage>(),
                 ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClient = new HttpClient(HandleMock.Object);
            var service = new ServiceCollection();

            service.AddTransient(_ => httpClient);
            service.ConfigureHttpClientService(new HttpClientSettings()
            {
                ServiceProtocol = "http",
                Context = "AnyContext",
                Hostname = "AnyName",
                Port = 2345
            });

            var provider = service.BuildServiceProvider();
            var serviceClientGeneric = provider.GetRequiredService<IHttpGenericBaseClient>();

            var response2 = await serviceClientGeneric.Get<TestDto>("AnyURl").ConfigureAwait(false);
        }
    }
}
