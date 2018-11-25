using System;
using System.Collections.Generic;

namespace cookpot_bl {

    public class Dish {
        public Dish()
        {
            this._ctime = DateTime.Now;
        }

        private DateTime _ctime;

        public DateTime creationTime{ get {return this._ctime; } }

        // things needed to prepare this dish (e.g. large pot)
        public List<CookingUtensil> CookingUtensils { get; set; }

        public string Description { get; set; }
        
        //List of Image Pathes
        public List<string> Imagery { get; set; }

        // Kitchen Type, e.g. Asian, Mediterranean
        public string Kitchen { get; set; }

        // User rating of the tastyness of this dish
        public int Rating { get; set; }

        public List<Recipe> Recipes { get; set; }

        // Number of servings of this Dish
        public int ServingSize { get; set; }

        // Source of the Recipe, e.g. URL or Bookname + Page
        public string Source { get; set; }

        // Name of the Dish
        public string Title { get; set; }
        
        // E.g. Soup, Poultry, Sidedish
        public string Type { get; set; }

        // Users who have access to this dish
        public List<User> Users { get; set; }

    }
}
