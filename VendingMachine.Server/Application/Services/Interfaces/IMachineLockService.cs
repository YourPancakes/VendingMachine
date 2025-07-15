using FluentResults;
using VendingMachine.Application.DTOs.MachineLock;

namespace VendingMachine.Application.Services;

public interface IMachineLockService
{
    Task<Result<MachineLockDto>> GetLockStatusAsync();
    Task<Result<MachineLockDto>> SetLockAsync(string lockedBy);
    Task<Result<bool>> ReleaseLockAsync(string lockedBy);
    Task<Result<bool>> IsLockedAsync();
} 