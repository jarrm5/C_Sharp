using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;

namespace CharacterLibrary
{
    public interface ICombatable
    {
        Weapon EquippedWeapon { get; set; }

        CombatInstance Engage(Combatant c);

        void Attack(Combatant target);

        void Heal();
    }
}
