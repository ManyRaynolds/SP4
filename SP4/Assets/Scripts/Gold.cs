using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour 
{
	public int gold;
	private float rtime;
	bool collectgold;

	// Use this for initialization
	void Start () 
	{
		gold = 0;
		guiText.text = "Gold : " + gold;
		rtime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time - rtime > 1.0f) 
		{
			gold += 1;
			rtime = Time.time;
			guiText.text = "Gold : " + gold;
		}
	}
}