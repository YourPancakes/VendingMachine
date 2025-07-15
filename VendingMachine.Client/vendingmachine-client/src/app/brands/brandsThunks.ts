import { createAsyncThunk } from '@reduxjs/toolkit';
import { getBrands } from '../../api/brandApi';
import { setBrands, setLoading, setError } from './brandsSlice';

export const fetchBrands = createAsyncThunk(
  'brands/fetchBrands',
  async (_, { dispatch }) => {
    dispatch(setLoading(true));
    try {
      const brands = await getBrands();
      dispatch(setBrands(brands));
      dispatch(setError(null));
    } catch (e) {
      dispatch(setError('Failed to load brands'));
    } finally {
      dispatch(setLoading(false));
    }
  }
); 