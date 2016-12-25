using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibrary
{
    public class Item
    {
        //We do not want a name for the weapon; its name will be the WepType property
        //But we still want a Weapon to inherit from Item because both will be things that can be 
        //traded for money.
        public Item(string name) 
        {
            Name = name; 
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return string.Format("ITEM INFO\nName: {0}", Name);
        }
    }
}
