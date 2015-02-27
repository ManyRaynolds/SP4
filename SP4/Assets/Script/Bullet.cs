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
		print (col.gameObject);
		if (col.gameObject.tag == "bullet") {
			return;		
		}
		col.gameObject.GetComponent<Units> ().health -= damage;
		Destroy (this.gameObject);
	}
	
}
