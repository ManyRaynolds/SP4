using UnityEngine;
using System.Collections;

public class UnitsControl : MonoBehaviour {

	public Transform target;
	float speed = 20;
	Vector3[] path;
	int targetIndex;
	ClickDetector cd;
	public bool commanded = false;

	void Start() {
		cd = FindObjectOfType<ClickDetector>();
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
		if (Input.GetMouseButtonDown(1)){
			if(gameObject.GetComponent<UnitSelect>().isSelected == true)
			{
				if (!target.position.Equals(new Vector3(cd.clickOnWorld.x, 0, cd.clickOnWorld.y))){
					target.position = new Vector3(cd.clickOnWorld.x, 0, cd.clickOnWorld.y);
					PathRequest.RequestPath(transform.position, target.position, OnPathFound);
					targetIndex = 0;
				}
			}
		}
	}
}
