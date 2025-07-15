export interface Product {
  id: number;
  name: string;
  brandId: number;
  brandName: string;
  price: number;
  quantity: number;
}

export interface ProductFilter {
  brandId?: number;
  minPrice?: number;
  maxPrice?: number;
} 