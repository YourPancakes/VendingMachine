import { RootState } from '../store';

export const selectBrands = (state: RootState) => state.brands.brands;