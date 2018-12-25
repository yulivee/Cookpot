using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public class Step
    {
        [RdfName(":stepDescription")]
        public string Description { get; set; }
        [RdfName(":Ingredient")]
        public List<Ingredient> Ingredients { get; set; }
    }
}
