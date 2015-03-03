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
		if (target != null){
			shootTimer += Time.deltaTime;
			if (shootTimer >= shootTime){
				shootTimer -= shootTime;
				GameObject newBullet = Network.Instantiate(bullet, transform.position, transform.rotation, 0) as GameObject;
				newBullet.GetComponent<Bullet>().target = target;
			}
		}
	}

	void  OnTriggerEnter(Collider other){
		if (other.gameObject.networkView.isMine){
			return;
		}
		if (other.tag == "SelectableUnit"){
			if (target == null){
				target = other.gameObject;
			}
		}
	}

	void OnTriggerStay(Collider other){
		//if (other.gameObject.networkView.isMine){
		//	return;
		//}
		if (other.tag == "SelectableUnit"){
			if (target == null){
				target = other.gameObject;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.networkView.isMine){
			return;
		}
		if (other.gameObject == target){
			target = null;
		}
	}
}
