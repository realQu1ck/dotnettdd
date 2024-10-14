using Moq.Protected;
using Newtonsoft.Json;

namespace DotnetTDDTest.Test.Helpers;

internal static class MockHttpMessageHandler<T>
{
    internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> exceptedResponse)
    {
        var mockRsponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(exceptedResponse))
        };

        mockRsponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>
            ("SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockRsponse);

        return mockHandler;
    }

    internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<User> expectedResult, string endpoint)
    {
        var mockRsponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(expectedResult))
        };
        mockRsponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var httpRequstMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri(endpoint),
            Method = HttpMethod.Get,
        }; 

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>
            ("SendAsync",
            httpRequstMessage,
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockRsponse);

        return mockHandler;
    }

    internal static Mock<HttpMessageHandler> SetupReturn404()
    {
        var mockRsponse = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
        {
            Content = new StringContent("")
        };

        mockRsponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>
            ("SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockRsponse);

        return mockHandler;
    }
}
