using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MoveCardController : MonoBehaviour {


    public GameObject dropZone;
    public GameObject cardHand;
    public GameObject deckposition;
    public GameObject heroPosition;
    public GameObject card;
    public List<GameObject> cards;

    /// <summary>
    /// Bewegt Card Objekte zwischen "Parent-Elementen"
    /// </summary>
    public void MoveCard(GameObject card, GameObject placeToDrop)
    {
        if (placeToDrop == heroPosition)
        {
            card.GetComponent<LayoutElement>().enabled = false;
        }

        card.transform.parent = placeToDrop.transform;
        card.SetActive(true);
    }



    // Use this for initialization
    void Start () {

        MoveCard(card, heroPosition);

	}
	

}
