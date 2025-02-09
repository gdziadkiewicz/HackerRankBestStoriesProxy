using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<HttpClient>(_ =>
{
    var handler = new SocketsHttpHandler()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(2) // Make HttpClient safe to be singleton
    };
    return new HttpClient(handler);
});
builder.Services.AddSingleton<BestStoriesProvider>();
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<BestStoriesBackgroundService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGet("/beststories/{n:int}", (IMemoryCache memoryCache, int n) =>
{
    var sortedBestStories = memoryCache.Get<List<HackerNewsStory>?>("BestStories");
    if (sortedBestStories == null)
    {
        return Results.NotFound("Best stories not available.");
    }

    return Results.Ok(sortedBestStories.Take(n).Select(ResponseFormater.FormatStory));
});

app.Run();