using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public Transform muzzle;
	public Rigidbody bullet;
	public float bulletSpeed = 100;
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Bullet(){
		if (Input.GetKey (KeyCode.Space)) {
			Debug.Log("YEAH");
			Rigidbody clone;
			clone = Instantiate(bullet, muzzle.position, muzzle.rotation) as Rigidbody;
			clone.rigidbody.velocity = transform.forward * bulletSpeed;
		}
	}
}
