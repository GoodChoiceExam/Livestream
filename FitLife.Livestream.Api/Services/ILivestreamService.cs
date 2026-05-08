using FitLife.Livestream.Api.DTOs;
using FitLife.Livestream.Api.Models;

namespace FitLife.Livestream.Api.Services;

public interface ILivestreamService
{
    Task<List<LivestreamSession>> GetAllAsync();
    Task<LivestreamSession?> GetLiveNowAsync();
    Task<LivestreamSession?> GetByIdAsync(Guid id);
    Task<LivestreamSession> CreateAsync(LivestreamRequest request);
    Task<bool> DeleteAsync(Guid id);
}