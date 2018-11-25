using System;
using System.Collections.Generic;

namespace cookpot_al
{
    interface IManager<T> {
        T Create(T obj);
        IEnumerable<T> Create(IEnumerable<T> objs);
        T Update(T obj);
        IEnumerable<T> Update(IEnumerable<T> objs); 
        Boolean Delete(T obj);
    }
}
