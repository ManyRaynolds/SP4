using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (NetworkView))]

public class Networking : MonoBehaviour {
	
	public bool build = false;
	public bool resource = false;

	public AudioClip[] AudioClipSFX;

	public string ipAddress = "127.0.0.1";
	public int port = 25167;
	public int maxConnections = 2;

	public GUIStyle unitstyle;
	public GUIStyle resourcestyle;
	public GUIStyle buildingstyle;
	public GUIStyle resourcebuildingstyle;

	public GameObject[] UnitPrefabs;
	public GameObject[] BuildingPrefabs;

	public GameObject[] SpawnPoints;

	//public GameObject GameController;

	//for chatting
	public List<string> chatLog = new List<string>();
	public string currentMessage = "";

	public string playername = " default";

	public GameObject playerObject;

	public struct PlayerInformation{
		public string name;
		public NetworkPlayer player;
		public bool ready;
		public bool winner;
	}
	public List<PlayerInformation> playerInfoList = new List<PlayerInformation>();

	public int gold = 0;
	public float goldTimer = 0;
	public float goldTime = 0;
	public int goldAmount = 0;

	public bool gameStarted = false;
	public bool gameOver = false;

	public GameObject myBase;

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;

		if (!gameObject.GetComponent<NetworkView> ()) {
			gameObject.AddComponent<NetworkView>();
			gameObject.networkView.observed = this;
			gameObject.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
			gameObject.networkView.viewID = Network.AllocateViewID();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (gameOver) {
			return;		
		}
		if (Network.peerType == NetworkPeerType.Disconnected) {
			chatLog.Clear();
			currentMessage = "";
		}
		else if (gameStarted){
			goldTimer += Time.deltaTime;
			if (goldTimer >= goldTime){
				goldTimer -= goldTime;
				gold += goldAmount;
			}
			if (myBase.GetComponent<BaseBuilding>().destroyed){
				networkView.RPC ("UpdateWinner", RPCMode.All,Network.player);
			}
		}
		else if (!gameStarted){	
			bool temp = true;
			foreach(PlayerInformation pi in playerInfoList){
				if (!pi.ready){
					temp = false;
				}
			}
			if (playerInfoList.Count > 0){
				gameStarted = temp;
				if (gameStarted){
					if (Network.isServer){
						myBase = Network.Instantiate(BuildingPrefabs[2], SpawnPoints[0].transform.position, Quaternion.identity, 0) as GameObject;
						myBase.GetComponent<Building>().placing = false;
					}
					else if (Network.isClient){
						myBase = Network.Instantiate(BuildingPrefabs[2], SpawnPoints[1].transform.position, Quaternion.identity, 0) as GameObject;
						myBase.GetComponent<Building>().placing = false;
					}
				}
			}
		}
	}

