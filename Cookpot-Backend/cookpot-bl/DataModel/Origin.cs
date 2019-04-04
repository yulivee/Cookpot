using System;
using System.Collections.Generic;

namespace cookpot.bl.DataModel
{
    public abstract class Origin
    {
        
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class OriginCountry : Origin 
    {
        
        public override int Id { get; set; }
        public override string Name { get; set; }

    }

    public class OriginRegion : Origin 
    {
        
        public override int Id { get; set; }
        public override string Name { get; set; }

    }
    public class OriginCity : Origin 
    {
        
        public override int Id { get; set; }
        public override string Name { get; set; }

    }
}