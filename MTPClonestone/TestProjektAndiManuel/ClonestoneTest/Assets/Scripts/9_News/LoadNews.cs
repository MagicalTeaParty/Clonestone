using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadNews : MonoBehaviour {

    public Vector2 scrollPosition = Vector2.zero;
    public string innerText = "hi";

    public GameObject news; //für das Prefab

    void OnGUI()
    {
        //GUI.BeginScrollView(new Rect(0, 0, 200, 200), scrollPosition, new Rect(0, 0, 190, 400));

        //innerText = GUI.TextArea(new Rect(0, 0, 200, 300), innerText);

        //GUI.EndScrollView();
    }

    // Use this for initialization
    /// <summary>
    /// Ladet alle News und fügt diese der Listbox hinzu
    /// </summary>
    void Start () {

        //holt das Gameobject von NewsGrid
        GameObject newsGrid = GameObject.Find("NewsGrid");
        //Instantiate(brick, new Vector3(x, y, 0), Quaternion.identity);
        GameObject actNews = Instantiate(news);

        
        actNews.GetComponentInChildren<Text>().text = "START";
        //actNews.GetComponent<Text>().text = "START";

        actNews.transform.SetParent(newsGrid.transform);

        //actNews.transform.parent = newsGrid.transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
