using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public GameObject[] UnitPrefabs;
	public GameObject[] UnitBuildingPrefabs;

	public List<GameObject> unitBuildingList = new List<GameObject>();

	public bool clickedOnGUI = false;

	// Use this for initialization
	void Start () {
		//GameObject newUnitBuilding = Instantiate (UnitBuildingPrefabs[0]) as GameObject;
		//Debug.Log (newUnitBuilding);
		//unitBuildingList.Add (newUnitBuilding);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GameObject temp = unitBuildingList.Find(i => i.GetComponent<UnitBuilding>().selected == true);
		if (temp != null){
			if (GUI.Button (new Rect (100, 100, 200, 100), "SPAWN")) {
				temp.GetComponent<UnitBuilding>().AddToQueue(UnitPrefabs[0].GetComponent<Unit>());
			}
		}
	}

}
