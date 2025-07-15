import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { selectCart } from '../../../app/cart/cartSelectors';
import { selectProducts } from '../../../app/products/productsSelectors';
import { CartItem } from '../../components/CartItem/CartItem';
import { updateCartThunk, removeCartItemThunk } from '../../../app/cart/cartThunks';
import { AppDispatch } from '../../../app/store';

export function CheckoutPage() {
  const cart = useSelector(selectCart);
  const products = useSelector(selectProducts);
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();

  useEffect(() => {
    if (!cart || cart.items.length === 0) {
      navigate('/');
    }
  }, [cart, navigate]);

  const handleQuantityChange = (cartItemId: number, quantity: number) => {
    dispatch(updateCartThunk({ cartItemId, quantity }));
  };

  const handleRemove = (cartItemId: number) => {
    dispatch(removeCartItemThunk(cartItemId));
  };

  const handleBack = () => {
    navigate('/');
  };

  const handlePay = () => {
    navigate('/payment');
  };

  if (!cart || cart.items.length === 0) {
    return null;
  }

  return (
    <div className="container py-5">
      <h1 className="mb-4">Checkout</h1>
      <div className="mb-2 d-flex align-items-center gap-3 fw-bold border-bottom pb-2" style={{fontSize: '1rem'}}>
        <div className="flex-grow-1">Product</div>
        <div style={{width: 160, textAlign: 'center'}}>Quantity</div>
        <div className="flex-shrink-0 text-end" style={{width: 80}}>Price</div>
        <div style={{width: 32}}></div>
      </div>
      <div className="mb-4">
        {cart.items.slice().sort((a, b) => a.id - b.id).map(item => {
          const product = products.find((p: { id: number }) => p.id === item.productId);
          const maxQuantity = product ? product.quantity : item.quantity;
          return (
            <CartItem
              key={item.id}
              item={item}
              maxQuantity={maxQuantity}
              onQuantityChange={q => handleQuantityChange(item.id, q)}
              onRemove={() => handleRemove(item.id)}
            />
          );
        })}
      </div>
      <div className="fs-4 fw-semibold text-center mb-4">Total: {cart.total}₽</div>
      <div className="d-flex justify-content-between align-items-center mt-4">
        <button className="btn btn-secondary checkout-back" onClick={handleBack}>Back</button>
        <button className="btn btn-primary checkout-pay" onClick={handlePay}>Pay</button>
      </div>
    </div>
  );
} 