using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (NetworkView))]

public class Networking : MonoBehaviour {

	public static AudioSource sfx;
	public bool build= false;
	public string ipAddress = "127.0.0.1";
	public int port = 25167;
	public int maxConnections = 10;

	public GUIStyle unitstyle;
	public GUIStyle resourcestyle;
	public GUIStyle buildingstyle;
	public GUIStyle building2style;

	public GameObject[] UnitPrefabs;
	public GameObject[] UnitBuildingPrefabs;

	//public GameObject GameController;

	//for chatting
	public List<string> chatLog = new List<string>();
	public string currentMessage = "";

	public string playername = "default";

	public GameObject playerObject;

	public struct PlayerInformation{
		public string name;
		public NetworkPlayer player;
		public bool ready;
	}
	public List<PlayerInformation> playerInfoList = new List<PlayerInformation>();

	public int gold = 0;
	public float goldTimer = 0;
	public float goldTime = 0;
	public int goldAmount = 0;

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
		else{
			goldTimer += Time.deltaTime;
			if (goldTimer >= goldTime){
				goldTimer -= goldTime;
				gold += goldAmount;
			}
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
			GUI.BeginGroup (new Rect (10, 10, Screen.width, Screen.height));
			//disconnect button
			if (GUI.Button (new Rect(0.0f, 0.0f, 125, 25), "Disconnect")){
				Network.Disconnect(200);
			}
			//display server info
			GUI.Label(new Rect(0.0f, 30.0f, 200, 25), "IP: " + ipAddress);
			GUI.Label(new Rect(0.0f, 45.0f, 100, 25), "Port: " + port);
			GUI.Label(new Rect(0.0f, 60.0f, 200, 25), "Players: " + (Network.connections.Length + 1));

			//display connected players
			int playerIndex = 0;
			foreach (PlayerInformation pi in playerInfoList){
				++playerIndex;
				GUI.Label(new Rect(0.0f, 100 + 12.5f * playerIndex, Screen.width, 25), pi.name + " (" + pi.ready + ")");
			}

			//ready button
			if (GUI.Button (new Rect(0.0f, 85.0f, 125, 25), "Ready")){
				this.networkView.RPC ("UpdateReady", RPCMode.All, Network.player, !playerInfoList[0].ready);
			}


			//chat input
			GUI.SetNextControlName("chatfield");
			currentMessage = GUI.TextField(new Rect(0.0f, Screen.height/100*95, 200, 25), currentMessage);

			if (GUI.Button(new Rect(Screen.width/100*19, Screen.height/100*95, 50, 25), "Send")){
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
				GUI.Label(new Rect(0.0f, Screen.height - 95 - 12.5f * (chatLog.Count - chatindex), Screen.width, 25), msg);
			}
			//=======================================
			//      Enforces it to build function
			//=======================================
			if (GUI.Button (new Rect(200.0f, 0.0f, 80, 25), "", unitstyle)){
<<<<<<< HEAD
				//AudioClip units = AudioClip.Create ("SFX/Units", 44100, 1, 44100, false, true);
				//sfx = this.audio;
=======
				if (gold >= UnitBuildingPrefabs[0].GetComponent<Building>().cost){
					AudioClip units = AudioClip.Create ("SFX/Units", 44100, 1, 44100, false, true);
					sfx = this.audio;
					gold -= UnitBuildingPrefabs[0].GetComponent<Building>().cost;
					Network.Instantiate(UnitBuildingPrefabs[0], Vector3.zero, Quaternion.identity, 0);
				}
>>>>>>> baeaf180439bbd4da4935308283c379926894afe

				build = true;
				//Debug.Log("build: " + build);
				//audio.Play ();
			}
			//===========================
			//  If its in build function
			//===========================
			if(build == true)
			{
				if(GUI.Button(new Rect(200.0f, Screen.height/100*4, 100,100), "", buildingstyle))
				{
					Network.Instantiate(UnitBuildingPrefabs[0], Vector3.zero, Quaternion.identity, 0);
					build = false;
				}
			}

			if (GUI.Button (new Rect(300.0f, 0.0f, 80, 25), "", resourcestyle)){
				if (gold >= UnitBuildingPrefabs[1].GetComponent<Building>().cost){
					gold -= UnitBuildingPrefabs[1].GetComponent<Building>().cost;
				Network.Instantiate(UnitBuildingPrefabs[1], Vector3.zero, Quaternion.identity, 0);
				}
			}
<<<<<<< HEAD
			if (GUI.Button (new Rect(750.0f, 0.0f, 100, 25), "Base")){
=======
				if (GUI.Button (new Rect(400.0f, 0.0f, 100, 25), "Base")){
				if (gold >= UnitBuildingPrefabs[2].GetComponent<Building>().cost){
					gold -= UnitBuildingPrefabs[2].GetComponent<Building>().cost;
>>>>>>> baeaf180439bbd4da4935308283c379926894afe
				Network.Instantiate(UnitBuildingPrefabs[2], Vector3.zero, Quaternion.identity, 0);
					}
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

	void OnPlayerConnected(NetworkPlayer player){
	}

	void OnPlayerDisconnected(NetworkPlayer player){
		for (int i = 0; i < playerInfoList.Count; ++i) {
			if (playerInfoList[i].player == player){
				this.networkView.RPC ("Chat", RPCMode.All, playerInfoList[i].name + " has left the server");
				break;
			}
		}
		this.networkView.RPC ("RemovePlayer", RPCMode.All, player);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	[RPC]		
	public void Chat(string message){
		chatLog.Add (message);
	}

	[RPC]
	public void AddPlayer(string name, NetworkPlayer player, bool ready){
		if (Network.isServer) {
			foreach (PlayerInformation pi in playerInfoList){
				this.networkView.RPC ("AddPlayer", player, pi.name, pi.player, pi.ready);
			}
		}
		foreach (PlayerInformation pi in playerInfoList){
			if (pi.player == player){
				return;
			}
		}
		PlayerInformation playerinfo = new PlayerInformation();
		playerinfo.name = name;
		playerinfo.player = player;
		playerinfo.ready = ready;
		playerInfoList.Add(playerinfo);
	}

	[RPC]
	public void RemovePlayer(NetworkPlayer player){
		for (int i = 0; i < playerInfoList.Count; ++i) {
			if (playerInfoList[i].player == player){
				playerInfoList.RemoveAt(i);
				break;
			}
		}
	}

	[RPC]
	public void UpdateReady(NetworkPlayer player, bool ready){
		for (int i = 0; i < playerInfoList.Count; ++i) {
			if (playerInfoList[i].player == player){	
				PlayerInformation pi = new PlayerInformation();
				pi.name = playerInfoList[i].name;
				pi.player = playerInfoList[i].player;
				pi.ready = ready;
				playerInfoList[i] = pi;
				break;
			}
		}
	}

	IEnumerator SendJoinMessage(){
		yield return new WaitForSeconds (0.01f);
		if (Network.peerType == NetworkPeerType.Server) {
			this.networkView.RPC ("Chat", RPCMode.All, "Server started");
			Vector3 temp = playerObject.transform.position;
			temp.x += 20;
			Network.Instantiate(playerObject, temp, playerObject.transform.rotation, 0);

			playerInfoList.Clear();
			networkView.RPC("AddPlayer", RPCMode.All, playername, Network.player, false);
		}
		else if (Network.peerType == NetworkPeerType.Client) {
			this.networkView.RPC ("Chat", RPCMode.All, playername + " has joined the server");
			Network.Instantiate(playerObject, playerObject.transform.position, playerObject.transform.rotation, 0);
			
			playerInfoList.Clear();
			networkView.RPC("AddPlayer", RPCMode.All, playername, Network.player, false);
		} 
		else {
			StartCoroutine (SendJoinMessage ());
		}
	}
}


















