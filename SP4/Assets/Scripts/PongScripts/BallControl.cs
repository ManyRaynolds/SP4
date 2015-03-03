using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	float randomNumber;
	float ballSpeed = 70.0f;
	// Use this for initialization
	void Start () {
		Wait();
		GoBall();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(5);
	}

	void OnCollisionEnter2D(Collision2D colInfo)
	{
		if(colInfo.collider.tag == "Player") {
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3);
		}
	}

	IEnumerator ResetBall() {
		rigidbody2D.velocity = new Vector2(0,0);
		transform.position = new Vector2(0,0);

		yield return new WaitForSeconds(3);
		GoBall();
	}

	void GoBall() {
		randomNumber = Random.Range (-1, 1);
		if (randomNumber < 0) {
			rigidbody2D.AddForce (new Vector2 (ballSpeed, 10));
		}
		else
		{
			rigidbody2D.AddForce (new Vector2 (-ballSpeed, -10));
		}
	}

}
