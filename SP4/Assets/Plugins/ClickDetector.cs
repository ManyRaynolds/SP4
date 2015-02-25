using UnityEngine;
using System.Collections;
 
public class ClickDetector : MonoBehaviour
{
    public bool HandleLeftClick = true;
    public bool HandleRightClick = true;
    public bool HandleMiddleClick = false;
    public string OnLeftClickMethodName = "OnLeftClick";
    public string OnRightClickMethodName = "OnRightClick";
    public string OnMiddleClickMethodName = "OnMiddleClick";
    public LayerMask layerMask;
 
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
            /*}*/
 
            if (clickedGmObj != null)
                clickedGmObj.SendMessage(OnLeftClickMethodName, null, SendMessageOptions.DontRequireReceiver);
        }
 
        // Right click
        if (HandleRightClick && Input.GetMouseButtonDown(1))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
 
            if (clickedGmObj != null)
                clickedGmObj.SendMessage(OnRightClickMethodName, null, SendMessageOptions.DontRequireReceiver);
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
    }
}
