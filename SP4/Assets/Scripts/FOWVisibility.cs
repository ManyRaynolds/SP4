using UnityEngine;
using System.Collections;

public class FOWVisibility : MonoBehaviour {

	private bool observed = false;

	// Use this for initialization
	void Start () {
	
	}

	void Update()
	{
		if(observed)
		{
			renderer.enabled = true;
		}
		else{
			renderer.enabled = false;
		}
		observed = false;
	}

	// Update is called once per frame
	void Observed () 
	{
		Debug.Log("Observed", gameObject);
		observed = true;
	}
}
