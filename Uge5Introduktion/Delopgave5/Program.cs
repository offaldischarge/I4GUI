using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delopgave5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Welcome to the Hi-Lo game");
            Console.WriteLine("Think of a number between 1 and 100");
            Console.WriteLine("I will make a guess");

            bool guessed = false;

            int guess = 50;
            int tempGuess;

            int maxNumber = 100;
            int minNumber = 0;

            while (!guessed)
            {
                Console.WriteLine($"My guess is: {guess}");
                Console.Write("Tell me if I'm (H)igh, (L)ow or (E)qual? ");

                string charNum = Console.ReadLine();

                char hle = char.Parse(charNum);

                switch (hle)
                {
                    case 'h':
                        maxNumber = guess;
                        tempGuess = guess / 2;
                        if (tempGuess < minNumber)
                        {
                            guess = guess - ((maxNumber - minNumber) / 2);
                        }
                        else
                        {
                            guess = tempGuess;
                        }
                        break;
                    case 'l':
                        minNumber = guess;
                        tempGuess = (int) (guess * 1.5);
                        if (tempGuess > maxNumber)
                        {
                            guess = guess + ((maxNumber - minNumber)/ 2);
                        }
                        else
                        {
                            guess = tempGuess;
                        }
                        break;
                    case 'e':
                        Console.WriteLine("*** I GOT IT ***");
                        guessed = true;
                        break;
                }
            }
        }
    }
}
