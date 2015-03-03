using UnityEngine;
using System.Collections;

public class PongGameSetup : MonoBehaviour {

	public Camera mainCam;

	public BoxCollider2D topBorder;
	public BoxCollider2D bottomBorder;
	public BoxCollider2D leftBorder;
	public BoxCollider2D rightBorder;

	public Transform PlayerA;
	public Transform PlayerB;

	// Update is called once per frame
	//void Start () {
	void Update () {
		//Move each wall to its pos.
		topBorder.size = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (Screen.width * 2.0f, 0.0f,0.0f)).x, 1.0f);
		topBorder.center = new Vector2 (0.0f, mainCam.ScreenToWorldPoint (new Vector3 (0.0f, Screen.height, 0.0f)).y + 0.5f);
	
		bottomBorder.size = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (Screen.width * 2.0f, 0.0f,0.0f)).x, 1.0f);
		bottomBorder.center = new Vector2 (0.0f, mainCam.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, 0.0f)).y - 0.5f);

		leftBorder.size = new Vector2 (1.0f, mainCam.ScreenToWorldPoint (new Vector3 (0.0f, Screen.height * 2.0f, 0.0f)).y);
		leftBorder.center = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, 0.0f)).x - 0.5f, 0.0f);

		rightBorder.size = new Vector2 (1.0f, mainCam.ScreenToWorldPoint (new Vector3 (0.0f, Screen.height * 2.0f, 0.0f)).y);
		rightBorder.center = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (Screen.width, 0.0f, 0.0f)).x + 0.5f, 0.0f);

		this.PlayerA.position = new Vector2(mainCam.ScreenToWorldPoint (new Vector3 (75.0f, 0.0f, 0.0f)).x, 0.0f);
		this.PlayerB.position = new Vector2(mainCam.ScreenToWorldPoint (new Vector3 (Screen.width -75.0f, 0.0f, 0.0f)).x, 0.0f);
	}
}
