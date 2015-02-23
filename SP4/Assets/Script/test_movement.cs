using UnityEngine;
using System.Collections;

public class test_movement : MonoBehaviour {
	//Character_Movement
	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			test_object obj = target.GetComponent<test_object>();
			obj.MoveSpeed = 1.0f;
			obj.Direction = new Vector3(-1,0,0); 
			//Flip ();
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			test_object obj = target.GetComponent<test_object>();
			obj.MoveSpeed = 1.0f;
			obj.Direction = new Vector3(1,0,0);
			//Flip ();
		}	
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			test_object obj = target.GetComponent<test_object>();
			obj.MoveSpeed = 1.0f;
			obj.Direction = new Vector3(0,1,0);
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			test_object obj = target.GetComponent<test_object>();
			obj.MoveSpeed = 1.0f;
			obj.Direction = new Vector3(0,-1,0);
		}

	}
}
