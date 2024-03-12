import { Component } from '@angular/core';
import {RouterOutlet, Routes} from '@angular/router';
import {v4 as uuidv4} from "uuid";
import {MatCard, MatCardContent, MatCardHeader} from "@angular/material/card";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatCard, MatCardContent, MatCardHeader],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'calcfrontend';
  id = uuidv4()
}




