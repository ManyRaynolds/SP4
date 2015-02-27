using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitBuilding : MonoBehaviour {

	public List <Unit> spawnQueue = new List<Unit>();
	public float spawnTimer;
	public short MAX_QUEUE_LENGTH = 5;

	public bool hover = false;
	public bool selected = false;

	public bool placing = true;
	public bool canPlace = true;
	public float placeBufferTime = 1.0f;

	public GameObject[] UnitPrefabs;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if (placing) {
			gameObject.rigidbody.useGravity = false;

			gameObject.collider.isTrigger = true;	
			//make object follow mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// create a plane at 0,0,0 whose normal points to +Y:
			Plane hPlane = new Plane(Vector3.up, Vector3.zero);
			// Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
			float distance = 0; 
			// if the ray hits the plane...
			if (hPlane.Raycast(ray, out distance)){
				// get the hit point:
				Vector3 temp = ray.GetPoint(distance);
				temp.y += 1;
				gameObject.transform.position = temp;
			}
			if (placeBufferTime <= 0){
				if (canPlace && Input.GetMouseButtonUp(0)){
					placing = false;
					gameObject.rigidbody.useGravity = true;
					gameObject.collider.isTrigger = false;	
				}
			}
			else{
				placeBufferTime -= Time.deltaTime;
			}
		}

		if (spawnQueue.Count > 0) {
			spawnTimer += Time.deltaTime;
			if (spawnTimer >= spawnQueue[0].spawnTime){
				spawnTimer -= spawnQueue[0].spawnTime;		
				Vector3 temp = this.transform.position;
				temp.x -= this.transform.lossyScale.x * 1.5f;
				temp.z -= this.transform.lossyScale.z * 1.5f;
				Network.Instantiate (spawnQueue[0], temp, this.transform.rotation, 0);
				//Instantiate(spawnQueue[0], this.transform.position, this.transform.rotation);
				spawnQueue.RemoveAt(0);
			}
		}
		else {
			spawnTimer = 0.0f;
		}

		if (networkView.isMine) {
			if (placing){
				if (canPlace) {
					this.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);		
				} 
				else {
					this.renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);		
				}
			}
			else{
				if (selected) {
					this.renderer.material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);		
				} 
				else if (hover) {
					this.renderer.material.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);		
				}
				else {
					this.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);	
				}
			}
		}
	}

	void OnMouseEnter(){
		if (!placing){
			hover = true;
		}
	}

	void OnMouseExit(){
		if (!placing){
		hover = false;
		}
	}

	void OnMouseDown(){
		if (!placing){
			selected = true;
		}
	}

	void OnTriggerEnter(){
		canPlace = false;
	}

	void OnTriggerExit(){
		canPlace = true;
	}

	void OnTriggerStay(){
		canPlace = false;
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
}
























