using UnityEngine;
using System.Collections.Generic;

public class UnitsManager : MonoBehaviour {

	int basicProjecttileDamage = 1;
	int damage;

	public float attack_time;
	public float attack_timer;

	public float weaponRange = 10.0f;

	Vector3 dir;
	public GameObject Bullet;
	public GameObject unit;
	//handle true false
	protected bool moving, rotating;
	protected bool attacking = false;
	protected bool movingIntoPosition = false;
	//set destination and do rotation
	private Vector3 destination;
	private Quaternion targetRotation;
	private int rotateSpeed = 5;
	private int moveSpeed = 5;

	protected Bounds selectionBounds;

	private UnitsControl uc = null;

	protected bool aiming = false;

	private bool TargetInFrontOfWeapon() {
		Vector3 targetLocation = uc.target.transform.position;
		Vector3 direction = targetLocation - transform.position;
		if(direction.normalized == transform.forward.normalized) return true;
		else return false;
	}

	protected virtual void AimAtTarget() {
		aiming = true;
		//this behaviour needs to be specified by a specific object
	}

	/*** Game Engine methods, all can be overridden by subclass ***/

	/// <summary>
	/// S/////////////////////////////////////	/// </summary>
	protected virtual void Start () {
		//SetPlayer();
//		if (this.gameObject.name == "Enemy")
//		{
//			//Set Enemy health here, if have
//		}
//		else
//		{
//			//Set Player health
//		}
		//dir.x = 1;
		//dir.y = 1;
		//dir.z = 1;
		if (this.gameObject.name == "Basic Projectile")
		{
			damage = basicProjecttileDamage;
		}
	}
	
