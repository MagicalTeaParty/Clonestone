using System.Collections.Generic;

namespace Klassenplanung
{
    internal class Player
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
    }
}