using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Save : MonoBehaviour {


        public InputField username;
        public InputField password;
    
    public void OnClick()
    {
        Debug.Log(username.text + ", " + password.text);
        //StreamWriter swr; 
        //TextWriter wr;
        using (System.IO.StreamWriter writer = new StreamWriter("newfile.txt"))
        {
            writer.WriteLine(username.text + " " + password.text);
        }

    }

}
