using UnityEngine;
using System.Collections;

public class Units : MonoBehaviour 
{
	public float spawnTime;
	
	public float health;
	public float max_health;
	
	public float damage;
	public float range;
	public float attack_time;
	public float attack_timer;
	
	public float move_speed;
	public float rot_speed;
	
	private float targetPos;

	//Pathfinder pather = new Pathfinder();

	//Other stuff
	Vector3 direction;
	
	public float rtime;
	
	public GameObject Bullet;
	public GameObject hptext;
	
	public Camera camera1;
	
	public int var = 1;

	public GameObject j_unit;
	public GameObject b_unit;

	//public BritishUnit ba_unit;
	//public JapanUnit ja_unit;

	public enum State
	{
		IDLE,
		MOVE,
		ATTACK,
		DEAD
	};
	public State state;

	//other stuff
	void Awake()
	{
		//ba_unit = b_unit.GetComponent<BritishUnit> ();
		//ja_unit = j_unit.GetComponent<JapanUnit> ();
	}

	// Use this for initialization
	void Start () 
	{
		//other stuff
		direction.x = 1;
		rtime = Time.time;
		
		hptext.guiText.text = "Health: " + health;
		Vector3 temp = camera1.WorldToViewportPoint (transform.position + new Vector3(0f,1f,0f));
		hptext.transform.position = new Vector3(temp.x, temp.y);
		
		state = State.MOVE;

		health = max_health;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log ("Current State: " + state);

		UpdateFSM ();
		ExecuteFSM ();

		//other stuff
		hptext.guiText.text = "Health: " + health;
		Vector3 temp = camera1.WorldToViewportPoint (transform.position + new Vector3(0f,1.1f,0f));
		hptext.transform.position = new Vector3(temp.x, temp.y);
	
		GameObject[] allObjects = UnityEngine.GameObject.FindObjectsOfType<GameObject>() ;
		if(tag == "BTank" || tag == "BHuman")
		{
			foreach(GameObject go in allObjects)	
			{
				if(go.tag == "JTank" || go.tag == "JHuman")
				{
					if (j_unit == null || Vector3.Distance(j_unit.transform.position, this.transform.position) > Vector3.Distance(go.transform.position, this.transform.position) )
					{
						j_unit = go;
					}
				}
			}
		}

		if(tag == "JTank" || tag == "JHuman")
		{
			foreach(GameObject go in allObjects)	
			{
				if(go.tag == "BTank" || go.tag == "BHuman")
				{
					if (b_unit == null || Vector3.Distance(b_unit.transform.position, this.transform.position) > Vector3.Distance(go.transform.position, this.transform.position) )
					{
						b_unit = go;
					}
				}
			}
		}
	}
	
	void UpdateFSM()
	{
		switch (state) 
		{
			case State.IDLE:
			{
				if (health <= 0)
				{
					state = State.DEAD;
				}
			}
			break;
		
			case State.MOVE:
			{
				if (health <= 0)
				{
					state = State.DEAD;
				}

				//other stuff
				if (gameObject.tag == "BTank") 
				{
					if ((transform.position - j_unit.transform.position).sqrMagnitude < 40) 
					{
						state = State.ATTACK;
					}
				}

				else if (gameObject.tag == "BHuman") 
				{
					if ((transform.position - j_unit.transform.position).sqrMagnitude < 5) 
					{
						state = State.ATTACK;
					}
				}

				else if (gameObject.tag == "JTank" ) 
				{
					if ((transform.position - b_unit.transform.position).sqrMagnitude < 40) 
					{
						state = State.ATTACK;

					}
				}
				
				else if (gameObject.tag == "JHuman") 
				{
					if ((transform.position - b_unit.transform.position).sqrMagnitude < 5) 
					{
						state = State.ATTACK;
					}
				}
			}
			break;

			case State.ATTACK:
			{
				if (health <= 0)
				{
					state = State.DEAD;
				}

				//other stuff
				if(j_unit == null)
				{
					state= State.MOVE;
					break;
				}

				if(b_unit == null)
				{
					state= State.MOVE;
					break;
				}
			}
			break;

			case State.DEAD:
			{
			}
			break;

			default:
			{
			}
			break;
		}
	}
	
	void ExecuteFSM()
	{
		switch (state) 
		{
			case State.IDLE:
			{
			}
			break;

			case State.MOVE:
			{
				//pather.seeker = this.transform;
				//pather.target = targetPos;

				//other stuff
				if (gameObject.tag == "BTank")
				{
					Vector3 pos = transform.position;
					pos.x += move_speed;
					transform.position = pos;
				}
				
				else if (gameObject.tag == "BHuman")
				{
					Vector3 pos = transform.position;
					pos.x += move_speed;
					transform.position = pos;
				}

				else if (gameObject.tag == "JTank")
				{
					Vector3 pos = transform.position;
					pos.x -= move_speed;
					transform.position = pos;
				}
				
				
				else if (gameObject.tag == "JHuman")
				{
					Vector3 pos = j_unit.transform.position;
					pos.x -= move_speed;
					j_unit.transform.position = pos;
				}
			}
			break;

			case State.ATTACK:
			{
				if (gameObject.tag == "BTank" && Bullet != null)
				{
					/*Vector3 bulletpos = Bullet.transform.position;
					bulletpos.x += 0.05f;
					Bullet.transform.position = bulletpos;*/

					attack_timer += Time.deltaTime;
					
					if (attack_timer >= attack_time)
					{
						attack_timer -= attack_time;
						GameObject go = Instantiate (Bullet, this.gameObject.transform.position + 2 * new Vector3(1,0,0), this.gameObject.transform.rotation) as GameObject;
						go.rigidbody.velocity = new Vector3(1, 0, 0);
						go.GetComponent<Bullet>().damage = damage;
					}
				}
						
				else if (gameObject.tag == "BHuman" && j_unit != null )
				{
					if (Time.time - rtime > 1.0f) 
					{
						j_unit.GetComponent<Units>().health -= damage; 
						rtime = Time.time;
					}
					
					if (j_unit != null && health <= 0)
					{
						health = 0;
						state = State.DEAD;
					}
				}
				
				if (gameObject.tag == "JTank"  && Bullet != null)
				{
					attack_timer += Time.deltaTime;
					
					if (attack_timer >= attack_time)
					{
						attack_timer -= attack_time;
						GameObject go = Instantiate (Bullet, this.gameObject.transform.position + 2 * new Vector3(-1,0,0), this.gameObject.transform.rotation) as GameObject;
					go.rigidbody.velocity = new Vector3(-1, 0, 0);
					go.GetComponent<Bullet>().damage = damage;
				}
				}
			
				else if (gameObject.tag == "JHuman" && b_unit != null )
				{
					if (Time.time - rtime > 1.0f) 
					{
						b_unit.GetComponent<Units>().health -= damage; 
						rtime = Time.time;
					}
					
					if (b_unit != null && health <= 0)
					{
						health = 0;
					}
				}
			}
			break;

			case State.DEAD:
			{
				if (gameObject.tag == "BTank")
				{
					Destroy(b_unit);
				}

				if (gameObject.tag == "BHuman")
				{
					Destroy(b_unit);
				}
				
				if (gameObject.tag == "JTank")
				{
					Destroy(j_unit);
				}
				
				if (gameObject.tag == "JHuman")
				{
					Destroy(j_unit);
				}

				//Network.Destroy(this.gameObject);
			}
			break;

			default:
			{
			}
			break;
		}
	}
}




























