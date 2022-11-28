using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Game
{
    internal class Object
    {
        private string name;
        private string longName;
        private string description;
        private string takeable;
        public string inInventory;
        public string room;

        public Object(
            string Aname,
            string AlongName,
            string Adescription,
            string Aroom,
            string Atakeable,
            string AinInventory
        )
        {
            name = Aname;
            longName = AlongName;
            description = Adescription;
            room = Aroom;
            takeable = Atakeable;
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

        public string Takeable
        {
            get { return takeable; }
            set { takeable = value; }
        }
    }

    internal class Player
    {
        public string room = "first";
    }
}
