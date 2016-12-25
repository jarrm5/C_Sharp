using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;

namespace CombatGame
{
    public class Wave
    {
        //**The Wave System**

        //Our program should construct the wave first so the player can preview what enemies are involved in each wave.
        //When the wave is constructed, it should recieve the appropriate wave number, and a list of enemies as indexed by our dictionary.

        //The program should not engage in combat immediately, but wait for confirmation from the player.
        //Upon confirmation from the player, the engage method will be called,  and either the player or enemy will be attacked based
        //on our Health rule.
        //After succesfully completing a round, our player should automatically heal 25% of his health - this is a 
        //Heal method found in the player class.
        
        public Wave()
        {
            //Creating a wave means we need to immediately populate the enemyList
            //Write a method to do this taking into account increasingly more difficult waves
            WaveNumber = ++_nextWaveNumber;
            WaveEnemyList = waveReference[WaveNumber];
        }

        //We will need a dictionary of Wave numbers and the List of enemies that corresponds to that wave number
        //Since our waves are specific, We can just call our GetSpecificEnemy method which will give us an Enemy AND weapon of our choosing
        //We will start out light, and as always we can add more waves later on.
        //Dictionary<int,List<Enemies>> waveReference

        private static Dictionary<int, List<Enemy>> waveReference = new Dictionary<int, List<Enemy>>()
        {
            //Wave 1: 2 Infantry w/ Brass Knuckles
            {1,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(0,0),CombatantFactory.GetSpecificEnemy(0,0)}},
            //Wave 2: 4 Infantry w/ Brass Knuckles
            {2,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(0,0),CombatantFactory.GetSpecificEnemy(0,0),
                 CombatantFactory.GetSpecificEnemy(0,0),CombatantFactory.GetSpecificEnemy(0,0)}},
            //Wave 3: 4 Infantry (2 w/ Brass Knuckles and 2 w/ Pistol)
            {3,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(0,1),CombatantFactory.GetSpecificEnemy(0,1),
                 CombatantFactory.GetSpecificEnemy(0,0),CombatantFactory.GetSpecificEnemy(0,0)}},
            //Wave 4: Sniper w/ AR15
            {4,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(1,1)}},
            //Wave 5: Sniper w/ Ar15, 2 Infantry (1 w/ Brass Knuckles, 1 w/ Pistol)
            {5,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(1,1), CombatantFactory.GetSpecificEnemy(0,1), 
                    CombatantFactory.GetSpecificEnemy(0,0)}},
            //Wave 6: Sniper w/ Ar15 X 2
            {6,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(1,1),CombatantFactory.GetSpecificEnemy(1,1)}},
            //Wave 7: Art x 1 w/ shotty, infantry x 2 w/ pistol
            {7,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(2,1), CombatantFactory.GetSpecificEnemy(0,1), 
                    CombatantFactory.GetSpecificEnemy(0,1)}},
            //Wave 8: Art x 2 (w/ shotty & w/ pistol)
            {8,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(2,1),CombatantFactory.GetSpecificEnemy(2,0)}},
            //Wave 9: Art x 2 w/ shotty, sniper w/ pistol
            {9,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(2,1), CombatantFactory.GetSpecificEnemy(2,1), 
                    CombatantFactory.GetSpecificEnemy(1,0)}},
            //Wave 10: juggy w/ rpg
            {10,new List<Enemy> 
                {CombatantFactory.GetSpecificEnemy(3,1)}},
        };

        //Wave numbers start at 1
        private static int _nextWaveNumber = 0;
        //Static member to store how many waves are possible
        //I need this number in the program as a check to see if the player has beaten the game.
        //This number becomes flexible if I decide to add more waves.

        public int WaveNumber { get; private set; }
        public static int NextWaveNumber { get { return _nextWaveNumber; } private set { _nextWaveNumber = value;} }
        public static int NbrOfPossibleWaves { get { return waveReference.Count; } }
        public List<Enemy> WaveEnemyList { get; private set; }

        public static void ResetWavesToZero()
        {
            NextWaveNumber = 0;
        }

        public override string ToString()
        {
            string output = string.Format("Wave {0}: ({1} Enemies)\n", WaveNumber, WaveEnemyList.Count);

            foreach (Enemy e in WaveEnemyList)
            {
                output += string.Format("{0} {1} ({2}/{3}) using {4}\n", e.CharType, e.Name, e.Health, e.MaxHealth,e.EquippedWeapon.Name);
            }

            return output;
        }
    }
}
