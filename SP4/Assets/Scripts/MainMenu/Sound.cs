using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public static AudioSource bgm;
	//public static float volume = 0.5f;
	// Use this for initialization
	void Start () {
		bgm = this.audio;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnGUI()
	{
		DontDestroyOnLoad (bgm);
	}
}
