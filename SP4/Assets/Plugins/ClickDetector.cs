using UnityEngine;
using System.Collections;
 
public class ClickDetector : MonoBehaviour
{
	public AudioClip[] AudioClipSFX;

	public bool HandleLeftClick = true;
	public bool HandleRightClick = true;
	public bool HandleMiddleClick = false;
	public string OnLeftClickMethodName = "OnLeftClick";
	public string OnRightClickMethodName = "OnRightClick";
	public string OnMiddleClickMethodName = "OnMiddleClick";
	public LayerMask layerMask;
 
	public Vector2 clickOnWorld;

	void Update()
	{
	GameObject clickedGmObj = null;
        bool clickedGmObjAcquired = false;
 
        // Left click
        if (HandleLeftClick && Input.GetMouseButtonDown(0))
        {
            /*if (!clickedGmObjAcquired)
            {*/
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
			PlaySound(4);
			/*}*/

 
            if (clickedGmObj != null)
			{
                clickedGmObj.SendMessage(OnLeftClickMethodName, null, SendMessageOptions.DontRequireReceiver);

			}
        }
 
        // Right click
        if (HandleRightClick && Input.GetMouseButtonDown(1))
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// create a plane at 0,0,0 whose normal points to +Y:
			Plane hPlane = new Plane(Vector3.up, Vector3.zero);
			// Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
			float distance = 0; 
			// if the ray hits the plane...
			if (hPlane.Raycast(ray, out distance)){
				// get the hit point:
				clickOnWorld.x = ray.GetPoint(distance).x;
				clickOnWorld.y = ray.GetPoint(distance).z;
			}

            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
 
            if (clickedGmObj != null)
                clickedGmObj.SendMessage(OnRightClickMethodName, null, SendMessageOptions.DontRequireReceiver);

			bool press = false;
			if (press == false)
			{
				int i = Random.Range(1,4);
				PlaySound(i-1);
			}
			press = true;
        }
 
        // Middle click
        if (HandleMiddleClick && Input.GetMouseButtonDown(2))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
 
            if (clickedGmObj != null)
                clickedGmObj.SendMessage(OnMiddleClickMethodName, null, SendMessageOptions.DontRequireReceiver);
        }
	
	//Mouse Scrolling
	if (Input.GetAxis("Mouse ScrollWheel") < 0)
	{
		if (Camera.main.fieldOfView <= 150)
			Camera.main.fieldOfView += 20;
		
		if (Camera.main.orthographicSize <= 50)
			Camera.main.orthographicSize += 0.5f;
		
	}
	if (Input.GetAxis("Mouse ScrollWheel") > 0)	
	{
		if (Camera.main.fieldOfView>2)
			Camera.main.fieldOfView -= 20;
		
		if (Camera.main.orthographicSize >= 1)
			
			Camera.main.orthographicSize -= 0.5f;
	}
	if (Input.GetKeyUp(KeyCode.B))	
	{
		if (Camera.main.orthographic == true)
			Camera.main.orthographic = false;
		
		else
			Camera.main.orthographic = true;
	}
    }
 
    GameObject GetClickedGameObject()
    {
        // Builds a ray from camera point of view to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
 
        // Casts the ray and get the first game object hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            return hit.transform.gameObject;
        else
            return null;

		/*
 			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// create a plane at 0,0,0 whose normal points to +Y:
			Plane hPlane = new Plane(Vector3.up, Vector3.zero);
			// Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
			float distance = 0; 
			// if the ray hits the plane...
			if (hPlane.Raycast(ray, out distance)){
				// get the hit point:
				clickOnWorld.x = ray.GetPoint(distance).x;
				clickOnWorld.y = ray.GetPoint(distance).z;
			}
		 */

    }
	void PlaySound(int clip)
	{
		//audio.volume = settings.SFXSliderValue;
		audio.clip = AudioClipSFX [clip];
		audio.Play ();
	}

}
