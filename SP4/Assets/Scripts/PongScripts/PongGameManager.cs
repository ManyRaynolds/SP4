using UnityEngine;
using System.Collections;

public class PongGameManager : MonoBehaviour {

	static int playerScoreA = 0;
	static int playerScoreB = 0;

	// Update is called once per frame
	public static void Score (string borderName) {
		if (borderName == "rightBorder")
		{
			playerScoreA += 1;
		}
		else if (borderName == "leftBorder")
		{
			playerScoreB += 1;
		}
		Debug.Log ("Player A's score is " + playerScoreA);
		Debug.Log ("Player B's score is " + playerScoreB);
	}

	public void OnGUI()
	{
		GUI.Label (new Rect(Screen.width/2-150-12, 20, 100, 100), "" + playerScoreA);
		GUI.Label (new Rect(Screen.width/2+150-12, 20, 100, 100), "" + playerScoreB);
	}

}
