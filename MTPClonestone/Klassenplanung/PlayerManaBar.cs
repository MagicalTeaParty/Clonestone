using System.Collections.Generic;

namespace Klassenplanung
{
    class PlayerManaBar : ManaBar
    {
        //Fields
        List<ManaCrystal> manyCrystals;
        byte maxManaCrystals;
        ManaOverload manaOverload;
        byte overload;
        byte currentOverload;
    }
}