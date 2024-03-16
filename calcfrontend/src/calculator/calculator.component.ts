import { Component } from '@angular/core';
import { v4 as uuidv4 } from "uuid";

@Component({
  selector: 'app-calculator',
  standalone: true,
  imports: [],
  templateUrl: './calculator.component.html',
  styleUrl: './calculator.component.css'
})
export class CalculatorComponent {

  id = uuidv4()
}

