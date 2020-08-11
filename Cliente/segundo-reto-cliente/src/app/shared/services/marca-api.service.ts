import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Marca } from '../models/marca'

@Injectable({
  providedIn: 'root'
})
export class MarcaApiService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(`/api/marcas`);
  }

  get(id: number) {
    return this.http.get(`/api/marcas/${id}`);
  }

  create(data: Marca) {
    return this.http.post(`/api/marcas`, data);
  }

  update(id: number, data: Marca) {
    return this.http.put(`/api/marcas/${id}`, data);
  }

  delete(id: number) {
    return this.http.delete(`/api/marcas/${id}`);
  }
}
