using UnityEngine;
using System.Collections;

public class BaseBuilding : Building {

	bool sentWinLose = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		UpdateBuilding ();

		if (destroyed){	
			if (networkView.isMine){
				if (!sentWinLose) {
					//update lose

					//send win to others
					networkView.RPC ("SendWinLose", RPCMode.Others);
				}	
			}
		}
		else{
			
		}
	}

	[RPC]
	void SendWinLose(){
	}

}
