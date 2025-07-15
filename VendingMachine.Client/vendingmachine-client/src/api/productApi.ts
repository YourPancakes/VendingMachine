import { Product, ProductFilter } from '../domain/product';
import { request } from './http';

export const getProductsFilter = async (filter: ProductFilter): Promise<Product[]> =>
  request<Product[]>('/products/filter', 'GET', { params: filter });

export const updateProduct = async (product: Product): Promise<Product> =>
  request<Product>(`/products/${product.id}`, 'PUT', {
    data: JSON.stringify(product),
    headers: { 'Content-Type': 'application/json' }
  }); 