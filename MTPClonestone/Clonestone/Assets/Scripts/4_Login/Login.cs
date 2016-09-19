using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public InputField email;
    public InputField password;



    public void onClick()
    {
        StartCoroutine("SendLoginInformation");
    }

    /// <summary>
    /// Methode um die Eingabe des Users zu übermitteln
    /// </summary>
    /// <returns>string (www.text)</returns>
    public IEnumerator SendLoginInformation()
    {
        string email, pass;

        email = this.email.text;
        pass = this.password.text;

        //  TEST TEST TEST
        Debug.Log("Email: " + email + "Passw: " + pass);

        //WICHTIG! Controller wird mit dem Controllernamen ohne *Controller angesprochen! 
        string saveUrl = "http://localhost:53861/Administration/Save";
        WWWForm form = new WWWForm();
        //WICHTIG! Formfelder müssen ident zu den Übergabewerten der Save() Methode sein!
        form.AddField("email", email);
        form.AddField("password", pass);
        WWW www = new WWW(saveUrl, form);

        yield return www;

        //  TEST TEST TEST 
        Debug.Log("Data: " + www.text);
        Debug.Log("Error: " + www.error);
    }


}
