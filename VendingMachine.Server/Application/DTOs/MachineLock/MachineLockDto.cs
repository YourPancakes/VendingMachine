namespace VendingMachine.Application.DTOs.MachineLock;

public record MachineLockDto(int Id, bool IsLocked, string? LockedBy, DateTime? LockTime); 