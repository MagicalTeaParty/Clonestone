using System.Collections.Generic;

namespace Klassenplanung
{
    class GameBoard
    {
        //Fields
        string name;
        Hero playerHero;
        Hero enemyHero;
        List<Card> playerDeck;
        List<Card> enemyDeck;
        List<Minion> playerMinionsOnBoard;
        List<Minion> enemyMinionsOnBoard;
        PlayerManaBar playerManaBar;
        OpponentManaBar enemyManaBar;
        EndTurnButton endTurnButton;
        TurnTimer turnTimer;
        HeroPowerButton playerHeroPower;
        HeroPowerButton enemyHeroPower;
        TurnHistory turnHistory;
    }
}