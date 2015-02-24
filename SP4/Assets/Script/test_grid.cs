using UnityEngine;
using System.Collections;

public class test_grid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var x = 100;
		var z = 100;
	}


	function Start () {
		for (i = 0; i < x; i++) {
			for (j = 0; j < z; j++) {
				Instantiate(prefab, Vector3(i, 0, j), Quaternion.identity);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