	void OnGUI(){

		if (gameOver) {
			if (playerInfoList[0].winner){
				GUI.Window(0, new Rect(Screen.width / 4, Screen.height /3, Screen.width/4, Screen.height/100*42), GUIWINDOWLOSE, "Game Over");
			}
			else{
				GUI.Window(0, new Rect(Screen.width / 4, Screen.height /3, Screen.width/2, Screen.height/100*20), GUIWINDOWWIN, "Game Over");
			
			}
			return;		
		}
				if (Network.peerType == NetworkPeerType.Disconnected) {
						//ip text box
						GUI.BeginGroup (new Rect (10, 10, 800, 600));
						GUI.Label (new Rect (0.0f, 0.0f, 100, 25), "IP: ");
						ipAddress = GUI.TextField (new Rect (40.0f, 0.0f, 100, 25), ipAddress);
						//port text box
						GUI.Label (new Rect (0.0f, 30.0f, 100, 25), "Port: ");
						string tempport;
						tempport = GUI.TextField (new Rect (40.0f, 30.0f, 100, 25), port.ToString ());
						port = int.Parse (tempport);
						//player name text box
						GUI.Label (new Rect (0.0f, 60.0f, 100, 25), "Name: ");
						playername = GUI.TextField (new Rect (40.0f, 60.0f, 100, 25), playername);


						//connect button
						if (GUI.Button (new Rect (0.0f, 90.0f, 125, 25), "Connect")) {
								print ("Connecting to " + ipAddress + " : " + port.ToString ());
								Network.Connect (ipAddress, port);
								StartCoroutine (SendJoinMessage ());
								//StartCoroutine(OnConnect ());
						}
						//host server button
						if (GUI.Button (new Rect (0.0f, 120.0f, 125, 25), "Host")) {
								print ("Hosting server on " + ipAddress + " : " + port.ToString ());
								Network.InitializeServer (maxConnections, port, false);
								ipAddress = Network.player.ipAddress;
								StartCoroutine (SendJoinMessage ());
								//StartCoroutine(OnConnect ());
						}
						GUI.EndGroup ();
		} 
		else {
			GUI.BeginGroup (new Rect (10, 10, Screen.width, Screen.height));
			if (!gameStarted){
						//disconnect button
						if (GUI.Button (new Rect (0.0f, 0.0f, 125, 25), "Disconnect")) {
								Network.Disconnect (200);
						}
						//display server info
						GUI.Label (new Rect (0.0f, 30.0f, 200, 25), "IP: " + ipAddress);
						GUI.Label (new Rect (0.0f, 45.0f, 100, 25), "Port: " + port);
						GUI.Label (new Rect (0.0f, 60.0f, 200, 25), "Players: " + (Network.connections.Length + 1));

						//display connected players
						int playerIndex = 0;
						foreach (PlayerInformation pi in playerInfoList) {
								++playerIndex;
								GUI.Label (new Rect (200.0f, 10 + 12.5f * playerIndex, Screen.width, 25), pi.name + " (" + pi.ready + ")");
						}

						//ready button
				if (GUI.Button (new Rect (0.0f, 85.0f, 125, 25), "Ready")) {
					this.networkView.RPC ("UpdateReady", RPCMode.All, Network.player, !playerInfoList [0].ready);
				}
				GUI.Box(new Rect(150, 0, 200,150), "Lobby");
			}


						//chat input
						GUI.SetNextControlName ("chatfield");
						currentMessage = GUI.TextField (new Rect (0.0f, Screen.height -49, 190, 20), currentMessage);

						if (GUI.Button (new Rect (Screen.width / 100 * 25, Screen.height -49, 60, 20), "Send")) {
								if (currentMessage.Length > 0) {
										string temp = "[" + playername + "]: " + currentMessage;
										this.networkView.RPC ("Chat", RPCMode.All, temp);
										currentMessage = "";
								}
						}
						if (Event.current.isKey && Event.current.keyCode == KeyCode.Return) { 
								if (GUI.GetNameOfFocusedControl () == "chatfield") {	
										if (currentMessage.Length > 0) {
												string temp = "[" + playername + "]: " + currentMessage;
												this.networkView.RPC ("Chat", RPCMode.All, temp);
												currentMessage = "";
										}
								} else {
										GUI.FocusControl ("chatfield");
								}

						}
						//chat log
						int chatindex = 0;

			foreach (string msg in chatLog) {
				++chatindex;
					GUI.Label (new Rect (0.0f, Screen.height - 72 - 12.5f * (chatLog.Count - chatindex), Screen.width, 25), msg);

			}
					GUI.Box(new Rect(0.0f, Screen.height-451 ,260, 400),"");


			//=======================================
			//      Enforces it to build function
			//=======================================
			if (gameStarted){
				//GUI.Label (new Rect (0.0f, 30.0f, 200, 25), "Gold: " + gold);
				GUI.Box(new Rect(Screen.width-220, Screen.height/100*2, 200,300), "Gold: " + gold);
				//Debug.Log("Gold: " + gold);
				//GUI.Label (new Rect (Screen.width/100*20, Screen.height/100*4, (float)Screen.width/100*20, (float)Screen.height), "You WIN!");
				if (GUI.Button (new Rect (Screen.width -180, Screen.height / 100*8, Screen.width / 100 * 15, Screen.height / 100 * 5), "", unitstyle)) 
				{
					//AudioClip units = AudioClip.Create ("SFX/Units", 44100, 1, 44100, false, true);
					//sfx = this.audio;
					build = true;
					//Debug.Log("build: " + build);
					//audio.Play ();
				}
				//===========================
				//  If its in build function
				//===========================
				if (build == true) 
				{
					if (GUI.Button (new Rect (Screen.width -160, Screen.height / 100 * 15, 80, 60), "", buildingstyle))
					{
						if (gold >= BuildingPrefabs [0].GetComponent<Building> ().cost) 
						{
							//AudioClip units = AudioClip.Create ("SFX/Units", 44100, 1, 44100, false, true);
							//sfx = this.audio;
							gold -= BuildingPrefabs [0].GetComponent<Building> ().cost;
							Network.Instantiate (BuildingPrefabs [0], Vector3.zero, Quaternion.identity, 0);
							build = false;
							PlaySound(1);
						}
					}
					else if (Input.GetMouseButtonDown (1) || Input.GetKey(KeyCode.Escape))
					{
						build = false;
						PlaySound(2);
					}
				} 

				
				//==========================
				//    Resources Building
				//==========================
				if (GUI.Button (new Rect (Screen.width - 180, Screen.height / 100 * 32, Screen.width / 100 * 15, Screen.height / 100 * 5), "", resourcestyle)) 
				{
					//AudioClip units = AudioClip.Create ("SFX/Units", 44100, 1, 44100, false, true);
					//sfx = this.audio;
					resource = true;
					//Debug.Log("build: " + build);
					//audio.Play ();
				}
				//===========================
				//  If its in build function
				//===========================
				if (resource == true) 
				{
					if (GUI.Button (new Rect (Screen.width -160, Screen.height / 100 * 40, 80, 60), "", resourcebuildingstyle))
					{
						if (gold >= BuildingPrefabs [1].GetComponent<Building> ().cost) 
						{
							//AudioClip units = AudioClip.Create ("SFX/Units", 44100, 1, 44100, false, true);
							//sfx = this.audio;
							gold -= BuildingPrefabs [1].GetComponent<Building> ().cost;
							Network.Instantiate (BuildingPrefabs [1], Vector3.zero, Quaternion.identity, 0);
							resource = false;
							PlaySound(1);
						}
					}
					else if (Input.GetMouseButtonDown (1) || Input.GetKey(KeyCode.Escape)) 
					{
						resource = false;
						PlaySound(2);
					}
				}
			}
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

	[RPC]
	public void UpdateWinner(NetworkPlayer player){
		gameOver = true;
		for (int i = 0; i < playerInfoList.Count; ++i) {
			if (playerInfoList[i].player == player){	
				PlayerInformation pi = new PlayerInformation();
				pi = playerInfoList[i];
				pi.winner = true;
				playerInfoList[i] = pi;
				break;
			}
		}
	}

	IEnumerator SendJoinMessage(){
		yield return new WaitForSeconds (0.01f);
		if (Network.peerType == NetworkPeerType.Server) {
			this.networkView.RPC ("Chat", RPCMode.All, "");
			Vector3 temp = playerObject.transform.position;
			temp.x += 20;
			//Network.Instantiate(playerObject, temp, playerObject.transform.rotation, 0);

			playerInfoList.Clear();
			networkView.RPC("AddPlayer", RPCMode.All, playername, Network.player, false);
		}
		else if (Network.peerType == NetworkPeerType.Client) {
			this.networkView.RPC ("Chat", RPCMode.All, playername + " has joined the server");
			//Network.Instantiate(playerObject, playerObject.transform.position, playerObject.transform.rotation, 0);
			
			playerInfoList.Clear();
			networkView.RPC("AddPlayer", RPCMode.All, playername, Network.player, false);
		} 
		else {
			StartCoroutine (SendJoinMessage ());
		}
	}

	void PlaySound(int clip)
	{
		audio.volume = Settings.SFXSliderValue;
		audio.clip = AudioClipSFX [clip];
		audio.Play ();
	}

	void LoginWindow(int WindowID)
	{
	}
		void GUIWINDOWWIN (int WindowID)
		{

			GUI.Label (new Rect (Screen.width/100*23, Screen.height/100*4, (float)Screen.width/100*20, (float)Screen.height), "You WIN!");
		if (GUI.Button (new Rect (Screen.width/100*17, Screen.height/100*10, 150, 30), "Return To Main Menu"))
			{
				Network.Disconnect (200);
				Initiate.Fade("Main Menu", Color.black, 0.5f);
			}
		}
	void GUIWINDOWLOSE (int WindowID)
		{
			GUI.Label (new Rect (Screen.width/100*23, Screen.height/100*4, (float)Screen.width/100*20, (float)Screen.height), "You LOSE!");

		if (GUI.Button (new Rect (Screen.width/100*17, Screen.height/100*10, 150, 30), "Return To Main Menu"))
			{
				Network.Disconnect (200);
				Initiate.Fade("Main Menu", Color.black, 0.5f);
			}
		}
}