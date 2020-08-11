import { Marca } from './marca';
import { Proveedor } from './proveedor';

export class Producto 
{
    id: number;
    nombre: string;
    existencia: number;
    precio: number; 
    foto: string;
    estado: number;
    marcaId: number;
    marca: Marca;
    proveedorId: number;
    proveedor: Proveedor;
}
