using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;

namespace CharacterLibrary
{
    public abstract class Combatant : Character, ICombatable
    {
        //For now, (no skill/experience system implemented yet) just generate an infantry enemy/player
        //Different charTypes will have different amounts of health based on their type..
        //(Do we want a seperate factoryclass to handle this?)
        //For now, we will have the Player and enemy class handle health, but it will get assigned here     
        protected Combatant(string name, CharacterType charType, int maxHealth, Weapon equippedWeapon) 
            : base(name, charType)
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
            EquippedWeapon = equippedWeapon;
        }

        //Random to generate our combatant's health
        //Also borrowed this object in my enemy factory class
        public static Random _randGen = new Random();

        //Health will be altered through combat methods instead of directly in the program..
        //Should be private or protected
        public int Health { get; protected set; }
        //May have to change this later to private we decide to handle health here
        //Right now it is being handled in the Player/Enemy class but it gets assigned here
        public int MaxHealth { get; protected set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})\nHealth: {2}/{3}\nWeapon: {4}", 
                base.ToString(), GetType(), Health, MaxHealth,EquippedWeapon.Name);
        }

        #region ICombatable Implementation

        //Player will get the first randomly generated weapon
        //Enemy will get weapons based on their CharacterType
        public Weapon EquippedWeapon { get; set; }

        public CombatInstance Engage(Combatant c)
        {
            return new CombatInstance(this, c);
        }
        public void Attack(Combatant target)
        {
            //This is just a dictionary with different verbs corresponding to different weapons
            //We'll use this when we need a different verb for each weapon type used against the target
            Dictionary<WeaponType, string> weaponVerbs = new Dictionary<WeaponType, string>()
            {
               {WeaponType.BrassKnuckles, "decked"},
               {WeaponType.Pistol, "capped"},
               {WeaponType.Shotgun, "pumped"},
               {WeaponType.AR15, "sniped"},
               {WeaponType.RPG, "obliterated"},
            };

            //Calculate the damage
           int damage = _randGen.Next(EquippedWeapon.MaxDamage + 1);

           //Don't generate hits greater than the combatant's current health 
           if (damage > target.Health)
           {
               damage = target.Health;
           }

           //Subtract damage from the other combatant
           target.Health -= damage;
            
           //ternary op: obejcts of type enemy will get red text, players get green text
           Console.ForegroundColor = this is Enemy ? ConsoleColor.Red : ConsoleColor.Green;
           
           Console.WriteLine("{0} {1} {2} for {3} damage using {4} ({5}/{6})", 
               Name, weaponVerbs[this.EquippedWeapon.WepType], target.Name, damage, EquippedWeapon.Name, target.Health, target.MaxHealth);
           Console.ResetColor();
        }

        public abstract void Heal();

        #endregion
    }
}
