public class BestStoriesProvider(IConfiguration config, HttpClient httpClient)
{
    private readonly string baseUrl = config.GetValue("HackerNewApiUrl", "https://hacker-news.firebaseio.com/v0")!;

    public async Task<List<HackerNewsStory>> GetBestStoriesAsync(CancellationToken stoppingToken = default)
    {
        var bestStoriesIds = await httpClient.GetFromJsonAsync<int[]>($"{baseUrl}/beststories.json", stoppingToken) ?? [];

        var tasks = bestStoriesIds.Select(id => httpClient.GetFromJsonAsync<HackerNewsStory>($"{baseUrl}/item/{id}.json", stoppingToken));

        var bestStories = await Task.WhenAll(tasks);
        return bestStories.Where(x => x != null).OrderByDescending(x => x!.Score).ToList()!;
    }
}
