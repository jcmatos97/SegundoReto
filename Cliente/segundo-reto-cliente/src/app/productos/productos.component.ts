import { Component, OnInit } from '@angular/core';
import { Producto } from '../shared/models/producto';
import { ProductoApiService } from '../shared/services/producto-api.service';

@Component({
  selector: 'jc-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css']
})
export class ProductosComponent implements OnInit {

  displayedColumns: string[] = ['Nombre', 'Existencia', 'Precio', 'Marca', 'Proveedor', 'Accion'];
  dataSource: Producto[] = [];
  model: Producto = new Producto();
  title: string = "Productos";

  processing: boolean = true;

  constructor(private productoService: ProductoApiService) { }

  ngOnInit(): void {
    this.readAll();
  }

  readAll(): void {
    this.dataSource = [];
    this.productoService.getAll()
    .subscribe(
      data => { 
        console.log(data);
        this.dataSource = (data as Producto[]);
        this.processing = false;
      },
      error => {
        console.log(error);
        this.processing =false;
      });
  }

  update(producto: Producto): void{
    this.model = new Producto();
    this.model.id = producto.id;
    this.model.nombre = producto.nombre;
    this.model.existencia = producto.existencia;
    this.model.precio = producto.existencia;
    this.model.foto = "foto";
    this.model.estado = producto.estado;
    this.model.marcaId = producto.marcaId;
    this.model.proveedorId = producto.proveedorId;
  }

  save(): void { 
    this.processing = true;
    if(this.model.nombre != null){
      if(this.model.id){
        //alert("Update!");
        this.productoService.update(this.model.id, this.model)
        .subscribe(
          response => {
            console.log(response);
            this.processing = false;
            this.readAll();
          },
          error => {
            console.log(error);
            this.processing = false;
            this.readAll();
          });
      }
      else{
        //alert("Create");
        this.model.foto = "null";
        this.model.estado = 1;
        this.productoService.create(this.model)
          .subscribe(
            response => {
              console.log(response);
              this.processing = false;
              this.readAll();
            },
            error => {
              console.log(error);
              this.processing = false;
              this.readAll();
          });
      }
      this.clean();
      
    }
  }

  delete(producto: Producto): void{
    //Delete
    this.processing = true;
    this.productoService.delete(producto.id)
      .subscribe(
        response => {
          console.log(response);
          this.processing = false;
          this.readAll();
        },
        error => {
          console.log(error);
          this.processing = false;
          this.readAll();
        });
      this.clean();
  }

  clean(): void{
    this.model = new Producto();
  }


}
