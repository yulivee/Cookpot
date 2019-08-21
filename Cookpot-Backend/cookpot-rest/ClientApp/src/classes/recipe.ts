import { Step } from './step';
import { Ingredient } from './ingredient';

export class Recipe {
  public Id: number;
  public DurationTime?: number;
  public DurationUnit?: string;
  public RecipeName: string = '';
  public Steps: Array<Step> = [];
  public Ingredients: Array<Ingredient> = [];
}