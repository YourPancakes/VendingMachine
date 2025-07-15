import axios, { AxiosRequestConfig, Method } from 'axios';

const api = axios.create({ baseURL: '/api/v1.0' });

export async function request<T>(
  url: string,
  method: Method,
  options?: AxiosRequestConfig
): Promise<T> {
  try {
    const { data } = await api.request<T>({ url, method, ...options });
    return data;
  } catch (error: any) {
    let message = 'Unknown error';
    if (error.response && error.response.data) {
      const data = error.response.data;
      switch (typeof data) {
        case 'string':
          message = data;
          break;
        case 'object':
          if (Array.isArray(data) && data.length > 0 && data[0].message) {
            message = data[0].message;
          } else if (data && data.message) {
            message = data.message;
          } else if (data && data.error) {
            message = data.error;
          } else {
            message = JSON.stringify(data);
          }
          break;
        default:
          message = JSON.stringify(data);
      }
    } else if (error.message) {
      message = error.message;
    }
    throw new Error(message);
  }
} 