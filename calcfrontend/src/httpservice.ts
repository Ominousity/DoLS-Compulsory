import {HttpClient} from "@angular/common/http";
import {numbers, CalculatedNumbers} from "./dto";
import {Injectable} from "@angular/core";
import {firstValueFrom} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class Httpservice{
  baseURL: string = 'url';

  constructor(public http: HttpClient) {
  }

  doCalculate(_list: numbers){
    return this.http.post(this.baseURL, _list)
  }

  getCalculations(id: string){
    return this.http.get(this.baseURL + '?id=' + id)

  }
}

