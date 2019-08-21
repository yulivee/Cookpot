import { Injectable } from '@angular/core';
import { Recipe, Ingredient, Step, UnitOfMeasure } from '../../classes';
import { Observable, of, from } from 'rxjs';
import { map, delay } from 'rxjs/operators';
import { ajax, AjaxResponse } from 'rxjs/ajax';

import { mockRecipe } from '../../mock';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  private _selectedRecipeId: number;

  constructor(private _http: HttpClient) { }


  private _responseHelper<T>(response: AjaxResponse, instance: T): T {
    if (!response.response)
      throw new Error('request not succesfull');
    if (response.responseType !== 'json')
      throw new Error('request has the wrong type');
    return Object.assign(instance, JSON.parse(response.response) as T);
  }

  private _responseHelper1<T>(response: AjaxResponse, creator: () => T): T {
    if (!response.response)
      throw new Error('request not succesfull');
    if (response.responseType !== 'json')
      throw new Error('request has the wrong type');
    return Object.assign(creator(), JSON.parse(response.response) as T);
  }

  private _responseHelper2<T>(creator: () => T): (response: AjaxResponse) => T {
    return (response: AjaxResponse) => this._responseHelper1(response, creator);
  }

  public selectRecipe(Id: number): void {
    this._selectedRecipeId = Id;
  }

  public getCurrentRecipe(): Observable<Recipe> {
    const mapToRecipe = this._responseHelper2(() => new Recipe());
    const directMapToRecipe = ((res: AjaxResponse) => this._responseHelper1(res, () => new Recipe()));
    ajax('').pipe(
      map(directMapToRecipe)
    );
    ajax('').pipe(
      map(mapToRecipe)
    );
    //this._http.get('').pipe(Map())
    return of(mockRecipe).pipe(delay(5000)).pipe(map((raw) => Object.assign(new Recipe(), raw)));
  }
}
