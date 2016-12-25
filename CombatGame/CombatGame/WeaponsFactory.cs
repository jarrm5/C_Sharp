using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using CharacterLibrary;

namespace CombatGame
{
    public class WeaponsFactory
    {
        //Weapons Factory - a purely static class that is reponsible for creating weapons with its corresponding stats

        //First, we need a dictionary of Weapon Stats Dict<WepType weapon, int maxDamage>
        //We will not worry about HitChance here, we will let the Combat system deal with that.    
        private static Dictionary<WeaponType, int> weaponStats = new Dictionary<WeaponType, int>()
        {
            { WeaponType.BrassKnuckles, 20 },
            { WeaponType.Pistol, 50 },
            //Shotgun will have high damage for a middle tier weapon, but its hitChance will make it very rare to
            //hit that high - again we'll handle that later on when we do combat
            { WeaponType.Shotgun, 300 }, 
            { WeaponType.AR15, 100 }, 
            { WeaponType.RPG, 750 }
        };

        //Put the dictionary weapontype keys into a list to get an index match
        public static List<WeaponType> wepTypes = new List<WeaponType>(weaponStats.Keys);

        //Get a weapon of your choice by using an index
        public static Weapon GetSpecificWeapon(int index)
        {
            //return a weapon of type indexed by the program, and its corresponding max hit in the dictionary
            return new Weapon(wepTypes[index], weaponStats[wepTypes[index]]);
        }

        //We need to write a new method that takes in a chartype as input, then generates a random weapon based on 
        //what weapons that type is able to be created with.
        //We will need to borrow the enemyWepsDict from the EnemyFactory in generating the appropriate weapons for each combatant type.
        //If we want different probabilities for each enemy having a different weapon on creation, this is going to be a big ass
        //method with a switch/if structure
        public static Weapon GetWeaponByCharType(CharacterType charType, Dictionary<CharacterType, List<WeaponType>> enemyWepsDict)
        {
            //First get a random number between 1 and 100 to determine what weapon out enemy will get
            //(remember that this depends on which combatant type they are)
            int wepChance = Weapon._randGen.Next(1, 101);

            //Set up a switch statement dependent on the charType
            //Used if statements to determine weapon probabilites per combatant type

            /* 
             * Infantry: Brass Knuckles (40%), Pistol (60%)
             * Sniper : Pistol (25%), Ar15 (75%)
             * Artillery : Pistol (70%), Shotgun (30%)
             * Juggernaught : Ar15 (80%), RPG (20%)
             * 
             * */

            switch (charType)
            {
                case CharacterType.Infantry:
                    if (wepChance < 41)
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Infantry][0],
                            weaponStats[enemyWepsDict[CharacterType.Infantry][0]]);
                    }
                    else
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Infantry][1],
                            weaponStats[enemyWepsDict[CharacterType.Infantry][1]]);
                    }
                case CharacterType.Sniper:
                    if (wepChance < 26)
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Sniper][0],
                            weaponStats[enemyWepsDict[CharacterType.Sniper][0]]);
                    }
                    else
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Sniper][1],
                            weaponStats[enemyWepsDict[CharacterType.Sniper][1]]);
                    }
                case CharacterType.Artillery:
                    if (wepChance < 71)
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Artillery][0],
                            weaponStats[enemyWepsDict[CharacterType.Artillery][0]]);
                    }
                    else
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Artillery][1],
                            weaponStats[enemyWepsDict[CharacterType.Artillery][1]]);
                    }
                case CharacterType.Juggernaught:
                    if (wepChance < 81)
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Juggernaught][0],
                            weaponStats[enemyWepsDict[CharacterType.Juggernaught][0]]);
                    }
                    else
                    {
                        return new Weapon(
                            enemyWepsDict[CharacterType.Juggernaught][1],
                            weaponStats[enemyWepsDict[CharacterType.Juggernaught][1]]);
                    }
            }
            //Method claims not all paths return a value so I trick the compiler :)
            throw new Exception("A weapon was never returned");
        }

        //Write yet another method that takes in weaponType as an argument and returns the appropriate maxDamage
        //The wave system will need this method when working with its own set of rules
        public static int GetWepMaxDamage(WeaponType wepType)
        {
            //Return the maxDamage number from the dictionary as referenced by the method argument
            return weaponStats[wepType];
        }
    }
}
