using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
        public string room;

        public Object(
            string Aname,
            string AlongName,
            string Aroom,
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

        public Object GetAllItemsFromInput(
            string userInput,
            Player player,
            // first one says whether to check, and the second is the rule to check
            // eg. check for items not in inventory || dont check for items in room
            Tuple<bool, bool> inventoryFilter, // check, rule
            Tuple<bool, bool> roomFilter // check, rule
        ) {
            List<Object> foundItems = new List<Object>();
            foreach (Object obj in instances)
            {
                if (obj.name == userInput || obj.longName == userInput) {
                    if (
                        (inventoryFilter.Item1 && obj.inInventory == inventoryFilter.Item2) ||
                        (roomFilter.Item1 && obj.room == player.room)
                    ) {
                        foundItems.Add(obj);
                    }
                }
            }

            if (foundItems.Count == 0)
            {
                return this;
            }
            else if (foundItems.Count == 1)
            {
                return foundItems[0];
                //Console.WriteLine(foundItems[0].Description);
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
                    //Console.WriteLine(foundObject.Description);
                }
                //else { Console.WriteLine(cantSee); }
                else { return this; }
            }
        }

        public void ActionSorter(string userInput, string action)
        {

        }
                                                                
        public void SeeInventory()
        {
            //Object[] inventory = new Object[instances.Count];
            List<Object> inventory = new List<Object>();
            foreach(Object item in instances)
            {
                if (item.inInventory)
                {
                    inventory.Add(item);
                }
            }
            Console.Write("Your inventory consists of: ");
            foreach(Object item in inventory)
            {
                if (item != inventory.Last())
                {
                    Console.Write($"{item.LongName}, ");
                }
                else
                {
                    Console.WriteLine($"and {item.LongName}");
                }
            }
        }

        public void SeeAll()
        {
            Console.WriteLine("You can see:");
            foreach (Object i in instances)
            {
                if (!(i.Name == "null"))
                {
                    Console.WriteLine(i.LongName);
                }
            }
        }

        //look at object based on name
        public void LookAtItem(string userInput)
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

            List<Object> foundItems = new List<Object>();
            foreach (Object obj in instances)
            {
                if (obj.name == userInput || obj.longName == userInput)
                {
                    foundItems.Add(obj);
                }
            }
            if (foundItems.Count == 0)
            {
                Console.WriteLine(cantSee);
            }
            else if (foundItems.Count == 1)
            {
                Console.WriteLine(foundItems[0].Description);
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
                    Console.WriteLine(foundObject.Description);
                }
                else { Console.WriteLine(cantSee); }
            }
        }
    }

    internal class Player
    {
        public string room = "first";
    }
}
