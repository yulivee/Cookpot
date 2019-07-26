import { Step } from './step';

export class Recipe {
  public Id: number;
  public DurationTime?: number;
  public DurationUnit?: string;
  public RecipeName: string = '';
  public Steps: Array<Step> = [];
}