using System;
using System.Collections.Generic;
using cookpot.bl.UserManagement;

namespace cookpot.bl.DataModel
{

    public class Dish
    {
        public Dish()
        {
            var ingredients = new List<Ingredient>();
            var cuisines = new List<Cuisine>();
            var origins = new List<Origin>();
            var recipes = new List<Recipe>();
            var users = new List<User>();
            var cookingUtensils = new List<CookingUtensil>();
        }

        public DateTime creationTime { get; set; }

        // things needed to prepare this dish (e.g. large pot)
        public List<CookingUtensil> CookingUtensils { get; set; }

        public List<Origin> Origins { get; set; }
        public List<string> RecipeTypes { get; set; }
        public List<Cuisine> Cuisines { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        [RdfName("description")]
        public string Description { get; set; }

        // Person which wrote the recipe
        public string Author { get; set; }

        //List of Image Pathes
        public List<string> Imagery { get; set; }

        // Kitchen Type, e.g. Asian, Mediterranean
        public string Kitchen { get; set; }

        // User rating of the tastyness of this dish
        public int Rating { get; set; }

        public List<Recipe> Recipes { get; set; }

        // Number of servings of this Dish
        public int ServingSize { get; set; }
        public int ServingSizeMin { get; set; }
        public int ServingSizeMax { get; set; }

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
