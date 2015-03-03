using UnityEngine;
using System.Collections;

public class EnemyLogic : MonoBehaviour {

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
	//private int speed = 10;
	
	//Variables
	public Transform target; //player
	public int moveSpeed; //how fast to move
	private int rotationSpeed; // how fast to rotate
	
	
	private Transform myTransform;
	
	
	//Enemy Detection Range;
	public int EnemyAttackRange; // range to attack
	
	//call anything else before the script below are called
	void Awake(){
		//enemy stats
		enemy_health = 100;
		Speed = 10;
		//moveSpeed = 0;
		rotationSpeed = 5;
		EnemyAttackRange = 60;
		
		
		myTransform = transform; // 
		
		playerinfo = GetComponent<PlayerInfo>();
	}
	
	// Use this for initialization
	void Start () {
		//Movement
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		
		//Starting State
		_state = State.MOVE;
		
		playerinfo = player.GetComponent<PlayerInfo> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawLine(target.position, myTransform.position, Color.black);
		
		//Look at Target
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, 
		                                         Quaternion.LookRotation(target.position - myTransform.position), 
		                                         rotationSpeed * Time.deltaTime);
		//Move towards Target
		myTransform.position += myTransform.forward * 	moveSpeed * Time.deltaTime;
		
		//FSM
		UpdateFSM ();
		ExecuteFSM ();
		
		Debug.Log ("CURRENT STATE: " + _state);
	}
	
	void UpdateFSM(){
		//Checking Range
		float distance = (target.position - myTransform.position).sqrMagnitude; // distance between enemy and player
		
		Debug.Log ("DISTANCE: " + distance);
		
		if (distance <= EnemyAttackRange) { 
			
			_state = State.ATTACK;	
			Speed = 0;
			
		}
		else 
		if (distance >= EnemyAttackRange) {
			
			_state = State.MOVE;		
		}
		
		if (playerinfo.player_health <= 0) {
			
			playerinfo.player_health = 0;
			Speed = 10;
			_state = State.MOVE;		
			
			
			//Target = Waypoint[CurrentWaypoint].position;
		}
		
		//Debug.Log ("The Distance is: " + distance);
	}
	
	void ExecuteFSM(){
		
		switch (_state) {
			
		case State.MOVE:{
			//Waypoints///
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
			//////////////
		}
			break;
			
		case State.ATTACK:{
			//Speed = 0;
			rigidbody.velocity	= Vector3.zero;
			playerinfo.player_health -= 1;
			Debug.Log(playerinfo.player_health);
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
