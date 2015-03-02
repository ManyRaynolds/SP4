using UnityEngine;
using System.Collections;

public class EnemyAI_Final : MonoBehaviour {

	//enemy var
	public int enemy_health;

	//WaysPoints Var//

	public Transform[] Waypoint;
	public float Speed;
	public int CurrentWaypoint;
	public bool doPatrol = true;
	public Vector3 Target;
	public Vector3 moveDirection;
	public Vector3 Velocity;

	//////////////////

	//FSM State
	public enum State{
		MOVE,
		ATTACK,
		SEEK,
		FOLLOW,
		DEAD,
		DEAD_UNIT,
	}

	//Accessing other script
	public GameObject player;
	private PlayerInfo playerinfo;

	private State _state; // local var that represent our State

	//const Var
	private int speed = 10;

	//Variables
	public Transform target; //player
	public int moveSpeed; //how fast to move
	public int rotationSpeed; // how fast to rotate


	private Transform myTransform;


	//Enemy Detection Range;
	private int EnemyAttackRange; // range to attack

	//call anything else before the script below are called
	void Awake(){
		//enemy stats
		enemy_health = 100;

		moveSpeed = speed;
		rotationSpeed = 5;
		EnemyAttackRange = 10;
	

		myTransform = transform; // 

		playerinfo = GetComponent<PlayerInfo>();
	}

	// Use this for initialization
	void Start () {
		//Movement
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;

		//Starting State
		_state = State.FOLLOW;

		playerinfo = player.GetComponent<PlayerInfo> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position, myTransform.position, Color.green);

		//Look at Target
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, 
		                                         Quaternion.LookRotation(target.position - myTransform.position), 
		                                         rotationSpeed * Time.deltaTime);
		//Move towards Target
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		//FSM
		UpdateFSM ();
		ExecuteFSM ();

		Debug.Log ("Current State: " + _state);
		Debug.Log ("Player's Health: " + playerinfo.player_health);
		Debug.Log ("CurrentWaypoint: " + moveDirection.sqrMagnitude);
		Debug.Log ("CurrentWaypoint2: " + CurrentWaypoint);


	}

	void UpdateFSM(){
		//Checking Range
		float distance = (target.position - myTransform.position).sqrMagnitude; // distance between enemy and player

		if (distance <= EnemyAttackRange) { 

			_state = State.ATTACK;		
		
		}
		else 
		if (distance >= EnemyAttackRange) {

			_state = State.MOVE;		
		}

		if (playerinfo.player_health <= 0) {

			playerinfo.player_health = 0;
			moveSpeed = speed;
			_state = State.MOVE;		
			
		
			//Target = Waypoint[CurrentWaypoint].position;
		}
		
		//Debug.Log ("The Distance is: " + distance);
	}

	void ExecuteFSM(){

		switch (_state) {
				
		case State.MOVE:{
			//Waypoints///
			if (CurrentWaypoint <  Waypoint.Length) {
				Target = Waypoint [CurrentWaypoint].position;
				moveDirection = Target - myTransform.position;
				Velocity = rigidbody.velocity;
				
				if (moveDirection.sqrMagnitude < 1) {
					CurrentWaypoint++;
					
				} else {
					Velocity = moveDirection.normalized * moveSpeed;
				}
			} else {
				CurrentWaypoint = 0;
			}
			transform.LookAt (Target);
			//////////////
		}
			break;
			
		case State.ATTACK:{
			moveSpeed = 0;
			playerinfo.player_health -= 10;
		}
			break;
			
		case State.SEEK:{
		}
			break;
			
		case State.DEAD:{
			Debug.Log ("I'm Dead!");

		}
			break;
		}
	}
}



















