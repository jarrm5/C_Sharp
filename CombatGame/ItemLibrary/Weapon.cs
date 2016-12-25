using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibrary
{
    public class Weapon : Item
    {
        //properties relating to weapon stats
        //These will vary according to weapontype - better weapons will have stronger stats.

        //Constructors: We do not want a name for the weapon; its name will be the WepType property
        //But we still want a Weapon to inherit from Item because both will be things that can be 
        //traded for money.

        //All weapons have a floor minDamage of 0
        //But the chances of low hits will decrease with better weapons and better skill level

        //Again, do we want a weaponfactory class for handling weapon creation based on weaponType?

        //For our Weapon constructor, we should just be able to call a simple factory method from our program,
        //and let the WeaponsFactory class give us a random weapon with the appropriate probabilities
        public Weapon(WeaponType wepType, int maxDamage) : base(wepType.ToString())
        {
            WepType = wepType;
            MaxDamage = maxDamage;
        }

        //Random for the WeaponsFactory Class
        public static Random _randGen = new Random();

        public WeaponType WepType { get; private set; }
        public int MaxDamage { get; private set; }
        
        public override string ToString()
        {
            return string.Format("{0} (0 - {1} damage)", 
                WepType, MaxDamage);
        }
    }
}
