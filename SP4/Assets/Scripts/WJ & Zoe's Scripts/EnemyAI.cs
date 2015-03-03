using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	/*//T - Tank, S - Soldier
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
	public int moveSpeed;
	public int rotationSpeed;

	public Transform target;
	private Transform myTransform;

	private GameObject unit;
	private Units u;

	//private State _state;
	//private bool _alive = true;

	float MonsterSeekRange = 8; // monster range to chase	
	float MonsterAttackRange = 5; // monster range to attack
		
	//call before anything the script
	void Awake(){
		//moveSpeed = 5;
		//rotationSpeed = 3;	0005oo
		myTransform = transform;
		u = GetComponent<Units> ();
	}

	// Use this for initialization
	void Start () {
		//GameObject go = GameObject.FindGameObjectWithTag ("BHuman");
		//GameObject go2 = GameObject.FindGameObjectWithTag ("BHuman");
		//target = go.transform;

		//target = go2.transform;


		u.state = Units.State.MOVE;	//initial state
		//pai = player.GetComponent<PlayerAI> ();
		//Debug.Log ("Player's Health: " + pai.player_health);
	}	
			
	// Update is called once per frame
	void Update () {	
		//Debug.DrawLine (u.Target.position, myTransform.position, Color.red);

		//Look at Player
		//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(u.Target.position - myTransform.position), u.rot_speed * Time.deltaTime);
	
		//Move toward Player
		//myTransform.position += myTransform.forward * u.move_speed * Time.deltaTime;

		float distance = (target.position - transform.position).magnitude; //dist between enemy and target
		
		if (distance > MonsterAttackRange)		//within attack range

			u.state = Units.State.MOVE;
		//u.move_speed = 0;
		Debug.Log ("pokemon: " + u.state);
	}

	void OnCollisionEnter(Collision Col){
				//moveSpeed = 0;
		}
	*/
}
