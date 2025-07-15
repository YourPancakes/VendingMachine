using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

public interface IMachineLockRepository
{
    Task<MachineLock?> GetCurrentLockAsync();
    Task<MachineLock> SetLockAsync(string lockedBy);
    Task<bool> ReleaseLockAsync(string lockedBy);
    Task<bool> IsLockedAsync();
} 