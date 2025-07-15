import { MachineLock } from '../domain/machineLock';
import { request } from './http';

export const getMachineLockStatus = async (): Promise<MachineLock> =>
  request<MachineLock>('/machinelock/status', 'GET');

export const lockMachine = async (userName: string): Promise<void> =>
  request<void>('/machinelock/lock', 'POST', { data: JSON.stringify(userName), headers: { 'Content-Type': 'application/json' } });