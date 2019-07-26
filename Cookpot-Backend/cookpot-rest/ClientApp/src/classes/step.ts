import { Ingredient } from './ingredient';

export class Step {
  public Id: number;
  public Description: string;
  public Ingredients: Array<Ingredient>;
}
