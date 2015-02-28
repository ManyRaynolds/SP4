using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	
	public float health = 0;
	public bool destroyed = false;
	
	public GameObject destroyedPartSys;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void SendDamage(float damage){
		networkView.RPC("Damage", RPCMode.All, damage);
	}
	
	[RPC]
	void Damage(float damage){
		health -= damage;
	}
}
