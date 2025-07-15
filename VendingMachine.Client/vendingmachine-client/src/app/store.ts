import { configureStore } from '@reduxjs/toolkit';
import productsReducer from './products/productsSlice';
import brandsReducer from './brands/brandsSlice';
import filtersReducer from './filters/filtersSlice';
import cartReducer from './cart/cartSlice';

export const store = configureStore({
  reducer: {
    products: productsReducer,
    brands: brandsReducer,
    filters: filtersReducer,
    cart: cartReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch; 