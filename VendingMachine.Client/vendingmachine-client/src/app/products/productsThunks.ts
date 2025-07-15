import { createAsyncThunk } from '@reduxjs/toolkit';
import { getProductsFilter } from '../../api/productApi';
import { setProducts, setLoading, setError } from './productsSlice';
import { ProductFilter } from '../../domain/product';

export const fetchProducts = createAsyncThunk(
  'products/fetchProducts',
  async (filter: ProductFilter, { dispatch }) => {
    dispatch(setLoading(true));
    try {
      const products = await getProductsFilter(filter);
      dispatch(setProducts(products));
      dispatch(setError(null));
    } catch (e) {
      dispatch(setError('Failed to load products'));
    } finally {
      dispatch(setLoading(false));
    }
  }
); 