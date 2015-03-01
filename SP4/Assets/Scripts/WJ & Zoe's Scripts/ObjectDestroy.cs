using UnityEngine;
using System.Collections;

public class ObjectDestroy : MonoBehaviour {

	void OnTiggerEnter(Collider trigger){
		if (trigger.tag == "Player") {
			Destroy(gameObject);		
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
