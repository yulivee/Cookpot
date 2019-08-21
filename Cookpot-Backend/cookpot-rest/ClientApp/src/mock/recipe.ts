import { Recipe } from "src/classes";

export const mockData = {
  Id: 0,
  RecipeName: 'Reis kochen',
  DurationTime: 20,
  DurationUnit: 'min',
  Steps: [
    {
      Id: 1.1,
      Description: 'Wasser kochen',
    },
    {
      Id: 1.2,
      Description: 'Salz dazu',
    },
    {
      Id: 1.3,
      Description: 'Wenn Wasser kocht, Reis hinterher',
    },
    {
      Id: 1.4,
      Description: '20 min warten',
    },
    {
      Id: 1.5,
      Description: 'zack - feddisch!',
    },
  ],
  Ingredients: [
          {
            Id: 2.1,
            IngredientName: 'Reis',
            Measure: 100,
            Unit: { Id: 3.0, UnitName: 'g' },
          },
          {
            Id: 2.2,
            IngredientName: 'Wasser',
            Measure: 1,
            Unit: { Id: 3.1, UnitName: 'L' },
          },
          {
            Id: 2.3,
            IngredientName: 'Salz',
            Measure: 1,
            Unit: { Id: 3.2, UnitName: 'Prise' },
          },
        ],
};