using Microsoft.Extensions.Caching.Memory;

public class BestStoriesBackgroundService(IConfiguration config, BestStoriesProvider bestStoriesProvider, IMemoryCache cache, ILogger<BestStoriesBackgroundService> logger) : BackgroundService
{
    private readonly TimeSpan CacheEntryMaxLifetime = config.GetValue<TimeSpan>("CacheEntryMaxLifetime", TimeSpan.FromMinutes(1));
    private readonly TimeSpan CacheRefreshDelay = config.GetValue<TimeSpan>("CacheRefreshDelay", TimeSpan.FromSeconds(10));

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                var bestStories = await bestStoriesProvider.GetBestStoriesAsync(ct);
                cache.Set("BestStories", bestStories, CacheEntryMaxLifetime);
                logger.LogDebug($"Refresh completed {CacheRefreshDelay}");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching best stories");
            }

            await Task.Delay(CacheRefreshDelay, ct);
        }
    }
}
