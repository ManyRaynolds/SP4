using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public GameObject target;
	public float speed = 10;
	public float damage = 10;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			if ((target.transform.position - transform.position).sqrMagnitude <= 1.0f){ 
				//if target is a building
				if (target.GetComponent<Building>() != null){	
					target.GetComponent<Building>().SendDamage(damage);
				}
				//else if target is a unit
				else if (target.GetComponent<Unit>() != null){
					target.GetComponent<Unit>().healthCurrent -= damage;
				}
				//destroy bullet after collision
				Network.Destroy(gameObject);
			}

			Vector3 newDir = target.transform.position - transform.position;
			newDir = newDir / newDir.magnitude;
			
			this.gameObject.rigidbody.velocity = newDir * speed;

		}
		else{
			//Destroy (this);
			//Network.Destroy(gameObject);
			Network.Destroy(gameObject);
		}
	}
	
<<<<<<< HEAD
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject == target) {
=======
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject == target) {
>>>>>>> 5736c08e73526ac47627dd56f2ffba741f03df5b
			//if target is a building
			if (target.GetComponent<Building>() != null){	
				target.GetComponent<Building>().SendDamage(damage);
			}
			//else if target is a unit
			
			//destroy bullet after collision
			Network.Destroy(gameObject);
		}
	}
}
