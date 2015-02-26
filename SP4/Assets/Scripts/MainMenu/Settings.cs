using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

		public static float BGMSliderValue = 0.5f;
		public static float BGMoldSliderValue = 0.5f;

		public static float SFXSliderValue = 0.5f;
		public static float SFXoldSliderValue = 0.5f;

	public GUIStyle BGMMute;
	public GUIStyle BGMMaxVolume;

	public GUIStyle SFXMute;
	public GUIStyle SFXMaxVolume;

	public bool Sbuttonpress = false;

	//public AudioSource bgm;
	
	// Use this for initialization
	void Start () 
	{
			
	}
	
	// Update is called once per frame
	void Update () {
		Sound.bgm.volume = BGMSliderValue;
	}

	void OnGUI()
	{
		//hSliderValue = GUILayout.HorizontalSlider(hSliderValue, 0.0F, 10.0F);
		//GUILayout.Label("Volume: " + hSliderValue);
		//GUILayout.Height (10f);
		if(!Sbuttonpress)
		{
			if(GUI.Button(new Rect(Screen.width/100*2, (float)Screen.height/100*1, Screen.width/100*15, Screen.height/100*10), "Return"))
			{
				Sbuttonpress = true;
				//Initiate.Fade("Main Menu", Color.black,0.5f);
				Initiate.Fade("Main Menu2", Color.black,0.5f);
			}

			//BGMSliderValue = GUI.HorizontalSlider(new Rect(300, 250, 200,100), BGMSliderValue, 0, 1);
			BGMSliderValue = GUI.HorizontalSlider(new Rect(((float)Screen.width/100*40), ((float)Screen.height/100*39), ((float)Screen.width/100*23), 100), BGMSliderValue, 0, 1);
			//SFXSliderValue = GUI.HorizontalSlider(new Rect(300, 350, 200,100), SFXSliderValue, 0, 1);
			SFXSliderValue = GUI.HorizontalSlider(new Rect((float)Screen.width/100*40, (float)Screen.height/100*55, ((float)Screen.width/100*23),100), SFXSliderValue, 0, 1);
			//audio.volume = SliderValue;
			
			GUI.Label(new Rect(370,200,100,50),"Volume: " + BGMSliderValue);

			if(GUI.Button(new Rect(((float)Screen.width/100*33), (float)Screen.height/100*37, (float)Screen.width/100*4, Screen.height/100*7), "", BGMMute))
			{
				if(BGMSliderValue != 0)
				{
					BGMoldSliderValue = BGMSliderValue;
					BGMSliderValue = 0;
				}
				//buttonpressed = true;
				//Initiate.Fade("Credits", Color.green,0.5f);
				Debug.Log("Mute");
				//GUI.Button = null;

			}
			
			if(GUI.Button(new Rect(((float)Screen.width/100*66), (float)Screen.height/100*36, (float)Screen.width/100*7, Screen.height/100*9), "", BGMMaxVolume))
			{
				if(BGMSliderValue == 0)
				{
					BGMSliderValue = BGMoldSliderValue;
				}
				Debug.Log("Volume: " + BGMSliderValue);
			}


			if(GUI.Button(new Rect(((float)Screen.width/100*33), (float)Screen.height/100*53, (float)Screen.width/100*4, Screen.height/100*7), "", SFXMute))
			{
				if(SFXSliderValue != 0)
				{
					SFXoldSliderValue = SFXSliderValue;
					SFXSliderValue = 0;
				}
				//buttonpressed = true;
				//Initiate.Fade("Credits", Color.green,0.5f);
				Debug.Log("Mute");
				//GUI.Button = null;
			}
			
			if(GUI.Button(new Rect(((float)Screen.width/100*66), (float)Screen.height/100*52, (float)Screen.width/100*7, Screen.height/100*9), "", SFXMaxVolume))
			{
				if(SFXSliderValue == 0)
				{
					SFXSliderValue = SFXoldSliderValue;
				}
				Debug.Log("Volume: " + SFXSliderValue);
			}



			Debug.Log("BGM Volume: " + BGMSliderValue + "          SFX Volume: " + SFXSliderValue );
		}
		//}
	}
	public float getVolume()
	{
		return BGMSliderValue;
	}
}


