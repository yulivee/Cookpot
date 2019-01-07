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

        // things needed to prepare this dish (e.g. large pot)
        [RdfName(":Utensil")]
        public List<CookingUtensil> CookingUtensils { get; set; }

        [RdfName(":Origin")]
        public List<Origin> Origins { get; set; }
        // E.g. Soup, Poultry, Sidedish
        [RdfName(":recipeType")]
        public List<string> RecipeTypes { get; set; }
        [RdfName(":Cuisine")]
        // Kitchen Type, e.g. Asian, Mediterranean
        public List<Cuisine> Cuisines { get; set; }
        [RdfName(":Ingredient")]
        public List<Ingredient> Ingredients { get; set; }

        [RdfName(":Recipe")]
        public List<Recipe> Recipes { get; set; }
        //List of Image Pathes
        public List<string> Imagery { get; set; }
        // Users who have access to this dish
        public List<User> Users { get; set; }
        [RdfName(":dishDescription")]
        public string Description { get; set; }

        // Person which wrote the recipe
        [RdfName(":author")]
        public string Author { get; set; }

        // User rating of the tastyness of this dish
        public Nullable<int> Rating { get; set; }

        // Number of servings of this Dish
        [RdfName(":servings")]
        public Nullable<int> ServingSize { get; set; }
        [RdfName(":servingsMin")]
        public Nullable<int> ServingSizeMin { get; set; }
        [RdfName(":servingsMax")]
        public Nullable<int> ServingSizeMax { get; set; }

        // Source of the Recipe, e.g. URL or Bookname + Page
        [RdfName(":source")]
        public string Source { get; set; }

        // Name of the Dish
        [RdfName(":title")]
        public string Title { get; set; }

    }
}
