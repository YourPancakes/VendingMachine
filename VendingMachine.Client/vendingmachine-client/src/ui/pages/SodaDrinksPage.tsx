import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { BrandFilter } from '../components/BrandFilter/BrandFilter';
import { PriceSlider } from '../components/PriceSlider/PriceSlider';
import { ProductGrid } from '../components/ProductGrid/ProductGrid';
import { CartButton } from '../components/CartButton/CartButton';
import { fetchBrands } from '../../app/brands/brandsThunks';
import { fetchProducts } from '../../app/products/productsThunks';
import { fetchCart } from '../../app/cart/cartThunks';
import { addToCartThunk } from '../../app/cart/cartThunks';
import { selectBrands } from '../../app/brands/brandsSelectors';
import { selectProducts } from '../../app/products/productsSelectors';
import { selectCart, selectCartTotalItems } from '../../app/cart/cartSelectors';
import { selectFilters, setBrandId } from '../../app/filters/filtersSlice';
import { Product } from '../../domain/product';
import { RootState, AppDispatch } from '../../app/store';
import './SodaDrinksPage.css';
import { updateProduct } from '../../api/productApi';

export function SodaDrinksPage() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const brands = useSelector((state: RootState) => selectBrands(state));
  const products = useSelector((state: RootState) => selectProducts(state));
  const cart = useSelector((state: RootState) => selectCart(state));
  const cartCount = useSelector((state: RootState) => selectCartTotalItems(state));
  const filters = useSelector((state: RootState) => selectFilters(state));
  const [maxPrice, setMaxPrice] = useState(0);
  const [selectedMaxPrice, setSelectedMaxPrice] = useState(0);
  const [adminMode, setAdminMode] = useState(false);
  const [updating, setUpdating] = useState(false);

  useEffect(() => {
    dispatch(fetchBrands());
    dispatch(fetchCart());
  }, [dispatch]);

  useEffect(() => {
    if (brands.length > 0) {
      dispatch(fetchProducts({ brandId: filters.brandId || undefined }));
    }
  }, [dispatch, brands, filters.brandId]);

  useEffect(() => {
    if (products.length > 0) {
      const rawMax = Math.max(...products.map((p: Product) => p.price));
      let max = Math.ceil(rawMax);
      if (max === rawMax) max += 1;
      setMaxPrice(max);
      setSelectedMaxPrice(max);
    }
  }, [products]);

  const handleBrandChange = (brandId: number | null) => {
    dispatch(setBrandId(brandId));
    dispatch(fetchProducts({ brandId: brandId || undefined }));
  };

  const handlePriceChange = (value: number) => {
    setSelectedMaxPrice(value);
  };

  const handleSelectProduct = (product: Product) => {
    dispatch(addToCartThunk({ productId: product.id, quantity: 1 }));
  };

  const handleCartClick = () => {
    navigate('/checkout');
  };

  const handleQuantityChange = async (productId: number, quantity: number) => {
    const product = products.find(p => p.id === productId);
    if (!product) return;
    setUpdating(true);
    try {
      await updateProduct({ ...product, quantity });
      dispatch(fetchProducts({ brandId: filters.brandId || undefined }));
    } catch {
    } finally {
      setUpdating(false);
    }
  };

  const filteredProducts = products.filter((p: Product) => p.price <= selectedMaxPrice).slice().sort((a, b) => a.id - b.id);
  const cartProductIds = new Set(cart?.items.map(i => i.productId));

  return (
    <div className="soda-drinks-page">
      <h1 className="mb-3">Soda Machine</h1>
      <div className="d-flex justify-content-between align-items-center mb-3">
        <div className="d-flex gap-4 align-items-center">
          <BrandFilter brands={brands} value={filters.brandId} onChange={handleBrandChange} />
          <PriceSlider min={0} max={maxPrice} value={selectedMaxPrice} onChange={handlePriceChange} />
        </div>
        <div className="d-flex flex-column align-items-end gap-2">
          <div className="form-check form-switch">
            <input
              className="form-check-input"
              type="checkbox"
              id="adminModeSwitch"
              checked={adminMode}
              onChange={() => setAdminMode(prev => !prev)}
            />
            <label className="form-check-label" htmlFor="adminModeSwitch" style={{fontSize: '0.9rem'}}>
              Admin Mode
            </label>
          </div>
          <CartButton count={cartCount} onClick={handleCartClick} disabled={cartCount === 0} />
        </div>
      </div>
      <ProductGrid
        products={filteredProducts}
        onSelect={handleSelectProduct}
        selectedProductIds={cartProductIds}
        adminMode={adminMode}
        onQuantityChange={handleQuantityChange}
      />
    </div>
  );
} 