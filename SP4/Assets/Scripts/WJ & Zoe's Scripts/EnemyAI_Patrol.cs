using UnityEngine;
using System.Collections;

public class EnemyAI_Patrol : MonoBehaviour {

	public Transform[] Waypoint;
	public float Speed;
	public int CurrentWaypoint;
	public bool doPatrol = true;
	public Vector3 Target;
	public Vector3 moveDirection;
	public Vector3 Velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (CurrentWaypoint < Waypoint.Length) {
			Target = Waypoint [CurrentWaypoint].position;
			moveDirection = Target - transform.position;
			Velocity = rigidbody.velocity;

			if (moveDirection.magnitude < 1) {
					CurrentWaypoint++;
	
			} else {
					Velocity = moveDirection.normalized * Speed;
			}
		} else {
			if (doPatrol = true){
				CurrentWaypoint = 0;
			}
			else {
				Velocity = Vector3.zero;
			}
		}
		rigidbody.velocity = Velocity;
		transform.LookAt (Target);
	}
}
