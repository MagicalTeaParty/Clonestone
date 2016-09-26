using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Login : MonoBehaviour {

    public InputField email;
    public InputField password;
    public Toggle stayIn;

    public void Start()
    {
        if (PlayerPrefs.GetInt("stayIn") == 1)
        {
            stayIn.isOn = true;
        }
        else
        {
            stayIn.isOn = false;
        }


        if (stayIn.isOn)
        {
            using (TextReader reader = File.OpenText("newfile.txt"))
            {
                string meinString = reader.ReadLine();
                string[] meineStrings = meinString.Split(new char[] { ' ' });
                email.text = meineStrings[0];
                password.text = meineStrings[1];
            }
        }       
    }

    public void Update()
    {
        if (stayIn.isOn == true)
        {
            PlayerPrefs.SetInt("stayIn", 1);
        }
        else
        {
            PlayerPrefs.SetInt("stayIn", 0);
        }
    }

    public void onClick()
    {
        if (stayIn.isOn)
        {
            using (System.IO.StreamWriter writer = new StreamWriter("newfile.txt"))
            {
                writer.WriteLine(email.text + " " + password.text);
            }
            // TEST TEST TEST
            Debug.Log("StayIn : " + stayIn.isOn);
        }


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
