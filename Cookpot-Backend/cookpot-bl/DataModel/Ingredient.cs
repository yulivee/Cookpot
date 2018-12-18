using System;

namespace cookpot.bl.DataModel
{

    public class Ingredient
    {
        public string Name { get; set; }
        public Nullable<float> Amount { get; set; }
        public Nullable<float> Measure { get; set; }
        public UnitofMeasurement Unit { get; set; }


    }
}
