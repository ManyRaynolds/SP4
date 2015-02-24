using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {     

    //**** Variables ****//
    public const short MAX_BLOCK = 15;
    public GameObject block1; 
    
    public int worldWidth  = 100;
    public int worldHeight  = 100;

    public float spawnSpeed = 0;

    private int Index = 0;

    public enum TileType
    	{
	        WALKABLE = 1,
	        NONWALKABLE = 2,
	};

    void  Start () {
        //StartCoroutine(CreateWorld());
        CreateWorld();
    }
    
       // IEnumerator CreateWorld () {
        void CreateWorld () {
        for(int x = 0; x < worldWidth; x++) {
            //yield return new WaitForSeconds(spawnSpeed);          
            for(int z = 0; z < worldHeight; z++) {                
            //yield return new WaitForSeconds(spawnSpeed);   
            GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
            block.transform.parent = transform;
            block.transform.localPosition = new Vector3(x, 0, z);
            }
        }
    }
}
