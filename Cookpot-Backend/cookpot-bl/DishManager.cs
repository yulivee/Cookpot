using System;

namespace cookpot_bl {
    public class DishManager {

        public Dish create(string Title){
            Dish newDish = new Dish();
            newDish.Title = Title;
            return newDish;
        }

        public void read(Dish dish) { }
        public void update(Dish dish) { }
        public void delete(Dish dish) { }

    }
}
