using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Save : MonoBehaviour {


        public InputField email;
        public InputField password;
    
    public void OnClick()
    {
        Debug.Log(email.text + ", " + password.text);
        //StreamWriter swr; 
        //TextWriter wr;
        using (System.IO.StreamWriter writer = new StreamWriter("newfile.txt"))
        {
            writer.WriteLine(email.text + " " + password.text);
        }

    }

}
