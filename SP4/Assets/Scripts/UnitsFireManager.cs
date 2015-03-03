using UnityEngine;
using System.Collections;

public class UnitsFireManager : MonoBehaviour {

	public GameObject basicProjectile;
	GameObject projectileInstance;
	public bool trigger = false;
	// Use this for initialization
	void Start () {
		Rigidbody projectileRigidInstance;
		projectileRigidInstance = projectileInstance.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	 //if got enemy in radius,
		if(trigger == true)
		{
			projectileInstance = (GameObject)Instantiate(basicProjectile, transform.position, transform.rotation);
		}
	}

	void  OnTriggerEnter(Collider other){
		if (other.tag == "SelectableUnit"){
			GameObject go = GameObject.FindGameObjectWithTag ("SelectableUnit");	
			trigger = true;
		}
	}
	void OnTriggerExit(Collider other) {
		trigger = false;
		DestroyObject(projectileInstance);
	}
}
