using UnityEngine;
using System.Collections;

public class UnitsFireManager : MonoBehaviour {

	public GameObject bullet;
	public GameObject target;

	public float shootTime = 0;
	public float shootTimer = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!networkView.isMine) {
			return;		
		}
		if (target != null){
			shootTimer += Time.deltaTime;
			if (shootTimer >= shootTime){
				shootTimer -= shootTime;
				GameObject newBullet = Network.Instantiate(bullet, transform.position, transform.rotation, 0) as GameObject;
				newBullet.GetComponent<Bullet>().target = target;
				Debug.Log ("Bullet created " + newBullet + newBullet.GetComponent<Bullet>().target);
			}
		}
	}

	void  OnTriggerEnter(Collider other){
		if (!networkView.isMine) {
			return;		
		}
		if (target == null) {
			if (other.tag == "SelectableUnit" || other.tag == "Building"){
				if (other.gameObject.networkView.isMine){
					return;
				}
				target = other.gameObject;
			}
		}
	}

	void OnTriggerStay(Collider other){
		if (!networkView.isMine) {
			return;		
		}
		if (target == null){
			if (other.tag == "SelectableUnit" || other.tag == "Building"){
				if (other.gameObject.networkView.isMine){
					return;
				}
				target = other.gameObject;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (!networkView.isMine) {
			return;		
		}
		if (other.gameObject == target){
			if (other.gameObject.networkView.isMine){
				return;
			}
			target = null;
		}
	}
}
