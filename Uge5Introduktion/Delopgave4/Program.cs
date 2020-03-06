using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delopgave4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Welcome to the Hi-Lo game");
            Console.WriteLine("The computer choose a number between 1 and 100, you guess it");

            Random rand = new Random();
            int randomNumber = rand.Next(1, 100);

            bool guessed = false;

            while (!guessed)
            {
                Console.Write("Enter your guess: ");
                string guess = Console.ReadLine();
                int guessNum = int.Parse(guess);

                if (guessNum > randomNumber)
                {
                    Console.WriteLine("Your guess is too high");
                }
                else if (guessNum < randomNumber)
                {
                    Console.WriteLine("You guess is too low");
                }
                else if (guessNum == randomNumber)
                {
                    Console.WriteLine("*** YOU GOT IT ***");
                    guessed = true;
                }
            }
            
        }
    }
}
