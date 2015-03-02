using UnityEngine;
using System.Collections;

public class JoinCreate : MonoBehaviour {
	
	public GUIStyle JoinGame;
	public GUIStyle CreateGame;
	
	bool buttonpressed = false;

//	public static string ipAddress = "127.0.0.1";
	public static int port = 25167;
	public int maxConnections = 10;
	public bool startgame = false;
	
	// Use this for initialization
	void Start () 
	{
		Application.runInBackground = true;

		gameObject.AddComponent<NetworkView>();
		gameObject.networkView.observed = this;
		gameObject.networkView.stateSynchronization = NetworkStateSynchronization.ReliableDeltaCompressed;
		gameObject.networkView.viewID = Network.AllocateViewID();
	}
	
	// Update is called once per frame
	void Update () {}
	
	void OnGUI(){
		//buttonpressed = true;
//		if(!buttonpressed)
//		{
		if (Network.peerType == NetworkPeerType.Disconnected) 
		{
			if(GUI.Button(new Rect(((float)Screen.width/100*17), (float)Screen.height/100*60, (float)Screen.width/100*25, (float)Screen.height/100*13), "", CreateGame))
			{
<<<<<<< HEAD
				//Network.InitializeServer(maxConnections, port, false);
=======
				Network.InitializeServer(maxConnections, port, false);
>>>>>>> 56d2a76de522223448b8882592754fff06503730
				//StartCoroutine (SendJoinMessage ());
				//buttonpressed = true;
				//Initiate.Fade("LoginMenu", Color.black,0.5f);
				//Debug.Log("Start Game");
				//GUI.Button = null;
			}
			
			if(GUI.Button(new Rect(((float)Screen.width/100*55), (float)Screen.height/100*60, (float)Screen.width/100*25, (float)Screen.height/100*13), "", JoinGame))
			{
//				Network.Connect(ipAddress, port);
<<<<<<< HEAD
				//Network.Connect (MainMenu.url, port);
				//StartCoroutine (SendJoinMessage ());
=======
				Network.Connect (MainMenu.url, port);
				StartCoroutine (SendJoinMessage ());
>>>>>>> 56d2a76de522223448b8882592754fff06503730
				//buttonpressed = true;
				//Initiate.Fade("Settings", Color.black,0.5f);
				//Debug.Log("Settings");
			}
			
			//		if (GUI.Button(new Rect(((float)Screen.width*0.75), ((float)Screen.height*0.75), 100, 50), "Start Game"))
			//			Debug.Log("Clicked the button with an image");
//		}	
		}
		else{
//			if(Network.peerType != NetworkPeerType.Disconnected)
//			{
<<<<<<< HEAD
				Initiate.Fade("MultiplayerLobby", Color.black, 0.5f);
//			}
//			GUI.Label(new Rect(20.0f, 30.0f, 100, 25), "IP: " + ipAddress);
//			GUI.Label(new Rect(20.0f, 30.0f, 100, 25), "IP: " + MainMenu.url);
//			GUI.Label(new Rect(20.0f, 50.0f, 100, 25), "Port: " + port);
//			GUI.Label(new Rect(20.0f, 70.0f, 200, 25), "Players: " + (Network.connections.Length + 1));
//			
//			int j = 0;
//			foreach(NetworkPlayer i in Network.connections){
//				GUI.Label(new Rect(0.0f, 70.0f + j * 20, 200, 25), " - " + Network.connections.ToString());
//				++j;
//			}
=======
				Initiate.Fade("Grid", Color.black, 0.5f);
//			}
//			GUI.Label(new Rect(20.0f, 30.0f, 100, 25), "IP: " + ipAddress);
			GUI.Label(new Rect(20.0f, 30.0f, 100, 25), "IP: " + MainMenu.url);
			GUI.Label(new Rect(20.0f, 50.0f, 100, 25), "Port: " + port);
			GUI.Label(new Rect(20.0f, 70.0f, 200, 25), "Players: " + (Network.connections.Length + 1));
			
			int j = 0;
			foreach(NetworkPlayer i in Network.connections){
				GUI.Label(new Rect(0.0f, 70.0f + j * 20, 200, 25), " - " + Network.connections.ToString());
				++j;
			}
>>>>>>> 56d2a76de522223448b8882592754fff06503730
		}
	}
	IEnumerator SendJoinMessage(){
		yield return new WaitForSeconds (0.01f);
		if (Network.peerType == NetworkPeerType.Server) {
			this.networkView.RPC ("Chat", RPCMode.All, "Server started");
		}
		else if (Network.peerType == NetworkPeerType.Client) {
			this.networkView.RPC ("Chat", RPCMode.All, LoginMenu.Username + " has joined the server");
		} 
		else {
			StartCoroutine (SendJoinMessage ());
		}
	}
}
