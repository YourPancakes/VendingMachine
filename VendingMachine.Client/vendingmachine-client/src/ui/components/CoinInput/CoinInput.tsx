import React from 'react';
import './CoinInput.css';

type Props = {
  denomination: number;
  value: number;
  onChange: (value: number) => void;
  max: number;
};

export function CoinInput({ denomination, value, onChange, max }: Props) {
  const handleInput = (e: React.ChangeEvent<HTMLInputElement>) => {
    let v = Number(e.target.value);
    if (v < 0) v = 0;
    if (v > max) v = max;
    onChange(v);
  };
  return (
    <div className="d-flex align-items-center gap-3 py-2 border-bottom">
      <div className="flex-grow-1 d-flex align-items-center">
        <span className="coin-circle">{denomination}</span>
      </div>
      <div style={{width: 160, textAlign: 'center'}} className="d-flex align-items-center justify-content-center gap-2">
        <button className="btn btn-outline-secondary btn-sm" onClick={() => onChange(Math.max(0, value - 1))} disabled={value <= 0}>-</button>
        <input type="number" min={0} max={max} value={value} onChange={handleInput} className="form-control form-control-sm text-center" style={{ width: 80 }} />
        <button className="btn btn-outline-secondary btn-sm" onClick={() => onChange(Math.min(max, value + 1))} disabled={value >= max}>+</button>
      </div>
      <div className="flex-shrink-0 text-end" style={{ width: 80 }}>{(denomination * value).toFixed(2)}₽</div>
      <div style={{width: 32}}></div>
    </div>
  );
} 