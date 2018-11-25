using System;
using System.Collections.Generic;

namespace cookpot_bl {

    public class Dish {
        public List<CookingUtensil> CookingUtensils { get; set; }
        public string Description { get; set; }
        public string Imagery { get; set; }
        public string Kitchen { get; set; }
        public string Origin { get; set; }
        public string Rating { get; set; }
        public List<Recipe> Recipes { get; set; }
        public string ServingSize { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public List<User> Users { get; set; }

    }
}
