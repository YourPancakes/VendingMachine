import React from 'react';
import { Brand } from '../../../domain/brand';
import './BrandFilter.css';

type Props = {
  brands: Brand[];
  value: number | null;
  onChange: (brandId: number | null) => void;
};

export function BrandFilter({ brands, value, onChange }: Props) {
  return (
    <select className="brand-filter" value={value ?? ''} onChange={e => onChange(e.target.value ? Number(e.target.value) : null)}>
      <option value="">All brands</option>
      {brands.map(brand => (
        <option key={brand.id} value={brand.id}>{brand.name}</option>
      ))}
    </select>
  );
} 