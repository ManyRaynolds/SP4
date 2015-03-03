using UnityEngine;
using System.Collections;

public class SFXSound : MonoBehaviour {

	public static AudioSource sfx;
	public static float volume = 0.5f;
	// Use this for initialization
	bool soundfix = false;

	void Start () {
		AudioClip units = AudioClip.Create ("Units", 44100, 1, 44100, false, true);
		audio.clip = units;
		audio.Play ();
		//audio.volume = MainMenu.
		//sfx = this.audio;
		//if (sfx.audio.isPlaying == false) 
		//{
		//	soundfix = false;
		//}
		//sfx = audio.
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnGUI()
	{
		//DontDestroyOnLoad (sfx);
	}
}
