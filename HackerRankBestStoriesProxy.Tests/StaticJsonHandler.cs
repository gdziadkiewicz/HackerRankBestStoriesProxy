using System.Net;
using System.Text;

namespace HackerRankBestStoriesProxy.Tests;

public class StaticJsonHandler(Dictionary<string, string> uriToJsonMap) : HttpMessageHandler
{
    private readonly Dictionary<string, string> UriToJsonMap = uriToJsonMap;

    sealed protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var uri = request.RequestUri;
        var json = UriToJsonMap[uri.ToString()];
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };

        return Task.FromResult(response);
    }
}
