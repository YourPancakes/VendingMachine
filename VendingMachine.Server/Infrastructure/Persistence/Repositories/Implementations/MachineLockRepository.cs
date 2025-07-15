using Microsoft.EntityFrameworkCore;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Infrastructure.Persistence.Repositories;

public class MachineLockRepository : IMachineLockRepository
{
    private readonly AppDbContext _context;

    public MachineLockRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MachineLock?> GetCurrentLockAsync()
    {
        return await _context.MachineLocks.FirstOrDefaultAsync();
    }

    public async Task<MachineLock> SetLockAsync(string lockedBy)
    {
        var existingLock = await GetCurrentLockAsync();
        
        if (existingLock != null)
        {
            existingLock.IsLocked = true;
            existingLock.LockedBy = lockedBy;
            existingLock.LockTime = DateTime.UtcNow;
            _context.MachineLocks.Update(existingLock);
        }
        else
        {
            existingLock = new MachineLock
            {
                IsLocked = true,
                LockedBy = lockedBy,
                LockTime = DateTime.UtcNow
            };
            _context.MachineLocks.Add(existingLock);
        }
        
        await _context.SaveChangesAsync();
        return existingLock;
    }

    public async Task<bool> ReleaseLockAsync(string lockedBy)
    {
        var currentLock = await GetCurrentLockAsync();
        
        if (currentLock == null || !currentLock.IsLocked || currentLock.LockedBy != lockedBy)
        {
            return false;
        }
        
        currentLock.IsLocked = false;
        currentLock.LockedBy = null;
        currentLock.LockTime = null;
        
        _context.MachineLocks.Update(currentLock);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> IsLockedAsync()
    {
        var currentLock = await GetCurrentLockAsync();
        return currentLock?.IsLocked ?? false;
    }
} 