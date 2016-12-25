using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterLibrary;
using ItemLibrary;

/* MY COMBAT GAME!!!
 * Uses everything I have learned up to this point: classes, abstract classes, inheritence, polymorphism, interfaces, object containment, factoryclasses, recursion
 * Wrote this all on my own with no help - about 20 hours of coding over 5 days.
 * 
 * Began writing program and wrote most of the classes (6/8/15)
 * Wrote Weapons Factory; Succesfully implemented a simple combat system (6/9/15)
 *        : Player fight the same enemy over and over until dead
 * Wrote Combatant Factory (6/10/15)
 *        : Player fights randomly generated enemies
 *        : Enemies can only have certain weapons and different ranges of health
 * Succesfully implemented Wave system (6/11/15)
 *        : Program gives default player for now
 *        : Wave class created and defines 5 waves of increasing difficulty
 *              : Program is able to adjust without any additional code needed if more waves are added later
 *        : Program gives appropriate output for dying, quitting early, or beating the game
 * 
 *        TODO/FIX : For each wave, enemies of the same type all have the same randomly generated health (we want different health
 *                   amounts for the same type of enemies). - DONE (6/11)
 *                 : Auto Heal the player by 10% on odd Wave numbers after Wave 2  - DONE (6/11)
 *                 : In the attack method, have different verbs for different weapon types (i.e. if attacker is using BrassKnuckles,
 *                   have attack message say "Player x bashed Enemy y"). - DONE (6/11)
 *                 : *** The console output needs an overhaul; it's hard to follow ***
 *                       Have player stats and enemy stats, then have the damage at the bottom
 *                 : Wave bug - Some enemies aren't getting the weapons they are supposed to get. - DONE (6/11)
 * 6/12/15 - Objectives
 *              : We will definately want to create a user-friendly prompts to let the user choose player attributes - DONE 6/12
 *                  : Add checks to make sure user enters valid numerical weapon choices in the prompts - DONE (6/12)
 *              : If the user beats the game, ask user if he wants to play again, saving the players name, but asking for different
 *                charTypes and weapon choices.
 *                      :Bugs: Juggernaught wont take an RPG - FIXED (6/12)
 *                           : Beat the game once then try to select another char, itll say you beat the game after 0 rounds. FIXED (6/12)
 *                           : Enemies have no health after 1 time thru the game! FIXED (6/12)
 *              : Code clean up - get rid of some methods we don't use in the factory classes - DONE (6/12)
 *              : Let's also add more waves - DONE - ADDED UP TO 10 WAVES (6/12) 
 * 6/12/15 - GAME COMPLETE
 * 
 * */

