using UnityEngine;
using System.Collections;

public class OpenURL : MonoBehaviour {

	// Use this for initialization
	public void onClickR ()

    {
        /// TODO - Link auf Registrierung!
        Application.OpenURL("http://www.google.at");
    }

    public void onClickPF()

    {
        /// TODO - Link auf Passwort vergessen!
        Application.OpenURL("http://www.standard.at");
    }

}
