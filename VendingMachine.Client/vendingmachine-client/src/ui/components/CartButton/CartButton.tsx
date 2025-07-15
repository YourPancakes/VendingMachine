import React from 'react';
import './CartButton.css';

type Props = {
  count: number;
  onClick: () => void;
  disabled: boolean;
};

export function CartButton({ count, onClick, disabled }: Props) {
  return (
    <button className="cart-button" onClick={onClick} disabled={disabled}>
      Selected ({count})
    </button>
  );
} 