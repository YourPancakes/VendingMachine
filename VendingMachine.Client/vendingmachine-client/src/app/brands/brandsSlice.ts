import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Brand } from '../../domain/brand';

interface BrandsState {
  brands: Brand[];
  loading: boolean;
  error: string | null;
}

const initialState: BrandsState = {
  brands: [],
  loading: false,
  error: null,
};

const brandsSlice = createSlice({
  name: 'brands',
  initialState,
  reducers: {
    setBrands(state, action: PayloadAction<Brand[]>) {
      state.brands = action.payload;
    },
    setLoading(state, action: PayloadAction<boolean>) {
      state.loading = action.payload;
    },
    setError(state, action: PayloadAction<string | null>) {
      state.error = action.payload;
    },
  },
});

export const { setBrands, setLoading, setError } = brandsSlice.actions;
export default brandsSlice.reducer; 