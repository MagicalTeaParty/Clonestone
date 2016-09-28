using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klassenplanung
{
    interface IOnBoard
    {
        //Properties
        int AttackValue { get; }
        int CurrentAttackValue { get; set; }
        int HealthValue { get; set; }
        int CurrentHealthValue { get; set; }
        int CurrentMaxHealthValue { get; set; }
        //-1 = Hero, 0 = Weapon, 1-7 = Minions on Board
        int RoundsOnBoard { get; set; }
        int Position { get; set; }
    }
}
