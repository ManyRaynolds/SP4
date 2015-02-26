using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		col.gameObject.GetComponent<Units> ().health -= damage;
		Destroy (this.gameObject);
	}

}
