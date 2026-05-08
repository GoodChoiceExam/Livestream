using FitLife.Livestream.Api.DTOs;
using FitLife.Livestream.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitLife.Livestream.Api.Controllers;

[ApiController]
[Route("api/livestreams")]
[Authorize]
public class LivestreamsController : ControllerBase
{
    private readonly ILivestreamService _livestreamService;

    public LivestreamsController(ILivestreamService livestreamService)
    {
        _livestreamService = livestreamService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _livestreamService.GetAllAsync());
    }

    [HttpGet("live")]
    public async Task<IActionResult> GetLiveNow()
    {
        var stream = await _livestreamService.GetLiveNowAsync();
        if (stream is null)
            return NotFound();

        return Ok(stream);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var stream = await _livestreamService.GetByIdAsync(id);
        if (stream is null)
            return NotFound();

        return Ok(stream);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LivestreamRequest request)
    {
        var stream = await _livestreamService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = stream.Id }, stream);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _livestreamService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}