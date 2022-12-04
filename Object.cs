using System;
using System.Collections.Generic;
using System.Linq;
using static Text_Game.Program;
//using System.Diagnostics.PerformanceData;
//using System.IO.MemoryMappedFiles;
//using System.Runtime.CompilerServices;
//using System.Security.Claims;
//using System.Security.Cryptography.X509Certificates;
//using System.Security.Policy;
//using System.Text;
//using System.Threading.Tasks;

namespace Text_Game
{
    internal class Object
    {
        public static List<Object> instances = new List<Object>();
        public string cantSee = "Hmm, I can't see that";

        private string name;
        private string longName;
        private string description;
        private bool takeable;
        public bool inInventory;
        public rooms room;

        // basic getters and setters for names, desc, etc...
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string LongName
        {
            get { return longName; }
            set { longName = value; }

        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool Takeable
        {
            get { return takeable; }
            set { takeable = value; }
        }

        // creating the object(def __init__)
        public Object(
            string Aname,
            string AlongName,
            rooms Aroom,
            string Adescription,
            bool Atakeable,
            bool AinInventory
        )
        {
            instances.Add(this);
            Name = Aname;
            LongName = AlongName;
            room = Aroom;
            Description = Adescription;
            Takeable = Atakeable;
            inInventory = AinInventory;
        }
        // ================= all other stuff ========================

        public Object[] GetAll()
        {
            return instances.ToArray();
        }

        //get objcet by item name
        public Object GetItemByName(string itemName)
        {
            foreach (Object item in instances)
            {
                if (item.LongName == itemName)
                {
                    return item;
                }
            }
            return this;
        }

        public Object GetOneItemFromInput(
            Player player,
            string userInput,
            Tuple<bool, bool> invFilter,
            Tuple<bool, bool> roomFilter,
            Tuple<bool, bool> takeableFilter
        )
        {
            List<Object> foundItems = GetAllItemsFromInput(player, invFilter, roomFilter, takeableFilter, userInput);

            if (foundItems.Count == 0)
            {
                return this;
            }
            else if (foundItems.Count == 1)
            {
                return foundItems[0];
            }
            else
            {
                Console.WriteLine("Which did you mean?");
                foreach (Object thing in foundItems)
                {
                    Console.WriteLine(thing.LongName);
                }
                string userChoice = Console.ReadLine();
                Object foundObject = GetItemByName(userChoice);
                if (!(foundObject.LongName == "null"))
                {
                    return foundObject;
                }
                else { return this; }
            }
        }

        public List<Object> GetAllItemsFromInput(
            Player player,
            // first one says whether to check, and the second is the rule to check
            // eg. check for items not in inventory || dont check for items in room
            Tuple<bool, bool> invFilter, // check, rule
            Tuple<bool, bool> roomFilter, // check, rule
            Tuple<bool, bool> takeableFilter,
            string name = "none"
        )
        {
            List<Object> foundItems = new List<Object>();
            List<Object> returnItems = new List<Object>();

            foreach (Object obj in instances)
            {
                if (!(obj.Name == "null"))
                {
                    if (invFilter.Item1)
                    {
                        if ((obj.inInventory == invFilter.Item2) && !foundItems.Contains(obj))
                        {
                            foundItems.Add(obj);
                        }
                    }
                    if (roomFilter.Item1)
                    {
                        bool roomIsSame = obj.room == player.room;
                        if ((roomIsSame == roomFilter.Item2)  && !foundItems.Contains(obj))
                        {
                            foundItems.Add(obj);
                        }
                    }
                    if (takeableFilter.Item1)
                    {
                        if ((obj.takeable == takeableFilter.Item2) && !foundItems.Contains(obj))
                        {
                            foundItems.Add(obj);
                        }
                    }
                }
            }
            if (name != "none")
            {
                foreach (Object obj in foundItems)
                {
                    if (obj.Name == name || obj.LongName == name)
                    {
                        returnItems.Add(obj);
                    }
                }
                return returnItems;
            }
            return foundItems;
        }

        public void ActionSorter(Player player, string userInput)
        {
            if (userInput.Contains("take"))
            {
                userInput = userInput.Replace("take ", "");
                TakeItem(player, userInput);
            }
            else if (userInput.Contains("drop"))
            {
                userInput = userInput.Replace("drop ", "");
                DropItem(player, userInput);
            }
        }

        public void DropItem(Player player, string item)
        {
            Object itemToDrop = GetOneItemFromInput(player, item, Tuple.Create(true, true), Tuple.Create(true, false), Tuple.Create(true, true));

            if (itemToDrop.Name != "null")
            {
                Console.WriteLine($"\nYou have dropped {itemToDrop.LongName}");
                itemToDrop.inInventory = false;
                itemToDrop.room = player.room;
            }
        }

        public void TakeItem(Player player, string item)
        {
            Object itemToTake = GetOneItemFromInput(player, item, Tuple.Create(true, false), Tuple.Create(true, true), Tuple.Create(true, true));

            if (itemToTake.Name != "null")
            {
                if (itemToTake.takeable)
                {
                    Console.WriteLine($"\nYou have taken {itemToTake.LongName}");
                    itemToTake.inInventory = true;
                    itemToTake.room = rooms.inventory;
                }
                else
                {
                    Console.WriteLine($"You are unable to take {itemToTake.LongName}");
                }
            }
        }
                                                                
        public void SeeInventory(Player player)
        {
            List<Object> inventory = GetAllItemsFromInput(player, Tuple.Create(true, true), Tuple.Create(false, false), Tuple.Create(false, false));

            Console.WriteLine("Your inventory consists of: ");
            if (!Convert.ToBoolean(inventory.Count))
            {
                Console.WriteLine("Nothing yet...");
            }
            foreach(Object item in inventory) { Console.WriteLine(item.LongName); }
        }

        public void SeeAll(Player player)
        {
            List<Object> roomItems = GetAllItemsFromInput(player, Tuple.Create(true, false), Tuple.Create(true, true), Tuple.Create(true, true));
            Console.WriteLine("You can see:");
            foreach (Object i in roomItems)
            {
                Console.WriteLine(i.LongName);
            }
        }

        //look at object based on name
        public void LookAtItem(Player player, string userInput)
        {
            //removing 'look at' from userInput
            string[] charToRemove = { "look", "at" };
            foreach (string charToRemoveItem in charToRemove)
            {
                if (userInput.Contains(charToRemoveItem))
                {
                    userInput = userInput.Replace(charToRemoveItem, "");
                }
            }
            userInput = userInput.Trim();
            //Console.WriteLine("--------------------------------------------");
            //Console.WriteLine(userInput);
            //Console.WriteLine("--------------------------------------------");

            Object oneItem = GetOneItemFromInput(player, userInput, Tuple.Create(true, true), Tuple.Create(true, true), Tuple.Create(true, true));
            if (!(oneItem.Name == "null"))
            {
                Console.WriteLine(oneItem.Description);
            }
            else { Console.WriteLine(cantSee); }
        }
    }

    internal class Player
    {
        public rooms room = rooms.first;
    }
}
