import { Component, OnInit } from '@angular/core';
import { analyzeFileForInjectables } from '@angular/compiler';
import { Marca } from '../shared/models/marca';
import { MarcaApiService } from '../shared/services/marca-api.service';

@Component({
  selector: 'jc-marcas',
  templateUrl: './marcas.component.html',
  styleUrls: ['./marcas.component.css']
})
export class MarcasComponent implements OnInit {

  displayedColumns: string[] = ['Nombre', 'Accion'];
  dataSource: Marca[] = [];
  model: Marca = new Marca();
  title: string = "Marcas";

  processing: boolean = true;

  constructor(private marcaService: MarcaApiService) { }

  ngOnInit(): void {
    this.readAll();
  }

  readAll(): void {
    this.dataSource = [];
    this.marcaService.getAll()
      .subscribe(
      data => { 
        this.dataSource = (data as Marca[]);
        this.processing = false;
      },
      error => {
        console.log(error);
        this.processing =false;
      });
  }

  update(marca: Marca): void{
    this.model = new Marca();
    this.model.id = marca.id;
    this.model.nombre = marca.nombre;
    this.model.estado = marca.estado;
  }

  save(): void {
    this.processing = true;
    if(this.model.nombre != null){
      if(this.model.id){
        //alert("Update!");
        this.marcaService.update(this.model.id, this.model)
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
        this.marcaService.create(this.model)
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

  delete(marca: Marca): void{
    //Delete
    this.processing = true;
    this.marcaService.delete(marca.id)
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
    this.model = new Marca();
  }

}
