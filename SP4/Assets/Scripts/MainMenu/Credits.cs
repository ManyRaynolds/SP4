using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public bool buttonpress = false;
	// Use this for initialization
	void OnGUI () {
		if(!buttonpress)
		{
		//if(GUI.Button(new Rect(((float)Screen.width/2-100), (float)Screen.height/2+65, 210, 60), "", "Return"))
		if(GUI.Button(new Rect(10, 1, 120, 60), "Return"))
		{
			Initiate.Fade("Main Menu2", Color.black,0.5f);
				buttonpress = true;
			//Destroy(this.audio);
		}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
