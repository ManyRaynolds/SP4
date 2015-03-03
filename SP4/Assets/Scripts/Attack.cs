using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	/*//Other stuff
	Vector3 direction;

	public float health;
	public float max_health;

	public float rtime;

	public float attack_time;
	public float attack_timer;

	public GameObject j_unit;
	public GameObject b_unit;
	
	public float damage;
	public float range;

	public LayerMask EnemyMask;

	public Bullet bullet;

	// Use this for initialization
	void Start () {
		//other stuff
		direction.x = 1;
		rtime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Attacking ();
	}

	void AttackCase () {
		if (health <= 0){
		}
		//other stuff
		if(j_unit == null){
		}if(b_unit == null){
		}
	}

	void Attacking () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, EnemyMask)) 
		{
			if (gameObject.tag == "BTank") 
			{
					/*Vector3 bulletpos = Bullet.transform.position;
					bulletpos.x += 0.05f;
					Bullet.transform.position = bulletpos;*/
				/*attack_timer += Time.deltaTime;
			
				if (attack_timer >= attack_time) 
				{
					attack_timer -= attack_time;
					GameObject go = Instantiate (bullet, this.gameObject.transform.position + 2 * new Vector3 (1, 0, 0), this.gameObject.transform.rotation) as GameObject;
					go.rigidbody.velocity = new Vector3 (1, 0, 0);
					go.GetComponent<Bullet> ().damage = damage;
				}
			} 
			else if (gameObject.tag == "BHuman") 
			{
				if (Time.time - rtime > 1.0f) 
				{
					j_unit.GetComponent<Units> ().health -= damage; 
					rtime = Time.time;
				}
			
				if (j_unit != null && health <= 0) 
				{
					health = 0;
				}
			}
			if (gameObject.tag == "JTank") 
			{
				attack_timer += Time.deltaTime;
			
				if (attack_timer >= attack_time) 
				{
						attack_timer -= attack_time;
						GameObject go = Instantiate (bullet, this.gameObject.transform.position + 2 * new Vector3 (-1, 0, 0), this.gameObject.transform.rotation) as GameObject;
						go.rigidbody.velocity = new Vector3 (-1, 0, 0);
						go.GetComponent<Bullet> ().damage = damage;
				}
			}
			else if (gameObject.tag == "JHuman" && b_unit != null) 
			{
				if (Time.time - rtime > 1.0f) {
						b_unit.GetComponent<Units> ().health -= damage; 
						rtime = Time.time;
				}
			
				if (b_unit != null && health <= 0) {
					health = 0;
				}
			}
		}
	}*/
}
