import React, { useState, useEffect } from 'react';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { selectCart } from '../../../app/cart/cartSelectors';
import { CoinInput } from '../../components/CoinInput/CoinInput';
import { purchaseCart } from '../../../api/cartApi';
import { getCoins, updateCoinQuantity } from '../../../api/coinApi';
import { Coin } from '../../../domain/coin';

const MAX_COIN = 100;

export function PaymentPage() {
  const cart = useSelector(selectCart);
  const navigate = useNavigate();
  const [coins, setCoins] = useState<{ [id: number]: number }>({});
  const [coinList, setCoinList] = useState<Coin[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getCoins().then(list => {
      setCoinList(list);
      const initial: { [id: number]: number } = {};
      list.forEach(c => { initial[c.id] = c.quantity; });
      setCoins(initial);
    });
  }, []);

  if (!cart) {
    navigate('/');
    return null;
  }

  const total = cart.total;
  const inserted = coinList.reduce((sum, c) => sum + Number(c.denomination) * (coins[c.id] || 0), 0);
  const enough = inserted >= total;

  const handleCoinChange = async (id: number, value: number) => {
    setError(null);
    try {
      const updatedCoin = await updateCoinQuantity(id, value);
      setCoins(prev => ({ ...prev, [id]: updatedCoin.quantity }));
    } catch (e: any) {
      setError(e.message || 'Failed to update coin quantity');
    }
  };

  const handleBack = () => {
    navigate('/checkout');
  };

  const handlePay = async () => {
    setLoading(true);
    setError(null);
    try {
      const result = await purchaseCart();
      navigate('/result', { state: result });
    } catch (e: any) {
      setError(e.message || 'Payment failed. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container py-5" style={{ maxWidth: 600 }}>
      <h1 className="mb-4">Payment</h1>
      <div className="mb-2 d-flex align-items-center gap-3 fw-bold border-bottom pb-2" style={{fontSize: '1rem'}}>
        <div className="flex-grow-1">Denomination</div>
        <div style={{width: 160, textAlign: 'center'}}>Quantity</div>
        <div className="flex-shrink-0 text-end" style={{width: 80}}>Sum</div>
        <div style={{width: 32}}></div>
      </div>
      <div className="mb-4">
        {coinList.map(coin => (
          <CoinInput
            key={coin.id}
            denomination={Number(coin.denomination)}
            value={coins[coin.id] || 0}
            onChange={v => handleCoinChange(coin.id, v)}
            max={MAX_COIN}
          />
        ))}
      </div>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <div className="fs-5 fw-semibold">Total: {total}₽</div>
        <div className={"fs-5 fw-semibold " + (enough ? "text-success" : "text-danger")}>Inserted: {inserted}₽</div>
      </div>
      {error && <div className="alert alert-danger mb-3">{error}</div>}
      <div className="d-flex gap-3">
        <button className="btn btn-secondary" onClick={handleBack} disabled={loading}>Back</button>
        <button className="btn btn-success" onClick={handlePay} disabled={!enough || loading}>
          {loading ? 'Processing...' : 'Pay'}
        </button>
      </div>
    </div>
  );
} 