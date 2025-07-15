using AutoMapper;
using FluentResults;
using VendingMachine.Application.DTOs.MachineLock;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Application.Services;

public class MachineLockService : IMachineLockService
{
    private readonly IMachineLockRepository _machineLockRepository;
    private readonly IMapper _mapper;

    public MachineLockService(IMachineLockRepository machineLockRepository, IMapper mapper)
    {
        _machineLockRepository = machineLockRepository;
        _mapper = mapper;
    }

    public async Task<Result<MachineLockDto>> GetLockStatusAsync()
    {
        var currentLock = await _machineLockRepository.GetCurrentLockAsync();
        var lockDto = _mapper.Map<MachineLockDto>(currentLock);
        return Result.Ok(lockDto);
    }

    public async Task<Result<MachineLockDto>> SetLockAsync(string lockedBy)
    {
        if (string.IsNullOrWhiteSpace(lockedBy))
        {
            return Result.Fail<MachineLockDto>("LockedBy cannot be empty");
        }

        var isLocked = await _machineLockRepository.IsLockedAsync();
        if (isLocked)
        {
            return Result.Fail<MachineLockDto>("Machine is already locked");
        }

        var lockEntity = await _machineLockRepository.SetLockAsync(lockedBy);
        var lockDto = _mapper.Map<MachineLockDto>(lockEntity);
        return Result.Ok(lockDto);
    }

    public async Task<Result<bool>> ReleaseLockAsync(string lockedBy)
    {
        if (string.IsNullOrWhiteSpace(lockedBy))
        {
            return Result.Fail<bool>("LockedBy cannot be empty");
        }

        var released = await _machineLockRepository.ReleaseLockAsync(lockedBy);
        return Result.Ok(released);
    }

    public async Task<Result<bool>> IsLockedAsync()
    {
        var isLocked = await _machineLockRepository.IsLockedAsync();
        return Result.Ok(isLocked);
    }
} 