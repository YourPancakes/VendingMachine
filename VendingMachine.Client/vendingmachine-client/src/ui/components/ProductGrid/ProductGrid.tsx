import React from 'react';
import { Product } from '../../../domain/product';
import { ProductCard } from '../ProductCard/ProductCard';

type Props = {
  products: Product[];
  onSelect: (product: Product) => void;
  selectedProductIds: Set<number>;
  adminMode?: boolean;
  onQuantityChange?: (productId: number, quantity: number) => void;
};

export function ProductGrid({ products, onSelect, selectedProductIds, adminMode = false, onQuantityChange }: Props) {
  return (
    <div className="row g-4 mt-4">
      {products.map(product => (
        <div className="col-12 col-sm-6 col-lg-3" key={product.id}>
          <ProductCard
            product={product}
            onSelect={onSelect}
            selected={selectedProductIds.has(product.id)}
            adminMode={adminMode}
            onQuantityChange={onQuantityChange}
          />
        </div>
      ))}
    </div>
  );
} 