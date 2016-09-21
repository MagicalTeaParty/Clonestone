using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    //Legt die zu ladende Szene fest
    public string Szenename; //wird bei StartCoroutine benötigt
    public float DelayTime = 5f; //Zeit bis die Szene geladen wird

    //wird für das asynchrone Laden der Szene benötigt
    private AsyncOperation async;

    // Use this for initialization
    void Start()
    {
        //Laden nach Zeit
        StartCoroutine(LoadAfterDelay(Szenename, DelayTime));

        //Laden anhand der Ladedauer der Szene
        //Hier kann man den Ladebalken sichtbar setzen
        //loadingImage.SetActive(true);
        //StartCoroutine(LoadLevelWithBar("2_MTPLogo"));
    }
        
    /// <summary>
    /// Lädt ein Level nach einer bestimmten Zeit, ideal für Splashscreen der nicht macht außer ein Logo anzuzeigen.
    /// </summary>
    /// <param name="levelName">Name der Szene</param>
    /// <param name="waitingTime">Zeit die gewartet werden soll</param>
    /// <returns></returns>
    IEnumerator LoadAfterDelay(string levelName, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime); // wait 5 seconds
        SceneManager.LoadScene(levelName);

    }


    /// <summary>
    /// Lädt ein Level mit einem Ladebalken (dieser gehört aber in der Szene hinzugefügt - siehe SelectionMenue Projekt mit ClickToLoadAsync.cs)
    /// </summary>
    /// <param name="levelName">Name der Szene</param>
    /// <returns></returns>
    private IEnumerator LoadLevelWithBar(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName);

        while(!async.isDone)
        {
            //Hier kann man den Fortschritt anhand eines Ladebalkens anzeigen lassen
            //loadingBar.value = async.progress;

            yield return null;
        }
    }

}
