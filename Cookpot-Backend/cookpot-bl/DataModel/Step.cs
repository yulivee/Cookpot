using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public class Step
    {
        
        public int Id { get; set; }
        public string Description { get; set; }
        
        public List<Ingredient> Ingredients { get; set; }
    }
}
