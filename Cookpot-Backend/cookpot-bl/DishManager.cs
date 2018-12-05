using System;
using System.Collections.Generic;
using cookpot.bl.DataModel;
using cookpot.bl.DataStorage;

namespace cookpot.bl
{
    public class DishManager : IManager<Dish>
    {
        public Dish Create(Dish obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> Create(IEnumerable<Dish> objs)
        {
            throw new NotImplementedException();
        }

        public Dish Update(Dish obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> Update(IEnumerable<Dish> objs)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Dish obj)
        {
            throw new NotImplementedException();
        }

        public void ReadRecipeFull()
        {
            RDF fuseki = RDF.new;
            Console.WriteLine(fuseki.Read("Test"));

        }

        public Dish Read(Dish obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> Read(IEnumerable<Dish> objs)
        {
            throw new NotImplementedException();
        }
    }
}
