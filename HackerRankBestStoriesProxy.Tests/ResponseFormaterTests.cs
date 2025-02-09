namespace HackerRankBestStoriesProxy.Tests;

public class ResponseFormaterTests
{
    [Test]
    public void FormatDate_ValidUnixTime_ReturnsFormattedDate()
    {
        // Arrange
        long unixTime = 1570881781; 
        string expectedDate = "2019-10-12T12:03:01+00:00";

        // Act
        string formattedDate = ResponseFormater.FormatDate(unixTime);

        // Assert
        Assert.That(formattedDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void FormatStory_ValidStory_ReturnsFormattedStory()
    {
        // Arrange
        var story = new HackerNewsStory
        {
            Title = "Test Story",
            Url = "http://example.com",
            By = "testuser",
            Time = 1570881781,
            Score = 100,
            Descendants = 50
        };

        var expectedFormattedStory = new
        {
            title = "Test Story",
            uri = "http://example.com",
            postedBy = "testuser",
            time = "2019-10-12T12:03:01+00:00",
            score = 100,
            commentCount = 50
        };

        // Act
        var formattedStory = ResponseFormater.FormatStory(story);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(formattedStory.title, Is.EqualTo(expectedFormattedStory.title));
            Assert.That(formattedStory.uri, Is.EqualTo(expectedFormattedStory.uri));
            Assert.That(formattedStory.postedBy, Is.EqualTo(expectedFormattedStory.postedBy));
            Assert.That(formattedStory.time, Is.EqualTo(expectedFormattedStory.time));
            Assert.That(formattedStory.score, Is.EqualTo(expectedFormattedStory.score));
            Assert.That(formattedStory.commentCount, Is.EqualTo(expectedFormattedStory.commentCount));
        });
       
    }
}