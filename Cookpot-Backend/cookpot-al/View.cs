using System;

namespace cookpot_al
{
    public class View
    {
        // property modelname
        // get; set; => generate implicit getter and setters (no need to write get_abc)
        public string Viewname { get; set; }
        public string Viewtype { get; private set; }

        private string _specialSecret;

        // custom getter/setter methods
        public string SpecialSecret{ 
            get {return this._specialSecret; } 
            set {this._specialSecret = value; }
            }
    }

    public class SubView : View, Model
    {
        public string modelname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string modeltype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
