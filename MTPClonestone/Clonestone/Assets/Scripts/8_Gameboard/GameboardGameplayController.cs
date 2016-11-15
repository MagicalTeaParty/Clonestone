using UnityEngine;

public class GameboardGameplayController : MonoBehaviour
{
    //Methods

    /// <summary>
    /// Diese Methode zieht eine Karte vom Deck des mitgegebenen Spielers.
    /// Soll mehr als eine Karte gezogen werden, muss die Methode dementsprechend oft aufgerufen werden
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static GameObject DrawCard(GameObject player)
    {
        GameObject cardDrawn = null;

        ///Die Schleife sucht in der Kartenliste des mitgegebenen Spielers die erste Karte, deren "CardStatus" gleich "inDeck" ist, und gibt diese zurück
        foreach (GameObject card in player.GetComponent<PlayerDataController>().CardList)
        {
            if (card.GetComponent<CardDataController>().Data.CardStatus == CardDataController.CardStatus.inDeck )
            {
                cardDrawn = card;
                return cardDrawn;
            }
        }

        return cardDrawn;
    }

    /// <summary>
    /// Diese Methode wechselt den Zustand des Bools "IsActivePlayer" des mitgegebenen Spielers
    /// --> Muss daher zweimal aufgerufen werden; einmal für jeden Spieler
    /// </summary>
    /// <param name="player"></param>
    public static void PlayerChange(GameObject player)
    { 
        player.GetComponent<PlayerDataController>().PlayerChange();
    }
}