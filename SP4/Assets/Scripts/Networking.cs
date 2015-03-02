using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (NetworkView))]

public class Networking : MonoBehaviour {

	public string ipAddress = "127.0.0.1";
	public int port = 25167;
	public int maxConnections = 10;

	public GameObject[] UnitPrefabs;
	public GameObject[] UnitBuildingPrefabs;

	//public GameObject GameController;

	//for chatting
	public List<string> chatLog = new List<string>();
	public string currentMessage = "";

	public string playername = "default";

	public GameObject playerObject;

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;

		gameObject.AddComponent<NetworkView>();
		gameObject.networkView.observed = this;
		gameObject.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		gameObject.networkView.viewID = Network.AllocateViewID();
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			chatLog.Clear();
			currentMessage = "";
		}
	}

	void OnGUI(){
		if (Network.peerType == NetworkPeerType.Disconnected) {
			//ip text box
			GUI.BeginGroup (new Rect (10, 10, 800, 600));
			GUI.Label(new Rect(0.0f, 0.0f, 100, 25), "IP: ");
			ipAddress = GUI.TextField(new Rect(40.0f, 0.0f, 100, 25), ipAddress);
			//port text box
			GUI.Label(new Rect(0.0f, 30.0f, 100, 25), "Port: ");
			string tempport;
			tempport = GUI.TextField(new Rect(40.0f, 30.0f, 100, 25), port.ToString());
			port = int.Parse(tempport);
			//player name text box
			GUI.Label(new Rect(0.0f, 60.0f, 100, 25), "Name: ");
			playername = GUI.TextField(new Rect(40.0f, 60.0f, 100, 25), playername);


			//connect button
			if (GUI.Button(new Rect(0.0f, 90.0f, 125, 25),"Connect")){
				print ("Connecting to " + ipAddress + " : " + port.ToString());
				Network.Connect(ipAddress, port);
				StartCoroutine (SendJoinMessage ());
				//StartCoroutine(OnConnect ());
			}
			//host server button
			if (GUI.Button(new Rect(0.0f, 120.0f, 125, 25),"Host")){
				print ("Hosting server on " + ipAddress + " : " + port.ToString());
				Network.InitializeServer(maxConnections, port, false);
				ipAddress = Network.player.ipAddress;
				StartCoroutine (SendJoinMessage ());
				//StartCoroutine(OnConnect ());
			}
			GUI.EndGroup ();
		}
		else{
			GUI.BeginGroup (new Rect (10, 10, 800, 600));
			//disconnect button
			if (GUI.Button (new Rect(0.0f, 0.0f, 125, 25), "Disconnect")){
				Network.Disconnect(200);
			}
			//display server info
			GUI.Label(new Rect(0.0f, 30.0f, 200, 25), "IP: " + ipAddress);
			GUI.Label(new Rect(0.0f, 45.0f, 100, 25), "Port: " + port);
			GUI.Label(new Rect(0.0f, 60.0f, 200, 25), "Players: " + (Network.connections.Length + 1));

			//chat input
			GUI.SetNextControlName("chatfield");
			currentMessage = GUI.TextField(new Rect(0.0f, Screen.height - 45, 200, 25), currentMessage);

			if (GUI.Button(new Rect(205, Screen.height - 45, 50, 25), "Send")){
				if (currentMessage.Length > 0){
					string temp = "[" + playername + "]: " + currentMessage;
					this.networkView.RPC ("Chat", RPCMode.All, temp);
					currentMessage = "";
				}
			}
			if (Event.current.isKey && Event.current.keyCode == KeyCode.Return){ 
				if (GUI.GetNameOfFocusedControl() == "chatfield"){	
					if (currentMessage.Length > 0){
						string temp = "[" + playername + "]: " + currentMessage;
						this.networkView.RPC ("Chat", RPCMode.All, temp);
						currentMessage = "";
					}
				}
				else{
					GUI.FocusControl("chatfield");
				}

			}
			//chat log
			int chatindex = 0;
			foreach(string msg in chatLog){
				++chatindex;
				GUI.Label(new Rect(0.0f, Screen.height - 65 - 12.5f * (chatLog.Count - chatindex), Screen.width, 25), msg);
			}
			
			if (GUI.Button (new Rect(200.0f, 0.0f, 125, 25), "Building")){
				Network.Instantiate(UnitBuildingPrefabs[0], Vector3.zero, Quaternion.identity, 0);
			}
//			if (GUI.Button (new Rect(200.0f, 0.0f, 125, 25), "Units")){
//				Network.Instantiate(UnitBuildingPrefabs[0], Vector3.zero, Quaternion.identity, 0);
//			}

			GUI.EndGroup ();
		}
	}
	/*IEnumerator OnConnect(){
		yield return new WaitForSeconds (1);
		if (Network.peerType == NetworkPeerType.Connecting) {
			StartCoroutine (OnConnect ());
		} 
		else {
			GameObject go = GameController.GetComponent<Game>().UnitBuildingPrefabs[0];
			GameObject go1 = Network.Instantiate(go, go.transform.position, go.transform.rotation, 0) as GameObject;
			GameController.GetComponent<Game>().unitBuildingList.Add(go1);
		}
	}*/

	void OnPlayerDisconnected(NetworkPlayer player){
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	[RPC]		
	public void Chat(string message){
		chatLog.Add (message);
	}

	IEnumerator SendJoinMessage(){
		yield return new WaitForSeconds (0.01f);
		if (Network.peerType == NetworkPeerType.Server) {
			this.networkView.RPC ("Chat", RPCMode.All, "Server started");
			Vector3 temp = playerObject.transform.position;
			temp.x += 20;
			Network.Instantiate(playerObject, temp, playerObject.transform.rotation, 0);
		}
		else if (Network.peerType == NetworkPeerType.Client) {
			this.networkView.RPC ("Chat", RPCMode.All, playername + " has joined the server");
			Network.Instantiate(playerObject, playerObject.transform.position, playerObject.transform.rotation, 0);
		} 
		else {
			StartCoroutine (SendJoinMessage ());
		}
	}
}


















