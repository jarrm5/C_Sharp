using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterLibrary
{
    public class CombatInstance
    {
        public CombatInstance(Combatant a, Combatant d)
        {
            attacker = a;
            defender = d;

            //Creating a combat instance means we will immediately engage in combat
            this.DoCombat();
        }

        private Combatant attacker;
        private Combatant defender;

        //Recursive Combat method
        public void DoCombat()
        {
            attacker.Attack(defender);
            if (attacker.Health > 0 && defender.Health <= 0)
            {
                //attacker wins

                //Player was the attacker and defeated the enemy, update player's killcount
                Player p = attacker as Player;
                if (p != null)
                {
                    p.UpdateKillCount();
                }
                Console.WriteLine(attacker.Name + " has defeated " + defender.Name + "\n");
                return;
            }

            defender.Attack(attacker);
            if (defender.Health > 0 && attacker.Health <= 0)
            {
                //defender wins

                //Enemy was the attacker but player defeated him; update player's killcount
                Player p = defender as Player;
                if (p != null)
                {
                    p.UpdateKillCount();
                }
                Console.WriteLine(defender.Name + " has defeated " + attacker.Name + "\n");
                return;
            }

            //otherwise keep engaging in combat
            DoCombat();
        }
    }
}
