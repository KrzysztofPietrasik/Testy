using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    public class FizzBuzz
    {
        public static string GetOutput(int number)
        {
            
            if ((number % 3 == 0) && (number % 5 == 0))
                return "FizzBuzz";
            
            if (number % 3 == 0)
                return "Fizz";

            if (number % 5 == 0)
                return "Buzz";
           
            if (number < 0)
                throw new System.ArgumentException("Liczba nie może być mniejsza od 0");

            return number.ToString();
        }
    }
}
