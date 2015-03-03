using UnityEngine;
using System.Collections;

public class UnitsFireManager : MonoBehaviour {

	public GameObject basicProjectile;
	GameObject projectileInstance;
	// Use this for initialization
	void Start () {
		Rigidbody projectileRigidInstance;
		projectileRigidInstance = projectileInstance.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	 //if got enemy in radius,
		projectileInstance = (GameObject)Instantiate(basicProjectile, transform.position, transform.rotation);
	}
}
