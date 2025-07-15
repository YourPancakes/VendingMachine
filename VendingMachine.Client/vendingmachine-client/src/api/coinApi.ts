import { Coin } from '../domain/coin';
import { request } from './http';

export const getCoins = async (): Promise<Coin[]> =>
  request<Coin[]>('/coins', 'GET');

export const updateCoinQuantity = async (id: number, quantity: number): Promise<Coin> =>
  request<Coin>(`/coins/${id}/quantity`, 'PUT', {
    data: JSON.stringify(quantity),
    headers: { 'Content-Type': 'application/json' }
  }); 