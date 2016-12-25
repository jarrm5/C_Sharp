using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;

namespace CharacterLibrary
{
    public class Player : Combatant
    {
        //Field/Property for a list of items the player can pick up when defeating an enemy
        //Will need another random to generate what items an enemy will drop

        public Player(string name, CharacterType charType, int maxHealth, Weapon wep) : base(name, charType, maxHealth, wep) 
        { 
            KillCount = 0; 
        }

        //KillCount property unique to Player class only!
        //Killcount will be updated in the program if the player is victorious thru UpdateKillCount()
        public int KillCount { get; private set; }

        public void UpdateKillCount()
        {
            KillCount++;
        }

        //Let's write a method that will heal our player's health by 25% after each round
        //If 25% of our players health exceeds maxhealth, set the health equal to maxHealth
        public override void Heal()
        {
            int healAmount = Convert.ToInt32(MaxHealth * .25m);

            //Our 25% heal exceeds MaxHealth, heal up the the MaxHealth
            if ((Health + healAmount) > MaxHealth)
            {
                healAmount = (MaxHealth - Health);
            }

            Health += healAmount;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have reached a checkpoint.\nYou will be healed by at most 25% every other round from now on.");
            Console.WriteLine("{0} was healed {1}!\n", Name, healAmount);
            Console.ResetColor();
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
