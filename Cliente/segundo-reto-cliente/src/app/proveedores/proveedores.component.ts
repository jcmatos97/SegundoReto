import { Component, OnInit } from '@angular/core';
import { analyzeFileForInjectables } from '@angular/compiler';
import { Proveedor } from '../shared/models/proveedor';
import { ProveedorApiService } from '../shared/services/proveedor-api.service';

@Component({
  selector: 'jc-proveedores',
  templateUrl: './proveedores.component.html',
  styleUrls: ['./proveedores.component.css']
})
export class ProveedoresComponent implements OnInit {

  displayedColumns: string[] = ['Nombre', 'Accion'];
  dataSource: Proveedor[] = [];
  model: Proveedor = new Proveedor();
  title: string = "Proveedores";

  processing: boolean = true;

  constructor(private proveedorService: ProveedorApiService) { }

  ngOnInit(): void {
    this.readAll();
  }

  readAll(): void {
    this.dataSource = [];
    this.proveedorService.getAll()
      .subscribe(
      data => { 
        this.dataSource = (data as Proveedor[]);
        this.processing = false;
      },
      error => {
        console.log(error);
        this.processing =false;
      });
  }

  update(proveedor: Proveedor): void{
    this.model = new Proveedor();
    this.model.id = proveedor.id;
    this.model.nombre = proveedor.nombre;
    this.model.estado = proveedor.estado;
  }

  save(): void {
    this.processing = true;
    if(this.model.nombre != null){
      if(this.model.id){
        //alert("Update!");
        this.proveedorService.update(this.model.id, this.model)
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
        this.proveedorService.create(this.model)
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

  delete(proveedor: Proveedor): void{
    //Delete
    this.processing = true;
    this.proveedorService.delete(proveedor.id)
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
    this.model = new Proveedor();
  }

}
