using System;

namespace cookpot.bl.DataModel
{

    public class Ingredient
    {
        [RdfName(":name")]
        public string Name { get; set; }
        [RdfName(":amount")]
        public Nullable<double> Amount { get; set; }
        [RdfName(":measureValue")]
        public Nullable<double> Measure { get; set; }
        [RdfName(":MeasureUnit")]
        public UnitofMeasurement Unit { get; set; }


    }
}
