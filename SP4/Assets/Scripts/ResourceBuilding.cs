using UnityEngine;
using System.Collections;

public class ResourceBuilding : Building {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !destroyed) {
			destroyed = true;
			Instantiate(destroyedPartSys, this.transform.position, destroyedPartSys.transform.rotation);
		}
	}
}
