using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using ItemLibrary;

namespace CombatGame
{
    public class CombatantFactory
    {
        //This class is responsible for generating enemies based on probabilities - very similar to the weapons factory class

        //Let's create a dictonary with all our enemy types and their maxHealth
        //We will plug these low and high numbers into our Enemy factory methods when generating random amounts of health

        //NOTE: you cannot call the random.next(lo, hi) directly as a value to each key in this dictionary - IT WILL NOT BE RANDOM!
        //You must call the _randGen outside the dict in order for a group of enemies of the same type to have different health amounts
        private static Dictionary<CharacterType, int[]> enemyHealthDict = new Dictionary<CharacterType, int[]>()
        {
            { CharacterType.Infantry, new int[] {40,61}}, //An infantry enemy will get health between 40-60
            { CharacterType.Sniper, new int[] {70,86}}, //70-85
            { CharacterType.Artillery,  new int[] {120,161}}, //120-160
            { CharacterType.Juggernaught, new int[] {275, 301}}, //275-300
        };

        //Create a list of CharacterTypes, then use its index to reference the dictionary
        public static List<CharacterType> enemyTypes = new List<CharacterType>(enemyHealthDict.Keys);

        //An array to use for the enemy's names
        private static string[] enemyNames = {"Shooter McGavin", "Mike Smart", "Rodney Dangerfield", "Sam Smith", "Brendan Murphy"};

        //Let's create another dictionary with all our enemy types and a list of weapons each type CAN have
        //We'll let the weaponsfactory determine the probability that each enemy type will start off with which weapon type
        private static Dictionary<CharacterType, List<WeaponType>> enemyWepsDict = new Dictionary<CharacterType, List<WeaponType>>()
        {
            { 
                CharacterType.Infantry, 
                new List<WeaponType>{WeaponType.BrassKnuckles, WeaponType.Pistol} 
            },
            { 
                CharacterType.Sniper, 
                new List<WeaponType>{WeaponType.Pistol, WeaponType.AR15}
            },
            { 
                CharacterType.Artillery, 
                new List<WeaponType>{WeaponType.Pistol, WeaponType.Shotgun} 
            }, 
            { 
                CharacterType.Juggernaught, 
                new List<WeaponType>{WeaponType.AR15, WeaponType.RPG}
            }, 
        };

        //We will use this method to implement our Wave system
        //Each Wave has a specific set of enemies using a specific weapon that the player will confront  
        public static Enemy GetSpecificEnemy(int enemyIndex, int weaponIndex)
        {
            //Get a specific enemy with a specific weapon as indexed by method arguments
            return new Enemy(
                //Assign the enemy a random name from our array of names
                enemyNames[Combatant._randGen.Next(enemyNames.Length)],

                //Character type as indexed by the first method arg
                enemyTypes[enemyIndex],

                //we HAVE to call the _randGen outside the dict to generate true random numbers
                //We do that by referencing the lo/hi values of the int array values in the enemyHealthDict
                Combatant._randGen.Next(enemyHealthDict[enemyTypes[enemyIndex]][0], enemyHealthDict[enemyTypes[enemyIndex]][1]),

                //Weapon as indexed by the second method arg (Note: do not call a WeaponsFactoryMethod, with the wave system, we are bound by
                //rules.. So we must reference the weapons lists in the enemyWepsdict b/c certain types of enemies can only have certain weapons
                new Weapon(enemyWepsDict[enemyTypes[enemyIndex]][weaponIndex], WeaponsFactory.GetWepMaxDamage(enemyWepsDict[enemyTypes[enemyIndex]][weaponIndex])));         
        }
        //Method to build player - Take in a list of string args and return a player based on user input
        public static Player CreateCustomPlayer(List<string> playerArgs)
        {
            return new Player(
                //string name from the list
                playerArgs[0],
                //indexes in the program are 1 based, subtract 1 to get the appropriate chartype from the list
                enemyTypes[Convert.ToInt32(playerArgs[1]) - 1],
                //Get a random amount of health based on the user's charType selection
                Combatant._randGen.Next(enemyHealthDict[enemyTypes[Convert.ToInt32(playerArgs[1]) - 1]][0], enemyHealthDict[enemyTypes[Convert.ToInt32(playerArgs[1]) - 1]][1]),
                //Get the weapon selected by the user and its corresponding max damage from weaponsfactory class
                new Weapon(WeaponsFactory.wepTypes[Convert.ToInt32(playerArgs[2]) - 1], WeaponsFactory.GetWepMaxDamage(WeaponsFactory.wepTypes[Convert.ToInt32(playerArgs[2]) - 1])));
        }
    }
}
