export interface CartItem {
  id: number;
  productId: number;
  productName: string;
  brandName: string;
  unitPrice: number;
  quantity: number;
  subtotal: number;
}

export interface Cart {
  id: number;
  items: CartItem[];
  total: number;
  totalItems: number;
  createdAt: string;
  updatedAt: string;
  isEmpty: boolean;
}

export interface AddToCart {
  productId: number;
  quantity: number;
}

export interface UpdateCartItem {
  cartItemId: number;
  quantity: number;
} 