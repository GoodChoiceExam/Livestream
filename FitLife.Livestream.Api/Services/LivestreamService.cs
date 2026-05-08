using FitLife.Livestream.Api.DTOs;
using FitLife.Livestream.Api.Models;
using MongoDB.Driver;

namespace FitLife.Livestream.Api.Services;

public class LivestreamService : ILivestreamService
{
    private readonly IMongoCollection<LivestreamSession> _streams;

    public LivestreamService(IConfiguration configuration)
    {
        var mongoConn = configuration["MongoDB:ConnectionString"]!;
        var mongoDb = configuration["MongoDB:DatabaseName"]!;
        var collectionName = configuration["MongoDB:CollectionName"] ?? "livestreams";

        var client = new MongoClient(mongoConn);
        var database = client.GetDatabase(mongoDb);
        _streams = database.GetCollection<LivestreamSession>(collectionName);
    }

    public async Task<List<LivestreamSession>> GetAllAsync()
    {
        return await _streams.Find(_ => true).SortBy(s => s.StartTime).ToListAsync();
    }

    public async Task<LivestreamSession?> GetLiveNowAsync()
    {
        return await _streams.Find(s => s.IsLive).FirstOrDefaultAsync();
    }

    public async Task<LivestreamSession?> GetByIdAsync(Guid id)
    {
        return await _streams.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<LivestreamSession> CreateAsync(LivestreamRequest request)
    {
        var stream = new LivestreamSession
        {
            Title = request.Title,
            Instructor = request.Instructor,
            Category = request.Category,
            StartTime = request.StartTime,
            DurationMinutes = request.DurationMinutes,
            TeamsUrl = request.TeamsUrl,
            IsLive = request.IsLive,
            Participants = request.Participants
        };

        await _streams.InsertOneAsync(stream);
        return stream;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _streams.DeleteOneAsync(s => s.Id == id);
        return result.DeletedCount > 0;
    }
}
