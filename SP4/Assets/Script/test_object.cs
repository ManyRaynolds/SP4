using UnityEngine;
using System.Collections;

public class test_object : MonoBehaviour {

	public float MoveSpeed = 0.0f;
	public Vector3 Direction = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Direction * MoveSpeed * Time.deltaTime);
		MoveSpeed = 0.0f;
		Direction = Vector3.zero;
	}
}
