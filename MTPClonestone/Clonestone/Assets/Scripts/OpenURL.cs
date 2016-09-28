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

    public void onClickTutorial()

    {
        /// TODO - Link auf Tutorial!
        Application.OpenURL("http://www.golem.de");
    }

    public void onClickProfil()

    {
        /// TODO - Link auf Profil!
        Application.OpenURL("http://www.heise.de");
    }

}
