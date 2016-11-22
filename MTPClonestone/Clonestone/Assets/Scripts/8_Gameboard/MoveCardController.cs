using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MoveCardController : MonoBehaviour {


    public GameObject dropZone;
    public GameObject cardHand;
    public GameObject deckposition;
    public GameObject heroPosition;
    
    public List<GameObject> cards;

    /// <summary>
    /// Bewegt Card Objekte zwischen "Parent-Elementen"
    /// </summary>
    /// <param name="card">Kartenobjekt</param>
    /// <param name="placeToDrop">Zone in der die Karte abgelegt wird</param>
    public void MoveCard(GameObject card, GameObject placeToDrop)
    {
        //heroPosition = Dropbereich der Hero-Karte
        if (placeToDrop == heroPosition)
        {
            card.GetComponent<LayoutElement>().enabled = false;
        }

        card.transform.parent = placeToDrop.transform;
        card.SetActive(true);
        
    }



    // Use this for initialization
    void Start () {
        GameObject card=null;
        MoveCard(card, heroPosition);

	}
	

}
