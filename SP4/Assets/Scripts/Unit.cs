using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public float spawnTime = 5.0f;
	public float healthMax = 100;
	public float healthCurrent = 0;
	public int cost = 0;
	
	// Use this for initialization
	void Start () {
		healthCurrent = healthMax;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthCurrent <= 0){
			Network.Destroy(transform.parent.gameObject);
		}
	}
}
