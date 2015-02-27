﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	//T - Tank, S - Soldier
	private enum State{
		ATTACK,
		SEEK,
		BUILDING,
		BUILDING_DESTROY,
		DEAD,
		T_DEAD,
		S_DEAD,
		PATROL,
	}

	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;



	private Transform myTransform;

	private State _state;
	private bool _alive = true;

	float MonsterSeekRange = 8; // monster range to chase	
	float MonsterAttackRange = 5; // monster range to attack

	//Player's Health
	public GameObject player;
	private PlayerAI pai;
	//private GameObject enemyObject;
		
	//call before anything the script
	void Awake(){
		moveSpeed = 5;
		rotationSpeed = 3;	
		myTransform = transform;
		pai = GetComponent<PlayerAI> ();
	}

	// Use this for initialization
	void Start () {
		if (_alive) {
			GameObject go = GameObject.FindGameObjectWithTag ("Player");
			target = go.transform;
			_state = State.SEEK;	//initial state
			pai = player.GetComponent<PlayerAI> ();
			//Debug.Log ("Player's Health: " + pai.player_health);
		}
	}		
			
	// Update is called once per frame
	void Update () {	
		Debug.DrawLine (target.position, myTransform.position, Color.red);

		//Look at Player
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	
		//Move toward Player
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		////Update FSM
		UpdateFSM ();
		////Excute FSM
		ExecuteFSM ();


		
	}

	void OnCollisionEnter(Collision Col){
		//moveSpeed = 0;
	}

	void UpdateFSM ()
	{
		float distance = (target.position - transform.position).magnitude; //dist between enemy and target

		if (distance < MonsterAttackRange)		//within attack range
			_state = State.ATTACK;
		else if (distance < MonsterSeekRange)	//within seek range
			_state = State.SEEK;

		if (pai.player_health <= 0) {
			pai.player_health = 0;
			_state = State.DEAD;
		}
	}

	void ExecuteFSM(){
		if (_alive) {
			switch(_state){
			case State.ATTACK:{
				Debug.Log(pai.player_health);
				moveSpeed = 0;
				pai.player_health -= 1;
			}
				//attack weijie
				//throw skill/attack, check if skill/attack collide
				break;
			case State.PATROL:{

			}
				break;
			case State.SEEK:{

			}
				//search weijie
				//ways ( static, waypoint )
				break;

			case State.BUILDING:{

			}
				break;
			case State.BUILDING_DESTROY:{

			}
				break;
			case State.T_DEAD:{

			}
				break;
			case State.S_DEAD:{

			}
				break;
			case State.DEAD:{
				//PlayerAI.Destroy(player);
				_alive = false;
			}
				break;
			}
		}	
	}

	//Enemy build barracks
	void Buildings(){

	}
}