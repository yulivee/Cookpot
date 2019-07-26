import { Component, OnInit } from '@angular/core';
import { RecipeService } from 'src/app/services/recipe.service';
import { Observable } from 'rxjs';
import { Recipe } from '../classes';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';

  public recipe: Observable<Recipe>;
  public localRecipe: Recipe = { Id: 666, DurationTime: 0, Steps: [], RecipeName: '' };

  constructor(private _recipeService: RecipeService) {
  }

  ngOnInit(): void {
    this.recipe = this._recipeService.getCurrentRecipe();
    //this.recipe.subscribe(r => this.localRecipe = r);
  }
}
