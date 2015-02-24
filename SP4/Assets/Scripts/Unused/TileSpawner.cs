/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSpawner : MonoBehaviour {

	// *** Variables *** //
	private const short MAX_TILES = 16, MAX_VARIETY = 8;
	public List<GameObject> TileList = new List<GameObject>();
	public static List<GameObject> DefaultTileList = new List<GameObject>();
	public static List<Entity> DefaultEntityList = new List<Entity>();
	public static List<GameObject> VarTileList = new List<GameObject>();
	private short ID = 0;
	private List<SimilarCheck> VarietyList = new List<SimilarCheck>();
	private Vector3 Pos = new Vector3(0.2865f, 0.61f, 0);
	private float SpawnTime = 0.0f, TimeDelay = 2.0f;
	public static float FlipTime = 0.0f, FlipDelay = 10.0f;

	//Use this for initialization
	void Start () {
		//Add Variety
		for (short i = 0; i < MAX_VARIETY; ++i)
		{
			SimilarCheck Temp = new SimilarCheck();
			Temp.ID = i+1;
			Debug.Log ("Variety List (" + i + ") ID: " + Temp.ID);
			VarietyList.Add(Temp);
		}
		Debug.Log ("Variety Count: " + VarietyList.Count);
	}
	
	//Update is called once per frame
	void Update () 
	{
		//Cap No Of Tiles
		if (ID < MAX_TILES)
		{
			//Increment Spawn Time
			SpawnTime += Time.deltaTime * 10.0f;
			
			//Spawn Time Reached
			if (SpawnTime > TimeDelay)
			{
				//Determine Tile's Position
				Pos.x += 0.14f; 
				if (ID == 0 || ID == 4 || ID == 8 || ID == 12)
				{
					Pos.x = 0.2865f;
					if (ID != 0)
						Pos.y -= 0.15f;
				} 

				//Index for tile type
				int Index = 0;

				//Randoms Type of Tile
				Index = Random.Range(1, (int)MAX_VARIETY+1);

				//Check Against Variety
				for (short i = 0; i < VarietyList.Count; ++i)
				{
					bool proceed = true;
					if (Index == VarietyList[i].ID)
					{
						//Cap it at 2
						if (VarietyList[i].count >= 2)
						{
							for (short j = 0; j < VarietyList.Count; ++j)
							{
								if (VarietyList[j].count < 2)
								{
									++VarietyList[j].count;
									Index = VarietyList[j].ID;
									Debug.Log ("Variety: " + VarietyList[j].ID + " | Count: " + VarietyList[j].count);
									proceed = false;
									break;
								}
							}
							break;
						}

						//Adds Count to Variety
						if (proceed)
						{
							++VarietyList[i].count;
							Debug.Log ("Variety: " + VarietyList[i].ID + " | Count: " + VarietyList[i].count);
						}
					}
				}
			
				//Spawns the GameObject
				GameObject SpawnedTile = Instantiate(TileList[Index], Pos, Quaternion.identity) as GameObject;
				VarTileList.Add(SpawnedTile);

				//Spawns Default Tile
				GameObject DefaultTile = Instantiate(TileList[0], new Vector3(Pos.x, Pos.y, -1), Quaternion.identity) as GameObject;
				DefaultTileList.Add(DefaultTile);

				//Creates Entity for Identification
				Entity DefaultEntity = new Entity();
				DefaultEntity.theObject = DefaultTile;
				DefaultEntity.Index = Index;
				DefaultEntityList.Add(DefaultEntity);

				//Create Name Tag for Spawned Tiles
				SpawnedTile.name = "Tile" + (++ID) + "-V:" + Index;
				SpawnedTile.tag = "Tile";

				//Reset Spawn Time
				SpawnTime = 0.0f;
			}
		}

		//Tiles has finished spawning
		else
		{
			//Increment Flip Time
			FlipTime += Time.deltaTime * 10.0f;

			//Flip Tiles After Some Time
			if (FlipTime > FlipDelay)
			{
				//Flip Tiles
				for (short i = 0; i < DefaultTileList.Count; ++i)
				{
					Vector3 newPos = new Vector3(DefaultTileList[i].transform.position.x,
					                             DefaultTileList[i].transform.position.y,
					                             2);
					DefaultTileList[i].transform.position = newPos;
				}
			}
		}
	}
}*/