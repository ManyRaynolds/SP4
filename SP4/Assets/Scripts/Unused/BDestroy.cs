using UnityEngine;
using System.Collections;

public class BDestroy : MonoBehaviour 
{
	public Units tank , human;
	public Units Ibullet;

	void OnCollisionEnter2D(Collision2D col)
	{
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.tag == "JTank") 
		{			
			tank = col.gameObject.GetComponent<Units>();
			if(tank != null)
			{
				tank.health -= 10.0f;
				Destroy(gameObject);
			}
		}

		else if (col.gameObject.tag == "JHuman") 
		{			
			human = col.gameObject.GetComponent<Units>();
			if(human != null)
			{
				human.health -= 10.0f;
				Destroy(gameObject);
			}
		}

		else if (col.gameObject.tag == "JBullet") 
		{
		}
	}
}
