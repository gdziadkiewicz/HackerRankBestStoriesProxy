public class HackerNewsStory
{
    public required string Title { get; set; }

    // Optionall, see eg. https://hacker-news.firebaseio.com/v0/item/42919502.json
    public string? Url { get; set; }

    public required string By { get; set; }
    
    public required long Time { get; set; }

    public required int Score { get; set; }

    public required int Descendants { get; set; }
}
