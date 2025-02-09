
public static class ResponseFormater
{
    public record FormatedHackerNewsStory(string title, string? uri, string postedBy, string time, int score, int commentCount);

    // Like "o" ISO 8601, but without the fracions of a second eg. 2019-10-12T13:43:01+00:00
    const string DATE_FORMAT_STRING = "yyyy-MM-ddTHH:mm:ssK";

    public static string FormatDate(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).ToString(DATE_FORMAT_STRING);
    }

    public static FormatedHackerNewsStory FormatStory(HackerNewsStory story)
    {
        return new FormatedHackerNewsStory(story.Title, story.Url, story.By, FormatDate(story.Time), story.Score, story.Descendants);
    }
}
