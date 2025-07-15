import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { FilterState } from '../../domain/filter';

const initialState: FilterState = {
  brandId: null,
  minPrice: 0,
  maxPrice: 0,
};

const filtersSlice = createSlice({
  name: 'filters',
  initialState,
  reducers: {
    setBrandId(state, action: PayloadAction<number | null>) {
      state.brandId = action.payload;
    },
    setMinPrice(state, action: PayloadAction<number>) {
      state.minPrice = action.payload;
    },
    setMaxPrice(state, action: PayloadAction<number>) {
      state.maxPrice = action.payload;
    },
    setAll(state, action: PayloadAction<FilterState>) {
      state.brandId = action.payload.brandId;
      state.minPrice = action.payload.minPrice;
      state.maxPrice = action.payload.maxPrice;
    },
  },
});

export const { setBrandId, setMinPrice, setMaxPrice, setAll } = filtersSlice.actions;
export const selectFilters = (state: any) => state.filters;
export default filtersSlice.reducer; 