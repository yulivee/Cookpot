using System;

namespace cookpot.bl.DataModel
{

    public class Ingredient
    {
        public string Name { get; set; }
        public Nullable<int> Amount { get; set; }
        public UnitofMeasurement Measure { get; set; }


    }
}
