using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Load : MonoBehaviour {

    public InputField email;
    public InputField password;

    public void Start()
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
