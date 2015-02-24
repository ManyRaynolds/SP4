using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour {

//	[HideInInspector]
	public float MoveSpeed = 0.0f, RotSpeed = 0.0f;
	private float rotAngle = 0.0f;
	public float MAX_ANGLE = 60.0f;
	private bool offset = false, init = false, bRot = false;
	private int timer = 0;
	public Vector3 Dir = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Init
		if (!init)
		{
			if (RotSpeed != 0.0f)
				bRot = true;

			init = true;
		}

		//Rotation
		if (bRot)
		{
			if (!offset)
			{
				this.transform.RotateAround (this.transform.position, new Vector3 (0, 0, 1), -MAX_ANGLE*0.5f);
				offset = true;
			}

			else
			{
				this.transform.RotateAround (this.transform.position, new Vector3 (0, 0, 1), RotSpeed);
				rotAngle += RotSpeed;
				if (RotSpeed > 0)
				{
					if (rotAngle >= MAX_ANGLE)
					{
						RotSpeed *= -1;
						rotAngle = 0.0f;
					}
				}
				else
				{
					if (rotAngle <= -MAX_ANGLE)
					{
						RotSpeed *= -1;
						rotAngle = 0.0f;
					}
				}
			}
		}

		this.transform.Translate (Dir * MoveSpeed * Time.deltaTime);

		MoveSpeed = 0.0f;
		Dir = Vector3.zero;

	//	this.transform.Translate (MoveSpeed, 0, 0);
	//	++timer;

	//	if (timer > 370) 
	//	{
	//		timer = 0;
	//		MoveSpeed *= -1;
	//	}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
	//	if (col.gameObject.tag == "Star")
	//		Destroy(col.gameObject);
	}
}