namespace CombatGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "-=-=-=-= COMBAT GAME -=-=-=-";

            Console.WriteLine("Welcome to Jim's Combat Game\n");

            //We will want to create a user-friendly set of prompts to allow our user to build his own character
            //The player variable will be named "p"

            //We will ask for a name, choose from a list of charTypes, assign a random health amount based on that choice, and
            //choose from a list of weapons

            Console.Write("First, what is your name? ");
            string playerName = Console.ReadLine();

            //Insert play again loop here
            bool keepPlaying = true;

            while (keepPlaying)
            {
                Console.Clear();

                List<string> playerArgs = new List<string>();
                bool goodInput = false;

                while (!goodInput)
                {
                    playerArgs.Add(playerName);

                    foreach (string prompt in GetPrompts())
                    {
                        Console.Write(prompt);
                        string playerInput = Console.ReadLine();
                        playerArgs.Add(playerInput);
                        Console.Clear();
                    }

                    for (int i = 1; i < playerArgs.Count; )
                    {
                        int argIndex = Convert.ToInt32(playerArgs[i]);

                        if (char.IsDigit(Convert.ToChar(playerArgs[i])))
                        {
                            switch (i)
                            {
                                case 1:
                                    if (argIndex <= CombatantFactory.enemyTypes.Count && argIndex > 0)
                                    {
                                        i++;
                                        continue;
                                    }
                                    break;
                                case 2:
                                    if (argIndex <= WeaponsFactory.wepTypes.Count && argIndex > 0)
                                    {
                                        i++;
                                        continue;
                                    }
                                    break;
                            }     
                        }
                        playerArgs.Clear();
                        Console.WriteLine(playerName + " gave bad input; press enter to try again.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    }
                    if (playerArgs.Count == 3)
                        goodInput = true;
                }

                Player p = CombatantFactory.CreateCustomPlayer(playerArgs);

                Console.WriteLine("Preview " + p.Name + "'s stats");
                Console.WriteLine("\n" + p + "\n");
                Console.WriteLine("Press enter when you're done..");
                Console.ReadLine();

                do
                {
                    Wave w;

                    Console.Clear();

                    //first, we need to check if the player has beat all the waves before generating a wave
                    //We check this by evaluating if the next wave number is greater than the number of waves we have referenced
                    //in our dictionary in the wave class.
                    //This is better than hard coding a number - this check makes it possible to add more waves later if we 
                    //want without altering any program code.
                    if (Wave.NextWaveNumber + 1 > Wave.NbrOfPossibleWaves)
                    {
                        Console.WriteLine("{0} beat the damn game! Cocaine and strippers for all!", p.Name);
                        Console.WriteLine();
                        PrintFinalResults(p, Wave.NextWaveNumber);

                        keepPlaying = PlayAgain();
                        
                        //Break out of the wave loop
                        break;
                    }

                    //After the player is created, create a wave of enemies
                    //The wave class' static member will iterate wave by wave; 
                    w = new Wave();

                    //Bug fixed: Make sure the enemies are all healed
                    //This is here because if player beats the game once, the enemies will have 0 health still in the next game
                    foreach (Enemy e in w.WaveEnemyList)
                    {
                        e.Heal();
                    }

                    //On odd wave numbers after wave 2, heal the player by 25%
                    if (w.WaveNumber > 2 && w.WaveNumber % 2 == 1)
                    {
                        p.Heal();
                    }

                    //show the player a preview of what is to come.
                    Console.WriteLine(w + "\n");


                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} has {1}/{2} health remaining.", p.Name, p.Health, p.MaxHealth);
                    Console.ResetColor();

                    //Ask the player if they would like to go through with the wave
                    Console.Write("Would you like to take on these enemies? [Y/N] ");
                    string confirmWave = Console.ReadLine().ToUpper();

                    //If the player wishes to engage the enemies...
                    if (string.Compare(confirmWave, "Y") == 0)
                    {
                        Console.Clear();

                        Console.WriteLine("\t\t-=-=- WAVE {0} -=-=-\n", w.WaveNumber);
                        //The player has agreed to do battle at this point
                        //Now we must determine who will engage who.
                        //Whoever has the higher current health will get the first attack
                        //To do this, we need a for loop to index the enemies in the wave list
                        for (int i = 0; i < w.WaveEnemyList.Count; i++)
                        {
                            //If you are "healthier" than your adversary, you attack first, else the enemy attacks you first
                            //Remember, engaging means attacking immediately and keep attacking recursively until someone is dead

                            //If you are victorious, you will keep attacking enemies until the current wave is over, after which 
                            //you will be asked if you want to proceed to the next wave.
                            if (p.Health > w.WaveEnemyList[i].Health)
                            {
                                p.Engage(w.WaveEnemyList[i]);
                            }
                            else
                            {
                                w.WaveEnemyList[i].Engage(p);
                            }

                            if (p.Health == 0)
                            {
                                Console.WriteLine("You are dead. Press enter to see your results...");
                                Console.ReadLine();

                                //Exit early from the loop
                                break;
                            }
                        }
                        //Player is dead, show him results but don't give him more waves
                        if (p.Health == 0)
                        {
                            Console.Clear();
                            //Game finished; display the final kill count
                            //You died during a round, does not count as a completed round
                            PrintFinalResults(p, w.WaveNumber - 1);

                            Console.WriteLine();

                            keepPlaying = PlayAgain();

                            //Get out of the wave loop
                            break;
                        }
                        //Player completed a wave, return to the top to get the next wave
                        else
                        {
                            Console.WriteLine("Congratulations! You survived wave {0}! Press enter to continue..", w.WaveNumber);
                            Console.ReadLine();
                        }
                    }
                    //Player has quit early, show the results and break out of loop; 
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You opted to quit early.\n");

                        //Subtract 1 from the current round because the current round was never completed
                        PrintFinalResults(p, w.WaveNumber - 1);

                        Console.WriteLine();

                        //Done playing.
                        keepPlaying = false;

                        //Get out of the wave loop
                        break;
                    }
                } while (true);
            }      
        }
        public static void PrintFinalResults(Player p, int waveNumber)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-=-=- {0} -=-=-\nKills: {1}\nWeapon Used: {2}\nWaves completed: {3}\nFealth (final health): {4}/{5}", 
                                p.Name, p.KillCount, p.EquippedWeapon.Name, waveNumber, p.Health, p.MaxHealth);
            Console.ResetColor();
        }

        //This method is responsible for printing out all of our questions to the user when creating a custom player
        public static IEnumerable<string> GetPrompts()
        {
            string[] playerPrompts = 
            { 
                string.Format("Please select a Character type from the list: \n\n"),
                string.Format("Please select a weapon from the list: \n\n")
            };

            for (int i = 0; i < playerPrompts.Length; i++)
            {
                int index = 1;

                switch (Array.IndexOf(playerPrompts, playerPrompts[i]))
                {
                    case 0:
                        foreach (CharacterType ct in CombatantFactory.enemyTypes)
                        {
                            playerPrompts[i] += string.Format("{0}: {1}\n", index, ct);
                            index ++;
                        }
                        playerPrompts[i] += "\nSelection: ";
                        break;
                    case 1:
                        foreach (WeaponType wt in WeaponsFactory.wepTypes)
                        {
                            playerPrompts[i] += string.Format("{0}: {1}\n", index, wt);
                            index++;
                        }
                        playerPrompts[i] += "\nSelection: ";
                        break;
                }          
            }
            foreach (string p in playerPrompts)
            {
                yield return p;
            }
        }
        public static bool PlayAgain()
        {
            Console.WriteLine();
            Console.Write("Play again? [Y/N] ");
            string playAgain = Console.ReadLine();

            if (string.Compare(playAgain.ToUpper(), "Y") != 0)
            {
                return false;
            }

            Wave.ResetWavesToZero();

            return true;        
        }
    }
}
