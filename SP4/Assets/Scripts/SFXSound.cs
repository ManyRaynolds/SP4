using UnityEngine;
using System.Collections;

public class SFXSound : MonoBehaviour {

	public static AudioSource sfx;
	public AudioClip[] AudioClipSFX;
	public static bool StartBuildSFX = false;
	//public static float volume = 0.5f;
	// Use this for initialization

	void Start () {
		if(StartBuildSFX)
		{
			sfx = this.audio;
			sfx.volume = Settings.BGMSliderValue;

		}
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
		//Debug.Log (sfx.volume);
		//Debug.Log(Settings.BGMSliderValue);
	}
	void OnGUI()
	{
		//DontDestroyOnLoad (sfx);
	}

	public void PlaySound(int clip)
	{
		audio.clip = AudioClipSFX [clip];
		audio.Play ();
	}
}