	protected virtual void Update () {
		TargetInRange(uc);
		if(rotating) TurnToTarget();
		//else if(moving) MakeMove(); pathfinding already moves.
		if(attacking && !movingIntoPosition && !aiming) PerformAttack();
		else{}
		//OnCollisionEnter();
	}
	//////////////////////////////////////////////////////////////////////
	private void TurnToTarget() {
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);
		//sometimes it gets stuck exactly 180 degrees out in the calculation and does nothing, this check fixes that
		Quaternion inverseTargetRotation = new Quaternion(-targetRotation.x, -targetRotation.y, -targetRotation.z, -targetRotation.w);
		if(transform.rotation == targetRotation || transform.rotation == inverseTargetRotation) {
			rotating = false;
			moving = true;
		}
	}


	public void StartMove(Vector3 destination) {
		this.destination = destination;
		targetRotation = Quaternion.LookRotation (destination - transform.position);
		rotating = true;
		moving = false;
	}
	private void MakeMove() {
		transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
		if(transform.position == destination) moving = false;
	}


	void OnCollisionEnter(Collision theCollision)//checking for the nearby units
	{
		Debug.Log ("You just hit " + theCollision.gameObject.name);
		if (theCollision.gameObject.tag == "Enemy")
		{
			Debug.Log(theCollision.gameObject.name + " is an enemy! You did " + damage + " damage!");
			Destroy(this.gameObject);
		}
	}
	//////////////////////////////////////////////////////////////////////
	/**
	 * A child class should only determine other conditions under which a decision should
	 * not be made. This could be 'harvesting' for a harvester, for example. Alternatively,
	 * an object that never has to make decisions could just return false.
	 */

	void OnControllerColliderHit(ControllerColliderHit theCollider)
	{
		if (theCollider.gameObject.tag == "Enemy")
		{
			Debug.Log(theCollider.gameObject.name);
		}
	}
			
	public static Rect CalculateSelectionBox(Bounds selectionBounds, Rect playingArea) {
		//shorthand for the coordinates of the centre of the selection bounds
		float cx = selectionBounds.center.x;
		float cy = selectionBounds.center.y;
		float cz = selectionBounds.center.z;
		//shorthand for the coordinates of the extents of the selection bounds
		float ex = selectionBounds.extents.x;
		float ey = selectionBounds.extents.y;
		float ez = selectionBounds.extents.z;
		
		//Determine the screen coordinates for the corners of the selection bounds
		List< Vector3 > corners = new List< Vector3 >();
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx+ex, cy+ey, cz+ez)));
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx+ex, cy+ey, cz-ez)));
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx+ex, cy-ey, cz+ez)));
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx-ex, cy+ey, cz+ez)));
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx+ex, cy-ey, cz-ez)));
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx-ex, cy-ey, cz+ez)));
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx-ex, cy+ey, cz-ez)));
		corners.Add(Camera.mainCamera.WorldToScreenPoint(new Vector3(cx-ex, cy-ey, cz-ez)));
		
		//Determine the bounds on screen for the selection bounds
		Bounds screenBounds = new Bounds(corners[0], Vector3.zero);
		for(int i = 1; i < corners.Count; i++) {
			screenBounds.Encapsulate(corners[i]);
		}
		
		//Screen coordinates start in the bottom left corner, rather than the top left corner
		//this correction is needed to make sure the selection box is drawn in the correct place
		float selectBoxTop = playingArea.height - (screenBounds.center.y + screenBounds.extents.y);
		float selectBoxLeft = screenBounds.center.x - screenBounds.extents.x;
		float selectBoxWidth = 2 * screenBounds.extents.x;
		float selectBoxHeight = 2 * screenBounds.extents.y;
		
		return new Rect(selectBoxLeft, selectBoxTop, selectBoxWidth, selectBoxHeight);
	}


	public void CalculateBounds() {
			selectionBounds = new Bounds(transform.position, Vector3.zero);
			foreach(Renderer r in GetComponentsInChildren< Renderer >()) {
				selectionBounds.Encapsulate(r.bounds);
			}
	}
			
	private bool TargetInRange(UnitsControl uc) { // Check whether in range or not
			Vector3 targetLocation = uc.transform.position;
			Vector3 direction = targetLocation - transform.position;
			if(direction.sqrMagnitude < weaponRange * weaponRange) {
				return true;
			}
			return false;
	}

		
	protected virtual void BeginAttack() {
		if(TargetInRange(uc)) {
			attacking = true;
			PerformAttack();
		} else AdjustPosition();
	}

	private void PerformAttack() {
		if(!uc) {
			attacking = false;
			return;
		}
		if(!TargetInRange(uc)) AdjustPosition();
		else if(!TargetInFrontOfWeapon()) AimAtTarget();
		else UseWeapon();
	}

	private void AdjustPosition() {
		{
			movingIntoPosition = true;
			Vector3 attackPosition = FindNearestAttackPosition();
			StartMove(attackPosition);
			attacking = true;
		}
	}
	private Vector3 FindNearestAttackPosition() {
			Vector3 targetLocation = uc.target.transform.position;
			Vector3 direction = targetLocation - transform.position;
			float targetDistance = direction.magnitude;
			float distanceToTravel = targetDistance - (0.9f * weaponRange);
			return Vector3.Lerp(transform.position, targetLocation, distanceToTravel / targetDistance);
		}

	private void UseWeapon() {
		GameObject[] allObjects = UnityEngine.GameObject.FindObjectsOfType<GameObject>() ;
		foreach(GameObject go in allObjects)	
		{
			if(go.tag == "Enemy")
			{
				if (unit == null || Vector3.Distance(unit.transform.position, this.transform.position) > Vector3.Distance(go.transform.position, this.transform.position) )
				{
					unit = go;
				}
			}
		}
		attack_timer += Time.deltaTime;
		
		if (attack_timer >= attack_time)
		{
			attack_timer -= attack_time;
			GameObject go = Instantiate (Bullet, this.gameObject.transform.position + 2 * new Vector3(1,0,0), this.gameObject.transform.rotation) as GameObject;
			go.rigidbody.velocity = new Vector3(1, 0, 0);
			go.GetComponent<Bullet>().damage = damage;
		}
	}

}
