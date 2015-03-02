using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {

	public Vector3 velocity;

	void OnCollisionEnter(Collision collision){
		Debug.Log (gameObject.name + "has collided with" + collision.gameObject.name);
	}

	void OnTriggerEnter3D(Collider other){
		Debug.Log (gameObject.name + "has trigger by" + other.gameObject.name);
	}

	//// Update is called once per frame
	void Update () {

	}
}
