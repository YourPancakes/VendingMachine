import { Brand } from '../domain/brand';
import { request } from './http';

export const getBrands = async (): Promise<Brand[]> =>
  request<Brand[]>('/brands', 'GET'); 