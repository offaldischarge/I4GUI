using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delopgave3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter 1st number:");
            string firstNumber = Console.ReadLine();
            int fNumber = int.Parse(firstNumber);

            Console.WriteLine("Enter 2nd number:");
            string secondNumber = Console.ReadLine();
            int sNumber = int.Parse(secondNumber);

            int result = fNumber + sNumber;

            Console.WriteLine($"The sum is: {result}");
        }
    }
}
