using Microsoft.Extensions.Configuration;

namespace HackerRankBestStoriesProxy.Tests;

public class BestStoriesProviderTests
{
    [Test]
    public void GetBestStories_ValidResponses_ReturnsBestStories()
    {
        // Arrange
        string json1 = @"{
            ""title"": ""Test Story1"",
            ""by"": ""testuser"",
            ""time"": 1570881781,
            ""score"": 100,
            ""descendants"": 50
        }";
        string json2 = @"{
            ""title"": ""Test Story2"",
            ""by"": ""testuser"",
            ""time"": 1570881781,
            ""score"": 200,
            ""descendants"": 50
        }";
        string json3 = @"{
            ""title"": ""Test Story3"",
            ""by"": ""testuser2"",
            ""time"": 1570881781,
            ""score"": 100,
            ""descendants"": 60
        }";
        var uriToJsonMap = new Dictionary<string, string>()
        {
            ["https://hacker-news.firebaseio.com/v0/beststories.json"] = @"[1, 2, 3]",
            ["https://hacker-news.firebaseio.com/v0/item/1.json"] = json1,
            ["https://hacker-news.firebaseio.com/v0/item/2.json"] = json2,
            ["https://hacker-news.firebaseio.com/v0/item/3.json"] = json3,
        };
        var handler = new StaticJsonHandler(uriToJsonMap);
        var client = new HttpClient(handler);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(initialData: new Dictionary<string, string?> {
                {"HackerNewApiUrl", "https://hacker-news.firebaseio.com/v0"},
            }).Build();
        var provider = new BestStoriesProvider(configuration, client);

        // Act
        var bestStories = provider.GetBestStoriesAsync().Result;

        // Assert
        Assert.That(bestStories, Is.Not.Null);
        Assert.That(bestStories.Count, Is.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(bestStories[0].Title, Is.EqualTo("Test Story2"));
            Assert.That(bestStories[0].Url, Is.Null);
            Assert.That(bestStories[0].By, Is.EqualTo("testuser"));
            Assert.That(bestStories[0].Time, Is.EqualTo(1570881781));
            Assert.That(bestStories[0].Score, Is.EqualTo(200));
            Assert.That(bestStories[0].Descendants, Is.EqualTo(50));
            
            Assert.That(bestStories[1].Title, Is.EqualTo("Test Story1"));
            Assert.That(bestStories[1].Url, Is.Null);
            Assert.That(bestStories[1].By, Is.EqualTo("testuser"));
            Assert.That(bestStories[1].Time, Is.EqualTo(1570881781));
            Assert.That(bestStories[1].Score, Is.EqualTo(100));
            Assert.That(bestStories[1].Descendants, Is.EqualTo(50));
            
            Assert.That(bestStories[2].Title, Is.EqualTo("Test Story3"));
            Assert.That(bestStories[2].Url, Is.Null);
            Assert.That(bestStories[2].By, Is.EqualTo("testuser2"));
            Assert.That(bestStories[2].Time, Is.EqualTo(1570881781));
            Assert.That(bestStories[2].Score, Is.EqualTo(100));
            Assert.That(bestStories[2].Descendants, Is.EqualTo(60));
        });
    }
}