namespace FitLife.Livestream.Api.DTOs;

public record LivestreamRequest(
    string Title,
    string Instructor,
    string Category,
    DateTime StartTime,
    int DurationMinutes,
    string TeamsUrl,
    bool IsLive,
    int Participants
);