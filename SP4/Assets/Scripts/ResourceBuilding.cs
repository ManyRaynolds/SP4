using UnityEngine;
using System.Collections;

public class ResourceBuilding : Building {

	public float resourceTime = 0;
	public float resourceTimer = 0;

	public int resourceAmount = 0;

	public GameObject networkController;

	// Use this for initialization
	void Start () {
		networkController = GameObject.Find ("Network");
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBuilding ();

		resourceTimer += Time.deltaTime;
		if (resourceTimer >= resourceTime) {
			resourceTimer -= resourceTime;
			networkController.GetComponent<Networking>().gold += resourceAmount;
		}
	}
}
