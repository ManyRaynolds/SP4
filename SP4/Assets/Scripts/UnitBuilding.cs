using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UnitBuilding : MonoBehaviour {

	public List <Unit> spawnQueue = new List<Unit>();
	public float spawnTimer;
	public short MAX_QUEUE_LENGTH = 5;

	public bool hover = false;
	public bool selected = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (spawnQueue.Count > 0) {
			spawnTimer += Time.deltaTime;
			if (spawnTimer >= spawnQueue[0].spawnTime){
				spawnTimer -= spawnQueue[0].spawnTime;				
				Network.Instantiate (spawnQueue[0], this.transform.position, this.transform.rotation, 0);
				//Instantiate(spawnQueue[0], this.transform.position, this.transform.rotation);
				spawnQueue.RemoveAt(0);
			}
		}
		else {
			spawnTimer = 0.0f;
		}

		if (networkView.isMine) {
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
		else{
			if (selected) {
				this.renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);		
			} 
			else if (hover) {
				this.renderer.material.color = new Color(1.0f, 0.5f, 0.5f, 1.0f);		
			}
			else {
				this.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);	
			}
		}
	}

	void OnMouseEnter(){
		hover = true;
	}

	void OnMouseExit(){
		hover = false;
	}

	void OnMouseDown(){
		selected = true;
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
}
