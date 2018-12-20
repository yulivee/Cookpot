using System;

namespace cookpot.bl.DataModel
{
    internal class RdfNameAttribute : Attribute
    {
        public string Name {get; set;}
        public RdfNameAttribute(string name) {
            this.Name = name;
        }
    }
}