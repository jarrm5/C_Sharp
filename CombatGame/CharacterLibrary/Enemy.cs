using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;

namespace CharacterLibrary
{
    public class Enemy : Combatant
    {
        //Field/Property for a list of items the enemy will drop upon death
        //Will need another random to generate what items an enemy will drop

        /* Tougher enemies will drop more valuable shit
         * So there will be a heirarchy of weapons/items
         * 
         * */
        
        //For now, let's just have this class generate a "nerfed" enemy (significantly lower health)
        //so our player will have a chance to easily defeat an enemy
        //Our enemy for now will also always get Brass Knuckles through the factory method
        public Enemy(string name, CharacterType charType, int maxHealth, Weapon equippedWeapon) 
            :base(name, charType, maxHealth, equippedWeapon) { }

        public override string ToString()
        {
            return base.ToString();
        }
        public override void Heal()
        {
            Health = MaxHealth;
        }
    }
}
