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
            while (true)
            {
                GetInput();
            }

            Console.ReadLine();
        }

        static void Startup()
        {
            Console.WriteLine("Welcome to 'Text Game Engine'");
        }

        static void GetInput()
        {
            Console.Write(">> ");
            string userInput = Console.ReadLine();
            string[] splitUserInput = userInput.Split(new char[] { });
            //prints the user input in a readable way
            //for (int i = 0; i < splitUserInput.Length; i++)
            //{
            //    Console.Write("{0} ", splitUserInput[i], i);
            //}
            //Console.WriteLine("\n-----------------------");

            if (userInput.Length > 1)
            {

            }
            else // if user types one word
            {

            }

            //return userInput;
        }
    }
}
