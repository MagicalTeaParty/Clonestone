using System.Collections.Generic;

namespace Klassenplanung
{
    abstract class Card
    {
        //Enumerations
        enum CardType { minion, spell, weapon, hero }
        enum CharClass { neutral, warrior, rogue, mage, paladin, shaman, druid, warlock, hunter, priest }
        enum Owner { activePlayer, enemy}
        enum Status { inDeck, inHand, onBoard, inDiscardPile }
        enum Rarity { common, rare, epic, legendary }

        //Fields
        bool playable;
        bool targetable;
        string name;
        List<Ability> abilities;
        string description;
        string flavorText;
        int manaCost;
        int currentManaCost;
        byte[] picture;
        byte[] cardBack;
    }
}