using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitBuilding : Building {

	public GUIStyle spawnunits;

	public List <GameObject> spawnQueue = new List<GameObject>();
	public float spawnTimer;
	public short MAX_QUEUE_LENGTH = 5;
	
	public GameObject[] UnitPrefabs;

	public Networking network;

	// Use this for initialization
	void Start () {
		network = FindObjectOfType<Networking> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		UpdateBuilding ();
		
		if (!destroyed) {
			if (networkView.isMine) {				
				if (spawnQueue.Count > 0) {
					//Debug.Log("spawned2");
					spawnTimer += Time.deltaTime;
					Unit unit = spawnQueue[0].transform.FindChild("Seeker").GetComponent<Unit>();
					if (spawnTimer >= unit.spawnTime){
						spawnTimer -= unit.spawnTime;		
						Vector3 temp = this.transform.position;
						temp.x -= 0.0f;
						temp.z -= 4.0f;
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
	
	public bool AddToQueue(GameObject newunit){
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
		Vector3 initialmousepos = Vector3.zero;
		//GameObject temp = unitBuildingList.Find(i => i.GetComponent<UnitBuilding>().selected == true);
		if (networkView.isMine) {
			if (selected == true){
				if (GUI.Button (new Rect (100, 200, 100, 200), "Spawn", spawnunits)) {
					//Vector3 temp = this.transform.position;
//					temp.x -= this.transform.lossyScale.x * 3.5f;
//					temp.z -= this.transform.lossyScale.z * 3.5f;
//					Network.Instantiate (spawnQueue[0], temp, this.transform.rotation, 0);
					if (network.gold >= UnitPrefabs[0].GetComponent<Unit>().cost){
						network.gold -= UnitPrefabs[0].GetComponent<Unit>().cost;
						AddToQueue(UnitPrefabs[0]);
					}
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
	}
}
























