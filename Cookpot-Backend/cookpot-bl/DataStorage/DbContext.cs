using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using cookpot.bl.DataModel;

namespace cookpot.bl.DataStorage
{
    public class CookpotContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Origin> Origins { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<CookingUtensil> CookingUtensils { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Dish> Dishs { get; set; }
        public DbSet<UnitofMeasurement> UnitofMeasurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=cookpot;Username=cookpot;Password=pw");
        
    }

}