using FitLife.Livestream.Api.Models;
using NUnit.Framework;

namespace FitLife.Livestream.Tests;

[TestFixture]
public class LivestreamServiceTests
{
    [Test]
    public void Livestream_DefaultValues_AreCorrect()
    {
        var stream = new LivestreamSession();

        Assert.That(stream.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(stream.Title, Is.EqualTo(string.Empty));
        Assert.That(stream.IsLive, Is.False);
        Assert.That(stream.Participants, Is.EqualTo(0));
    }

    [Test]
    public void Livestream_SetProperties_ReturnsCorrectValues()
    {
        var stream = new LivestreamSession
        {
            Title = "Morning HIIT",
            Instructor = "Marie Jensen",
            Category = "HIIT",
            DurationMinutes = 30,
            IsLive = true,
            Participants = 47
        };

        Assert.That(stream.Title, Is.EqualTo("Morning HIIT"));
        Assert.That(stream.Instructor, Is.EqualTo("Marie Jensen"));
        Assert.That(stream.IsLive, Is.True);
        Assert.That(stream.Participants, Is.EqualTo(47));
    }

    [Test]
    public void Livestream_IsLive_CanBeToggled()
    {
        var stream = new LivestreamSession { IsLive = true };
        stream.IsLive = false;

        Assert.That(stream.IsLive, Is.False);
    }

    [Test]
    public void Livestream_UniqueIds_AreGenerated()
    {
        var stream1 = new LivestreamSession();
        var stream2 = new LivestreamSession();

        Assert.That(stream1.Id, Is.Not.EqualTo(stream2.Id));
    }

    [Test]
    public void Livestream_TeamsUrl_CanBeSet()
    {
        var url = "https://teams.microsoft.com/l/meetup-join/123";
        var stream = new LivestreamSession { TeamsUrl = url };

        Assert.That(stream.TeamsUrl, Is.EqualTo(url));
    }
}