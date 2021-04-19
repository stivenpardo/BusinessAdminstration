using BusinessAdministration.Infrastructure.Transversal;
using BusinessAdministration.Infrastructure.Transversal.Configurator;
using BusinessAdministration.Infrastructure.Transversal.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace BusinessAdministration.Test.Core._5.Infrastrucutre._5._2Transversal.HttpGenericBase
{
    public class HttpClientSettingsinBaseTest
    {
        [Fact]
        [UnitTest]
        public async Task Throw_Exception_When_Client_is_Null()
        {
            Assert.Throws<ClientNotEspecificateException>(() =>
            new HttpGenericBaseClient(null, null));
        }

        [Fact]
        [UnitTest]
        public async Task Throw_Exception_When_Client_is_invalid()
        {
            var service = new ServiceCollection();
            service.ConfigureHttpClientService(new HttpClientSettings());
            var provider = service.BuildServiceProvider();
            await Assert.ThrowsAsync<UriFormatException>(() =>
            Task.FromResult(provider.GetRequiredService<IHttpGenericBaseClient>()));
        }
    }
}
