using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitsControl : MonoBehaviour {

	public Transform target;
	float speed = 20;
	Vector3[] path;
	int targetIndex;
	ClickDetector cd;
	public bool commanded = false;
	public bool found = false;
	public LayerMask EnemyMask;
	bool NotAttackable;
	public List<GameObject> UnSelectableUnits;
	public List<GameObject> list;
	public List<GameObject> GO;
	public Building myBuilding;
	public UnitsManager um;

	void Start() {
		cd = FindObjectOfType<ClickDetector>();
		um = FindObjectOfType<UnitsManager>();
		myBuilding = FindObjectOfType<Building>();
		if(gameObject.GetComponent<UnitSelect>().isSelected == true)
		{
			//Debug.Log("Hello", gameObject);
			PathRequest.RequestPath(transform.position, target.position, OnPathFound);
		}
	}

	public void OnPathFound(Vector3[] newpath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newpath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];

		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i++) {
				Gizmos.color = Color.cyan;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1], path[i]);
				}
			}

		}
	}

	void Update() {
		//This is where i attack
		if (Input.GetMouseButtonDown(1)){
			if(gameObject.GetComponent<UnitSelect>().isSelected == true)
			{
				if (!target.position.Equals(new Vector3(cd.clickOnWorld.x, 0, cd.clickOnWorld.y))){
					target.position = new Vector3(cd.clickOnWorld.x, 0, cd.clickOnWorld.y);
					PathRequest.RequestPath(transform.position, target.position, OnPathFound);
					targetIndex = 0;
				}
			}

			UnSelectableUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("UnSelectableUnit"));
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, EnemyMask))
			{
				//UnSelectableUnits.Remove(hit.transform.gameObject);
				//hit.transform.gameObject.SendMessage("OnSelected",SendMessageOptions.DontRequireReceiver);
				//can add type checking here
				//if (type == Building)
				HitBuilding();
				//if (type == Unit)
				//HitUnit();
			}

		}
		if (um && found == false && gameObject.GetComponent<PathRequest>().isProcessingPath == false)
		{
			//&& gameObject.GetComponent<PathRequest>().isProcessingPath == false
			PathRequest.RequestPath(transform.position, target.position, OnPathFound);
			//found = true;
			targetIndex = 0;
		}
		else{
			found = true;
		}
	}
	
	public void HitBuilding() {
		myBuilding.SendDamage(20);
	}

	public void HitUnit() {

	}

}
