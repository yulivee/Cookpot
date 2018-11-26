using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public class Recipe
    {
        public string Time { get; set; }
        public List<Step> Steps { get; set; }

    }
}
