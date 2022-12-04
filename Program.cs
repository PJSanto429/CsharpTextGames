﻿// Peyton Santo
// This is a text game engine. originally made in Python, translated into C#
// started - 11/28/2022

using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

namespace Text_Game
{
    internal class Program
    {
        // ================== all enums/types ==========================
        public enum feedbackTypes
        {
            danger,
            success
        }
        public enum rooms
        {
            none,
            inventory,
            first,
            second
        }
        // ===================================================

        static void Main(string[] args)
        {
            //Startup();
            Player player = new Player();
            Object thing = CreateAllObjects();
            Room room = CreateAllRooms();

            bool running = true;
            while (running)
            {
                GetInput(thing, player);
                Console.WriteLine("");
            }

            Console.ReadLine();
        }

        static void CreateFeedback(feedbackTypes type, string message)
        {
            switch (type)
            {
                case feedbackTypes.danger:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case feedbackTypes.success:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(message);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        static Room CreateAllRooms()
        {
            new Room("Kitchen", "This is a large kitchen that seems to be themed like ancient times. I wonder why that is...", rooms.first);
            new Room("Bedroom", "This is a bedroom with all the accommodating stuff.", rooms.second);

            // pretty much a window into all rooms
            return new Room("null", "null", rooms.none);
        }

        static Object CreateAllObjects()
        {
            //first room
                //keys
            new Object("key", "large gold key", rooms.inventory, "This is a large gold key", true, false);
            new Object("key", "small copper key", rooms.inventory, "This is a small copper key", true, false);
            new Object("key", "jade key", rooms.inventory, "This is a fancy jade key", false, false);
                //chairs
            new Object("chair", "metal folding chair", rooms.first, "This is a metal folding chair", false, false);

            //second room
            //  add items here
            //==========================================================================
            // window into all objects
            return new Object("null", "null", rooms.none, "null", false, false);
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
                string[] splitUserInput = userInput.Split(new char[] { });
                if (splitUserInput.Length > 1) // if user types multiple words
                {
                    if (splitUserInput[0] == "see")
                    {
                        if (splitUserInput[1] == "items" || splitUserInput[1] == "objects") { thing.SeeAll(player); }

                        if (splitUserInput[1] == "inventory") { thing.SeeInventory(player); }
                    }

                    if (splitUserInput[0] == "take" || splitUserInput[0] == "drop")
                    {
                        thing.ActionSorter(player, userInput);
                    }

                    if (splitUserInput[0] == "look" && splitUserInput[1] == "at")
                    {
                        if (splitUserInput[2] == "items") { thing.SeeAll(player); }
                        else { thing.LookAtItem(player, userInput); }
                    }
                }
                else // if user types one word
                {
                    if (userInput == "test")
                    {
                        //CreateFeedback(feedbackTypes.danger, "wow this is a test");
                        //CreateFeedback(feedbackTypes.success, "wow this is a test");
                        //ConsoleColor[] consoleColors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

                        //Console.WriteLine("List of available Console Colors:");
                        //foreach (var color in consoleColors)
                        //    Console.WriteLine(color);
                    }
                    if (userInput == "look")
                    {
                        Console.WriteLine("Nothing much to see...");
                    }
                    if (userInput == "inventory")
                    {
                        thing.SeeInventory(player);
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
