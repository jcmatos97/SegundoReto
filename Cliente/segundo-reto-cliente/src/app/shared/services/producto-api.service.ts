import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Producto } from '../models/producto'

@Injectable({
  providedIn: 'root'
})
export class ProductoApiService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(`/api/productos`);
  }

  get(id: number) {
    return this.http.get(`/api/productos/${id}`);
  }

  create(data: Producto) {
    return this.http.post(`/api/productos`, data);
  }

  update(id: number, data: Producto) {
    return this.http.put(`/api/productos/${id}`, data);
  }

  delete(id: number) {
    return this.http.delete(`/api/productos/${id}`);
  }
}
