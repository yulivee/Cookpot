import { Component, OnInit } from '@angular/core';
import { RecipeService } from 'src/app/services/recipe.service';
import { Observable } from 'rxjs';
import { Recipe } from '../../../classes';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.css']
})
export class RecipeComponent implements OnInit {
  public recipe$: Observable<Recipe>;
  public recipe: Recipe;
  public devMode: boolean = false;

  constructor(private _recipeService: RecipeService) {
  }

  ngOnInit(): void {
    this.recipe$ = this._recipeService.getCurrentRecipe();
    this.recipe$.subscribe(r => this.recipe = r);
  }
}
