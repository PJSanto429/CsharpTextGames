using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using static Text_Game.Program;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Text_Game
{
    internal class Room
    {
        public static List<Room> instances = new List<Room>();

        private string name;
        private string description;
        public rooms Id;
        private Dictionary<bool, rooms> directions;

        // basic getters and setters
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Dictionary<bool, rooms> Directions
        {
            get { return directions; }
            set { directions = value; }
        }

        //creating room(__init__)
        public Room(
            string Aname,
            string Adescription,
            rooms AId,
            Dictionary<bool, rooms> Adirections
        )
        {
            instances.Add(this);
            Name = Aname;
            Description = Adescription;
            Id = AId;
            Directions = Adirections;
        }

        //all other functions and stuff

        public Room GetOneRoomByID(Player player)
        {
            foreach (Room room in instances)
            {
                if (room.Id == player.room)
                {
                    return room;
                }
            }
            return this;
        }

        public void Move(Player player, string direction)
        {

        }
    }
}
