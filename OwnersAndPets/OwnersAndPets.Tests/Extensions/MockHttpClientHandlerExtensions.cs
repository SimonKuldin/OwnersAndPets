using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OwnersAndPets.Tests.Extensions
{
    public static class MockHttpClientHandlerExtensions
    {
        public static void SetupGetStringAsync(this Mock<HttpClientHandler> mockHandler, Uri requestUri, string response)
        {
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(response) }));
        }
    }
}
