using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitBuilding : Building {

	public List <Unit> spawnQueue = new List<Unit>();
	public float spawnTimer;
	public short MAX_QUEUE_LENGTH = 5;

	public GameObject[] UnitPrefabs;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		UpdateBuilding ();

		if (!destroyed) {
			if (networkView.isMine) {				
				if (spawnQueue.Count > 0) {
					spawnTimer += Time.deltaTime;
					if (spawnTimer >= spawnQueue[0].spawnTime){
						spawnTimer -= spawnQueue[0].spawnTime;		
						Vector3 temp = this.transform.position;
						temp.x -= 0;
						temp.z -= 4;
						Network.Instantiate (spawnQueue[0], temp, this.transform.rotation, 0);
						//Instantiate(spawnQueue[0], this.transform.position, this.transform.rotation);
						spawnQueue.RemoveAt(0);
					}
				}
				else {
					spawnTimer = 0.0f;
				}
			}
		}
		else{

		}
	}

	public bool AddToQueue(Unit newunit){
		//check if building has space to build unit
		if (spawnQueue.Count >= MAX_QUEUE_LENGTH) {
			return false;
		}
		spawnQueue.Add (newunit);
		return true;
	}

	public bool RemoveFromQueue(int index){
		if (index < 0 || index >= spawnQueue.Count) {
			return false;
		}
		spawnQueue.RemoveAt(index);
		return true;
	}
	
	void OnGUI(){
		//GameObject temp = unitBuildingList.Find(i => i.GetComponent<UnitBuilding>().selected == true);
		if (selected == true){
			if (GUI.Button (new Rect (100, 100, 200, 100), "SPAWN")) {
				AddToQueue(UnitPrefabs[0].GetComponent<Unit>());
			}
			else{
				if (!hover){
					Event e = Event.current;
					if (e.type == EventType.MouseUp){
						if (!new Rect(100, 100, 200, 100).Contains(e.mousePosition)){
							selected = false;
						}
					}
				}
			}
		}
	}
	
	[RPC]		
	public void PlaceBuilding(){
		placing = false;
		gameObject.rigidbody.useGravity = true;
		gameObject.collider.isTrigger = false;	
	}
}
























