using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public class Login : MonoBehaviour {
    //Ich bin ein .....

    // Variablen die auf IO Objekte in der Login Scene beziehen.
    public InputField email;
    public InputField password;
    public Toggle stayIn;

    /// <summary>
    /// Methode prüft bei Start ob Toggel "stayin" true oder false ist, im Falle true werden die in der Datei "newfile.txt" strings gesplittet und auf die InputFields "email" und "password" geschrieben. 
    /// </summary>
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

    /// <summary>
    /// Methode dient nur dazu den Wert im Toggle "stayIn" zu speichern.
    /// </summary>
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

    /// <summary>
    /// Methode wird bei Betätigung des Buttons "Login" ausgelöst, prüft ob Toggle "stayin" true ist und schreibt in diesem Fall die Strings der Inputfields "email" und "password" in die Datei "newfile.txt" und erstellt dieses wenn nötig.
    /// </summary>
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

        /// TODO - BESCHREIBEN der StartCoroutine()
        StartCoroutine("SendLoginInformation");
    }

    /// <summary>
    /// Methode um die Eingabe des Users zu übermitteln
    /// </summary>
    /// <returns>string (www.text)</returns>
    public IEnumerator SendLoginInformation()
    {
        string email, pass, hashpass;

        email = this.email.text;
        pass = this.password.text;
        hashpass = getHashSha512(pass);


        //  TEST TEST TEST
        Debug.Log("Email: " + email + "Passw: " + pass);
        Debug.Log("Hash: " + hashpass);

        //WICHTIG! Controller wird mit dem Controllernamen ohne *Controller angesprochen! 
        string saveUrl = "http://localhost:53861/Administration/Save";
        WWWForm form = new WWWForm();
        //WICHTIG! Formfelder müssen ident zu den Übergabewerten der Save() Methode sein!
        form.AddField("email", email);
        form.AddField("password", hashpass);
        WWW www = new WWW(saveUrl, form);


        ///TODO - Erklärung von yield
        yield return www;

        //  TEST TEST TEST 
        Debug.Log("Data: " + www.text);
        Debug.Log("Error: " + www.error);
    }

    /// <summary>
    /// Generiert einen Hash-String via SHA512 Algorytmus
    /// </summary>
    /// <param name="pass"></param>
    /// <returns>hashstring</returns>
    public static string getHashSha512(string pass)
    {
        byte[] bytes = Encoding.Unicode.GetBytes(pass);
        SHA512Managed hashstring = new SHA512Managed();
        byte[] hash = hashstring.ComputeHash(bytes);
        string hashString = string.Empty;
        foreach (byte x in hash)
        {
            hashString += String.Format("{0:x2}", x);
        }
        return hashString;
    }

}
