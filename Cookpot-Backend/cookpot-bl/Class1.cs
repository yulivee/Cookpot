using System;

namespace cookpot.bl
{
    public class Class1
    {

     /**
      Prints Helo world!
      */   
       public void Hello(string fancyParam)
        {
            Console.WriteLine($"Hello World!{fancyParam}"); // String Interpolation: $ before " indicates special string, variable is put in {} for interpolation
            Console.WriteLine("Hello World!"+fancyParam); // String Concatenation
        }

        public void HelloDefault(string anotherFancyParam = "I am the default :P")
        {
            Console.WriteLine("This is a method with default parameters :D!" + anotherFancyParam);
        }
    }
}
