using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.DTOs;
using VendingMachine.Application.Services;

namespace Api.Controllers;

[ApiController]
[Route("api/v1.0/[controller]")]
[Produces("application/json")]
public class MachineLockController : ControllerBase
{
    private readonly IMachineLockService _machineLockService;

    public MachineLockController(IMachineLockService machineLockService)
    {
        _machineLockService = machineLockService;
    }

    [HttpGet("status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStatus()
    {
        var result = await _machineLockService.GetLockStatusAsync();
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPost("lock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetLock([FromBody] string lockedBy)
    {
        var result = await _machineLockService.SetLockAsync(lockedBy);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Value);
    }

    [HttpPost("release")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReleaseLock([FromBody] string lockedBy)
    {
        var result = await _machineLockService.ReleaseLockAsync(lockedBy);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(new { released = result.Value });
    }


} 