using UnityEngine;
using System.Collections;

public class OpenURL : MonoBehaviour {

	// Use this for initialization
	public void onClickR ()

    {
        /// TODO - Link auf Registrierung!
        Application.OpenURL("http://mtp.a-k-t.at/Clonestone/Registration/create");
    }

    public void onClickPF()

    {
        /// TODO - Link auf Passwort vergessen!
        Application.OpenURL("http://mtp.a-k-t.at/Clonestone/Login/Login");
    }

    public void onClickTutorial()

    {
        /// TODO - Link auf Tutorial!
        Application.OpenURL("http://mtp.a-k-t.at/Clonestone/Tutorial");
    }

    public void onClickProfil()

    {
        /// TODO - Link auf Profil!
        Application.OpenURL("http://mtp.a-k-t.at/Clonestone/Login/Login");
    }

}
