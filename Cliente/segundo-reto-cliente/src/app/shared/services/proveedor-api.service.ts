import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Marca } from '../models/marca'

@Injectable({
  providedIn: 'root'
})
export class ProveedorApiService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(`/api/proveedores`);
  }

  get(id: number) {
    return this.http.get(`/api/proveedores/${id}`);
  }

  create(data: Marca) {
    return this.http.post(`/api/proveedores`, data);
  }

  update(id: number, data: Marca) {
    return this.http.put(`/api/proveedores/${id}`, data);
  }

  delete(id: number) {
    return this.http.delete(`/api/proveedores/${id}`);
  }

}
