using System;
using System.Collections.Generic;

namespace cookpot_bl {
    public class Step {
        public string Description { get; set; }
        public string Time { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
