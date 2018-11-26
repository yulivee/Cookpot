using System;
using System.Collections.Generic;

namespace cookpot.bl
{

    public class AccessManager : IManager<string>
    {
        public void checkAccessRights() { }

        public string Create(string obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Create(IEnumerable<string> objs)
        {
            throw new NotImplementedException();
        }

        public string Update(string obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Update(IEnumerable<string> objs)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
