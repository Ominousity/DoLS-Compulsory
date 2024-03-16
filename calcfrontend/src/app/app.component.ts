import {Component, OnInit} from '@angular/core';
import {RouterOutlet, Routes} from '@angular/router';
import {v4 as uuidv4} from "uuid";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {FormsModule} from "@angular/forms";
import {Httpservice} from "../httpservice";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {CalculatedNumbers, numbers, NumbersToBeCalculated} from "../dto";
import AppModule from "./app.module";
import {Observable} from "rxjs";
import {AsyncPipe, NgForOf} from "@angular/common";




@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatCard, MatCardContent, MatCardHeader, FormsModule, AppModule, NgForOf, AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  data$: Observable<CalculatedNumbers[]>;
  title = 'calcfrontend';
  id = uuidv4()
  calculateString: string = '';
  baseURL: string = 'http://localhost:8091';

  constructor(private http: HttpClient) {
  }
  addToList(value: string){
  this.calculateString = this.calculateString + value;
  }

  clearList(){
    this.calculateString = '';
  }

  doCalculate(){
    let calculation: NumbersToBeCalculated = {
      userId: this.id,
      equation: this.calculateString,
      operation: 0
    }
    console.log(calculation);
    return this.http.post(this.baseURL + '/api/doCalculation', calculation).subscribe((data) =>{
      console.log(data);
      this.getCalculations(this.id)
    })
  }

  getCalculations(id: string){
  this.data$ = this.http.get<CalculatedNumbers[]>(this.baseURL + '/api/getCalculations' +'?id=' + id)
  }

  ngOnInit(): void {
    this.getCalculations(this.id)
  }


}




