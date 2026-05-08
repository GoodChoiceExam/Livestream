using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitLife.Livestream.Api.Models;

public class LivestreamSession
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Instructor { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public string TeamsUrl { get; set; } = string.Empty;
    public bool IsLive { get; set; }
    public int Participants { get; set; }
}