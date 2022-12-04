using System;
using System.Collections.Generic;
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

        //creating room(__init__)
        public Room(
            string Aname,
            string Adescription,
            rooms AId
        )
        {
            instances.Add(this);
            Name = Aname;
            Description = Adescription;
            Id = AId;
        }

        //all other functions and stuff

        public Room GetOneRoomByID(Player player)
        {
            foreach(Room room in instances)
            {
                if (room.Id == player.room)
                {
                    return room;
                }
            }

            return this;
        }
    }
}
