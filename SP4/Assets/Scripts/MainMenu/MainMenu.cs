using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUIStyle StartGame;
	public GUIStyle Settings;
	public GUIStyle Credits;
	public GUIStyle ExitGame;

	bool buttonpressed = false;

	public static string url = "127.0.0.1";

//	public static AudioSource bgm;
//	public static float volume = 0.5f;


	//AudioClip audioclips = AudioClip.Create("Burn", 44100, 1, 44100, false, true);
	// Use this for initialization
	void Start () 
	{
//		bgm = this.audio;
	}
	
	// Update is called once per frame
	void Update () {}
	
	void OnGUI(){
		//buttonpressed = true;
		if(!buttonpressed)
		{
			if(GUI.Button(new Rect(((float)Screen.width/100*38), (float)Screen.height/100*36, (float)Screen.width/100*25, (float)Screen.height/100*10), "", StartGame))
			{
				buttonpressed = true;
				Initiate.Fade("LoginMenu", Color.black,0.5f);
				Debug.Log("Start Game");
				//GUI.Button = null;
			}

			else if(GUI.Button(new Rect(((float)Screen.width/100*38), (float)Screen.height/100*47, (float)Screen.width/100*25, (float)Screen.height/100*10), "", Settings))
			{
				buttonpressed = true;
				Initiate.Fade("Settings", Color.black,0.5f);
				Debug.Log("Settings");
			}

			else if(GUI.Button(new Rect(((float)Screen.width/100*38), (float)Screen.height/100*58, (float)Screen.width/100*25, (float)Screen.height/100*10), "", Credits))
			{
				buttonpressed = true;
				Initiate.Fade("Credits", Color.black,0.5f);
				Debug.Log("Credits");

			}

			else if(GUI.Button(new Rect(((float)Screen.width/100*38), (float)Screen.height/100*69, (float)Screen.width/100*25, (float)Screen.height/100*10), "", ExitGame))
			{
				Application.Quit();
				Debug.Log("Exit Game");
			}

	//		if (GUI.Button(new Rect(((float)Screen.width*0.75), ((float)Screen.height*0.75), 100, 50), "Start Game"))
	//			Debug.Log("Clicked the button with an image");
		}
//		DontDestroyOnLoad (bgm);
		//DontDestroyOnLoad (audio);

	}
}
