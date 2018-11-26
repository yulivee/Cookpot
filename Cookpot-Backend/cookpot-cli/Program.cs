using System;
using cookpot_al;
using cookpot.bl;

namespace cookpot_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var hello = new Class1();
            hello.Hello("C# is fancy");
            hello.HelloDefault();
            hello.HelloDefault("No default here");

            Model modell = new SubView();

/*
 Action<string> # Function without return code follows
 ( x )  =>   { }   # Function 

 Action setter = ( string y ) => { do stuff };

 */
            Action<string> setter = (x) => modell.modelname = x;
            setter("hello");


            modell.modelname = "Sandra";
            var sandra = modell.modelname;

            var view = new View();
            view.Viewname = "Benny";

        }
    }
}
