// Peyton Santo
// This is a text game engine. originally made in Python, translated into C#
// started - 11/28/2022

using System;
using System.Collections.Generic;
using System.Linq;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

namespace Text_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Startup();
            Player player = new Player();
            bool running = true;

            Object thing = CreateAllObjects();
            while (running)
            {
                GetInput(thing, player);
                Console.WriteLine("");
            }

            Console.ReadLine();
        }

        static Object CreateAllObjects()
        {
            Object thing = new Object("null", "null", "null", "null", false, false);
            new Object("key", "large gold key", "first", "This is a large gold key", true, true);
            new Object("key", "small copper key", "first", "This is a small copper key", true, true);
            new Object("chair", "metal folding chair", "first", "This is a metal folding chair", false, false);

            return thing;
        }

        static void Startup()
        {
            Console.WriteLine("Welcome to 'Text Game Engine'");
        }

        static void seeInput(string[] splitUserInput)
        {
            //prints the user input in a readable way
            for (int i = 0; i < splitUserInput.Length; i++)
            {
                Console.Write($"{splitUserInput[i]} ");
            }
            Console.WriteLine("\n-----------------------");
            Console.WriteLine(splitUserInput.Length);
        }

        static void GetInput(Object thing, Player player)
        {
            Console.Write(">> ");
            string userInput = Console.ReadLine().ToLower();
            try
            {
                string[] splitUserInput = userInput.split(new char[] { });
                if (splitUserInput.Length > 1) // if user types multiple words
                {
                    if (splitUserInput[0] == "see")
                    {
                        if (splitUserInput[1] == "items" || splitUserInput[1] == "objects") { thing.SeeAll(); }

                        if (splitUserInput[1] == "inventory") { thing.SeeInventory(); }
                    }

                    if (splitUserInput[0] == "take" || splitUserInput[0] == "drop")
                    {

                    }

                    if (splitUserInput[0] == "look" && splitUserInput[1] == "at")
                    {
                        thing.LookAtItem(userInput);
                    }
                }
                else // if user types one word
                {
                    if (userInput == "look")
                    {
                        Console.WriteLine("Nothing much to see...");
                    }
                    if (userInput == "inventory")
                    {
                        thing.SeeInventory();
                    }
                    if (userInput == "clear")
                    {
                        Console.Clear();
                    }
                }
                }
            catch
            {
                Console.WriteLine("Invalid Input");
            }
        }
    }
}
