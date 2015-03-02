using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	
	public float health = 0;
	public bool destroyed = false;
	
	public ParticleSystem destroyedPartSys;
	public ParticleSystem damagePartSys;
	
	public bool hover = false;
	public bool selected = false;
	
	public bool placing = true;
	public bool canPlace = true;
	public float placeBufferTime = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void UpdateBuilding(){
		
		if (health <= 0 && !destroyed) {
			destroyed = true;
			Instantiate(destroyedPartSys, this.transform.position, destroyedPartSys.transform.rotation);
		}
		if (placing) {
			gameObject.rigidbody.useGravity = false;
			
			gameObject.collider.isTrigger = true;	
			//make object follow mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// create a plane at 0,0,0 whose normal points to +Y:
			Plane hPlane = new Plane(Vector3.up, Vector3.zero);
			// Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
			float distance = 0; 
			// if the ray hits the plane...
			if (hPlane.Raycast(ray, out distance)){
				// get the hit point:
				Vector3 temp = ray.GetPoint(distance);
				temp.y += 1;
				gameObject.transform.position = temp;
			}
			if (placeBufferTime <= 0){
				if (canPlace && Input.GetMouseButtonUp(0)){
					networkView.RPC ("PlaceBuilding", RPCMode.All);
				}
			}
			else{
				placeBufferTime -= Time.deltaTime;
			}
		}
		if (!destroyed) {
			if (networkView.isMine){					
				if (placing){
					if (canPlace) {
						this.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);		
					} 
					else {
						this.renderer.material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);		
					}
				}
				else{
					if (selected) {
						this.renderer.material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);		
					} 
					else if (hover) {
						this.renderer.material.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);		
					}
					else {
						this.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);	
					}
				}
			}
			else{
				if (placing){
					this.enabled = false;
				}
				else{
					this.enabled = true;
				}
			}
		}
	}
	
	void OnMouseEnter(){
		if (!destroyed && !placing){
			hover = true;
		}
	}
	
	void OnMouseExit(){
		if (!destroyed && !placing){
			hover = false;
		}
	}
	
	void OnMouseDown(){
		if (!destroyed && !placing){
			selected = true;
		}
	}
	
	void OnTriggerEnter(){
		canPlace = false;
	}
	
	void OnTriggerExit(){
		canPlace = true;
	}
	
	void OnTriggerStay(){
		canPlace = false;
	}

	public void SendDamage(float damage){
		networkView.RPC("Damage", RPCMode.All, damage);
	}
	
	[RPC]
	void Damage(float damage){
		health -= damage;
		damagePartSys.Emit ((int)damage * 10);
	}
}
