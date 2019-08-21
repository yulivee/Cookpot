import { UnitOfMeasure } from './unitofmeasure';

export class Ingredient {
  public Id: number;
  public IngredientName: string;
  public Amount?: number;
  public Measure?: number;
  public Unit: UnitOfMeasure;
}
