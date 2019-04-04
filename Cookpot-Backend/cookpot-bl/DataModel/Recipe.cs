using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public class Recipe
    {
        
        public int Id { get; set; }
        public Nullable<int> DurationTime { get; set; }
        
        public string DurationUnit { get; set; }
        
        public string RecipeName { get; set; }
        
        public List<Step> Steps { get; set; }
    }
}
