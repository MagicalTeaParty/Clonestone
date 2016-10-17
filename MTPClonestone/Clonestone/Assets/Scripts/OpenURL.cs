using UnityEngine;
using System.Collections;

public class OpenURL : MonoBehaviour {

	// Use this for initialization
	public void onClickR ()

    {
        /// TODO - Link auf Registrierung!
        Application.OpenURL("http://localhost:53861/Registration");
    }

    public void onClickPF()

    {
        /// TODO - Link auf Passwort vergessen!
        Application.OpenURL("http://localhost:53861/PasswordForgotten");
    }

    public void onClickTutorial()

    {
        /// TODO - Link auf Tutorial!
        Application.OpenURL("http://localhost:53861/Tutorial");
    }

    public void onClickProfil()

    {
        /// TODO - Link auf Profil!
        Application.OpenURL("http://localhost:53861/Profile");
    }

}
