import { RootState } from '../store';

export const selectCart = (state: RootState) => state.cart.cart;
export const selectCartTotalItems = (state: RootState) => state.cart.cart?.totalItems || 0; 