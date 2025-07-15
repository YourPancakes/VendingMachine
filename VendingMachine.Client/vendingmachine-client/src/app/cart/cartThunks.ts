import { createAsyncThunk } from '@reduxjs/toolkit';
import { getCart, addToCart, updateCart, removeCartItem as removeCartItemApi } from '../../api/cartApi';
import { setCart, setLoading, setError } from './cartSlice';
import { AddToCart, UpdateCartItem } from '../../domain/cart';

export const fetchCart = createAsyncThunk(
  'cart/fetchCart',
  async (_, { dispatch }) => {
    dispatch(setLoading(true));
    try {
      const cart = await getCart();
      dispatch(setCart(cart));
      dispatch(setError(null));
    } catch (e) {
      dispatch(setError('Failed to load cart'));
    } finally {
      dispatch(setLoading(false));
    }
  }
);

export const addToCartThunk = createAsyncThunk(
  'cart/addToCart',
  async (payload: AddToCart, { dispatch }) => {
    dispatch(setLoading(true));
    try {
      const cart = await addToCart(payload);
      dispatch(setCart(cart));
      dispatch(setError(null));
    } catch (e) {
      dispatch(setError('Failed to add to cart'));
    } finally {
      dispatch(setLoading(false));
    }
  }
);

export const updateCartThunk = createAsyncThunk(
  'cart/updateCart',
  async (payload: UpdateCartItem, { dispatch }) => {
    dispatch(setLoading(true));
    try {
      const cart = await updateCart(payload);
      dispatch(setCart(cart));
      dispatch(setError(null));
    } catch (e) {
      dispatch(setError('Failed to update cart'));
    } finally {
      dispatch(setLoading(false));
    }
  }
);

export const removeCartItemThunk = createAsyncThunk(
  'cart/removeCartItem',
  async (cartItemId: number, { dispatch }) => {
    dispatch(setLoading(true));
    try {
      const cart = await removeCartItemApi(cartItemId);
      dispatch(setCart(cart));
      dispatch(setError(null));
    } catch (e) {
      dispatch(setError('Failed to remove cart item'));
    } finally {
      dispatch(setLoading(false));
    }
  }
); 