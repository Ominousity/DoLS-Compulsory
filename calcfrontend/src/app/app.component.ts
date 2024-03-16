import {Component, OnInit} from '@angular/core';
import {RouterOutlet, Routes} from '@angular/router';
import {v4 as uuidv4} from "uuid";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";
import {FormsModule} from "@angular/forms";
import {Httpservice} from "../httpservice";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatCard, MatCardContent, MatCardHeader, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent{
  title = 'calcfrontend';
  id = uuidv4()
  calculateString: string[] = [];

  constructor(public http: Httpservice) {
  }
  addToList(value: string){
  this.calculateString.push(value);
  }

  clearList(){
    this.calculateString = [];
  }

}




