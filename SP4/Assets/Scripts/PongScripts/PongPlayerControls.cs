using UnityEngine;
using System.Collections;

public class PongPlayerControls : MonoBehaviour {

	public KeyCode moveUp;
	public KeyCode moveDown;

	public float speed = 10.0f;

	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(moveUp))
		{
			rigidbody2D.velocity = new Vector2(0, speed);
		}
		else if(Input.GetKey(moveDown))
		{
			rigidbody2D.velocity = new Vector2(0,speed*-1.0f);
		}
		else
		{
			//stop the mvt.
			rigidbody2D.velocity = new Vector2(0,0);
		}
	}
}
