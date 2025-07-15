import React from 'react';

type Props = {
  min: number;
  max: number;
  value: number;
  onChange: (value: number) => void;
};

export function PriceSlider({ min, max, value, onChange }: Props) {
  return (
    <div className="d-flex flex-column align-items-start gap-2">
      <label className="form-label">Price: {value}₽</label>
      <input
        type="range"
        min={min}
        max={max}
        value={value}
        onChange={e => onChange(Number(e.target.value))}
        className="form-range"
        style={{ width: 200 }}
      />
      <div className="d-flex justify-content-between w-100" style={{ width: 200 }}>
        <span>{min}₽</span>
        <span>{max}₽</span>
      </div>
    </div>
  );
} 