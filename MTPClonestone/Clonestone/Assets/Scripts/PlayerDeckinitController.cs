using UnityEngine;
using System.Collections;

public class PlayerDeckinitController : MonoBehaviour {

    
    public void onClick()
    {
        StartCoroutine("getDeck");
    }


    public IEnumerator getDeck()
    {

        string url = "http://localhost:53861/Deck/GetDeck";
        WWWForm form = new WWWForm();
        form.AddField("idDeck", 1);

        WWW www = new WWW(url, form);
        Debug.Log("hallo");

        yield return www;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
