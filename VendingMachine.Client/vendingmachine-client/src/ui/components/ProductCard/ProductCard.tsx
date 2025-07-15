import React from 'react';
import { Product } from '../../../domain/product';

type Props = {
  product: Product;
  onSelect: (product: Product) => void;
  selected: boolean;
  adminMode?: boolean;
  onQuantityChange?: (productId: number, quantity: number) => void;
};

export function ProductCard({ product, onSelect, selected, adminMode = false, onQuantityChange }: Props) {
  const outOfStock = product.quantity === 0;
  let btnClass = 'btn w-100 ';
  if (outOfStock) btnClass += 'btn-secondary';
  else if (selected) btnClass += 'btn-success';
  else btnClass += 'btn-primary';

  let imageSrc: string | null = null;
  if (product.name.includes('Coca-Cola')) {
    imageSrc = process.env.PUBLIC_URL + '/images/CocaCola.jpg';
  } else if (product.name.includes('Pepsi')) {
    imageSrc = process.env.PUBLIC_URL + '/images/Pepsi.jpg';
  }

  const handleQtyChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = Math.max(0, Number(e.target.value));
    onQuantityChange?.(product.id, value);
  };

  const handleWheel = (e: React.WheelEvent<HTMLInputElement>) => {
    e.currentTarget.blur();
  };

  return (
    <div className="border rounded-3 p-3 d-flex flex-column align-items-center bg-white">
      <div style={{ width: 120, height: 180, background: '#f0f0f0', borderRadius: 12, marginBottom: 12, overflow: 'hidden', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
        {imageSrc ? (
          <img src={imageSrc} alt={product.name} style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
        ) : null}
      </div>
      <div className="fs-5 fw-semibold mb-2 text-center">{product.name}</div>
      <div className="fs-6 text-secondary mb-3">{product.price}₽</div>
      {adminMode ? (
        <div className="w-100 mb-2">
          <label className="form-label mb-1" style={{ fontSize: '0.8rem' }}>Quantity</label>
          <input
            type="number"
            min={0}
            className="form-control form-control-sm text-center"
            value={product.quantity}
            onChange={handleQtyChange}
            onWheel={handleWheel}
          />
        </div>
      ) : null}
      <button
        className={btnClass}
        disabled={outOfStock}
        onClick={() => onSelect(product)}
      >
        {outOfStock ? 'Out of stock' : selected ? 'Selected' : 'Select'}
      </button>
    </div>
  );
} 