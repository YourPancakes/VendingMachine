import React from 'react';
import { CartItem as CartItemType } from '../../../domain/cart';

type Props = {
  item: CartItemType;
  maxQuantity: number;
  onQuantityChange: (quantity: number) => void;
  onRemove: () => void;
};

export function CartItem({ item, maxQuantity, onQuantityChange, onRemove }: Props) {
  const handleInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    let value = Number(e.target.value);
    if (value < 0) value = 0;
    if (value > maxQuantity) value = maxQuantity;
    onQuantityChange(value);
  };
  const price = (item.unitPrice * item.quantity).toFixed(2);
  return (
    <div className="d-flex align-items-center gap-3 py-2 border-bottom">
      <div className="flex-grow-1 fs-6">{item.productName}</div>
      <div className="d-flex align-items-center gap-2">
        <button className="btn btn-outline-secondary btn-sm" onClick={() => onQuantityChange(Math.max(0, item.quantity - 1))} disabled={item.quantity <= 0}>-</button>
        <input type="number" min={0} max={maxQuantity} value={item.quantity} onChange={handleInput} className="form-control form-control-sm text-center" style={{ width: 80 }} />
        <button className="btn btn-outline-secondary btn-sm" onClick={() => onQuantityChange(Math.min(maxQuantity, item.quantity + 1))} disabled={item.quantity >= maxQuantity}>+</button>
      </div>
      <div className="flex-shrink-0 fs-6 text-end" style={{ width: 80 }}>{price}₽</div>
      <button className="btn btn-link text-danger fs-4 ms-2 p-0" onClick={onRemove}>&times;</button>
    </div>
  );
} 