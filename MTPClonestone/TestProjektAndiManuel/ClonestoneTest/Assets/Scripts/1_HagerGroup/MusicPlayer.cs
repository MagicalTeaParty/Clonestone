using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip menueClip;
	public AudioClip gameClip;
		
	private AudioSource music;
    float currentMusicTime;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Duplicate music player self-destructing!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = menueClip;
            music.loop = true;
            music.Play();
        }
    }




    //siehe: http://answers.unity3d.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        int level = scene.buildIndex;

        Debug.Log("MusicPlayer: loaded level " + level);
        music.Stop();
        
        if(level == 7) //wenn es das gameboard ist
        {
            music.time = currentMusicTime;
            music.clip = gameClip;           
        }
        else //alle anderen scenen
        {            
            music.time = currentMusicTime;
            music.clip = menueClip;
        }

        music.loop = true;
        music.Play();
    }

    void Update()
    {
        currentMusicTime = music.time;
    }

}
