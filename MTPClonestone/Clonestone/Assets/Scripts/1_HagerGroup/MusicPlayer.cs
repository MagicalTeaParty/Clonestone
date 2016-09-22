using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip menueClip;
	public AudioClip gameClip;
		
	private AudioSource music;
	
	void Awake () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = menueClip;
			music.loop = true;
			music.Play();
		}
	}
	
	void OnLevelWasLoaded(int level){
		Debug.Log("MusicPlayer: loaded level "+level);
		music.Stop ();
		
		if(level >= 0 && level <= 5){
			music.clip = menueClip;
		}
		if(level == 6){
			music.clip = gameClip;
		}
		
		music.loop = true;
		music.Play();
	}
}
