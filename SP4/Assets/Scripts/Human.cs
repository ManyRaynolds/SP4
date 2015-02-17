using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour 
{
	Vector3 position;
	Vector3 direction;
	public int state;

	public int damage;
	float speed = 0.3f;
	public int health;

	// Use this for initialization
	void Start () 
	{
//		damage = 5;
//		health = 100;

//		guiText.text = "Damage : " + damage;
//		guiText.text = "Health : " + health;
//		guiText.text = "Speed : " + speed;
//		guiText.text = "Position : " + position;
//		guiText.text = "Direction : " + direction;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (state) 
		{
			case 1: //idle
			{
				//transform.Translate (speed * direction.x, speed * direction.y, 0);
				transform.Translate (speed * direction.x, 0, 0);
				if (transform.position.x > 50.0f || transform.position.x < 9.9f) 
				{
					direction = -direction;
				}
//				if (transform.position.y > 50.0f || transform.position.y < 9.9f)
//				{
//					direction = -direction;
//				}
			}
			break;
		}
	}
}