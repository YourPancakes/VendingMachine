export interface MachineLock {
  id: number;
  isLocked: boolean;
  lockedBy?: string;
  lockTime?: string;
} 