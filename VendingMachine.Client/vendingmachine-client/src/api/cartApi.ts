import { Cart, AddToCart, UpdateCartItem } from '../domain/cart';
import { request } from './http';

export interface PurchaseResult {
  success: boolean;
  message: string;
  totalPaid: number;
  changeAmount: number;
  changeCoins: Array<{ denomination: number; quantity: number; totalValue: number }>;
  purchasedItems: Array<any>;
  purchaseTime: string;
}

export const getCart = async (): Promise<Cart> =>
  request<Cart>('/cart', 'GET');

export const addToCart = async (payload: AddToCart): Promise<Cart> =>
  request<Cart>('/cart/add', 'POST', { data: payload });

export const updateCart = async (payload: UpdateCartItem): Promise<Cart> =>
  request<Cart>('/cart/update', 'PUT', { data: payload });

export const removeCartItem = async (cartItemId: number): Promise<Cart> =>
  request<Cart>(`/cart/remove/${cartItemId}`, 'DELETE');

export const purchaseCart = async (): Promise<PurchaseResult> =>
  request<PurchaseResult>('/cart/purchase', 'POST'); 