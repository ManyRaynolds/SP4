using UnityEngine;
using System.Collections;

public class PongSideWalls : MonoBehaviour {

	public string borderName;

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		if (hitInfo.name == "Ball")
		{
			borderName = transform.name;
			PongGameManager.Score(borderName);
			hitInfo.gameObject.SendMessage("ResetBall");
		}
	}
}
