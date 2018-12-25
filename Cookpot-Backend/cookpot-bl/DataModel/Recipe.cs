using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public class Recipe
    {
        [RdfName(":durationTime")]
        public Nullable<int> DurationTime { get; set; }
        [RdfName(":DurationUnit")]
        public string DurationUnit { get; set; }
        [RdfName(":recipeName")]
        public string RecipeName { get; set; }
        [RdfName(":Step")]
        public List<Step> Steps { get; set; }
    }
}
