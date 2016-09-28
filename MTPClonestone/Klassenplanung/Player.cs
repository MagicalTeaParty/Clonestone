using System.Collections.Generic;

namespace Klassenplanung
{
    public class Player
    {
        //Fields
        string loginName;
        int maxHandSize;
        int startingHandSize;
        Hero playerHero;
        List<Card> deck;
        List<Card> hand;
        bool isActive;
        List<Card> discardPile;
        List<Minion> minionsOnBoard;

        //Methods
        void GetHand() { }
        void GetManaCrystal() { }
        void RefillMana() { }
        void DrawCard() { }
        void DiscardCard() { }
        void PlayCard() { }
    }
}