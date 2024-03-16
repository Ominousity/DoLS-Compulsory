import {Component, OnInit} from '@angular/core';
import {RouterOutlet, Routes} from '@angular/router';
import {v4 as uuidv4} from "uuid";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {FormsModule} from "@angular/forms";
import {Httpservice} from "../httpservice";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {numbers} from "../dto";
import AppModule from "./app.module";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatCard, MatCardContent, MatCardHeader, FormsModule, AppModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent{
  title = 'calcfrontend';
  id = uuidv4()
  calculateString: string = '';
  baseURL: string = 'url';

  constructor(private http: HttpClient) {
  }
  addToList(value: string){
  this.calculateString = this.calculateString + value;
  }

  clearList(){
    this.calculateString = '';
  }

  doCalculate(){
    return this.http.post(this.baseURL, this.calculateString).subscribe((data) =>{
      console.log(data);
    })
  }

  getCalculations(id: string){
    return this.http.get(this.baseURL + '?id=' + id)

  }
}




