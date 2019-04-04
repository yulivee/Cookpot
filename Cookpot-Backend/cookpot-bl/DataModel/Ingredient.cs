using System;

namespace cookpot.bl.DataModel
{

    public class Ingredient
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Nullable<double> Amount { get; set; }
        
        public Nullable<double> Measure { get; set; }
        
        public UnitofMeasurement Unit { get; set; }


    }
}
