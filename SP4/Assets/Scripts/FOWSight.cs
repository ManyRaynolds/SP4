using UnityEngine;
using System.Collections;

public class FOWSight : MonoBehaviour {

	public float radius = 500.0f;
	public LayerMask layermask = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach(Collider col in Physics.OverlapSphere(transform.position, radius, layermask))
		{
			col.SendMessage("Observed",SendMessageOptions.DontRequireReceiver);
//			if(col.renderer != null)
//			{
//				col.renderer.enabled = true;
//			}
		}
	}
}
