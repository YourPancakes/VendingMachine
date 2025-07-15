namespace VendingMachine.Domain.Entities;

public class MachineLock
{
    public int Id { get; set; }
    public bool IsLocked { get; set; }
    public string? LockedBy { get; set; }
    public DateTime? LockTime { get; set; }
} 