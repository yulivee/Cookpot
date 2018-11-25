using System;
using System.Collections.Generic;

namespace cookpot_bl {

    public class Dish {
        public List<CookingUtensil> CookingUtensils { get; set; }
        public string Description { get; set; }
        
        //List of Image Pathes
        public List<string> Imagery { get; set; }
        public string Kitchen { get; set; }
        public string Origin { get; set; }
        public int Rating { get; set; }
        public List<Recipe> Recipes { get; set; }
        public int ServingSize { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public List<User> Users { get; set; }

    }
}
