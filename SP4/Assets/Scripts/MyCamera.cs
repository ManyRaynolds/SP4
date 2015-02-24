using UnityEngine;
using System.Collections;

public class MyCamera : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        int scrollDistance = 5;
        float scrollSpeed = 70;
        
        if (mousePosX < scrollDistance)
        {
            transform.Translate(Vector3.right * -scrollSpeed *Time.deltaTime, Space.World);
      	}
        if (mousePosX >= Screen.width - scrollDistance)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime, Space.World);
        }
        if (mousePosY < scrollDistance)
        {
            transform.Translate(transform.forward * -scrollSpeed * Time.deltaTime, Space.World);
        }
        if (mousePosY >= Screen.height - scrollDistance)
        {
            transform.Translate(transform.forward * scrollSpeed * Time.deltaTime, Space.World);
        }
    }
}
