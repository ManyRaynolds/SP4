using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour {

	public string ipAddress = "127.0.0.1";
	public int port = 25167;
	public int maxConnections = 10;

	//public GameObject GameController;

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		if (Network.peerType == NetworkPeerType.Disconnected) {
			//ip text box
			//GUILayout.BeginHorizontal();
			GUI.BeginGroup (new Rect (10, 10, 800, 600));
			GUI.Label(new Rect(0.0f, 0.0f, 100, 25), "IP: ");
			ipAddress = GUI.TextField(new Rect(30.0f, 0.0f, 100, 25), ipAddress);
			
			GUI.Label(new Rect(0.0f, 30.0f, 100, 25), "Port: ");
			string tempport;
			tempport = GUI.TextField(new Rect(30.0f, 30.0f, 100, 25), port.ToString());
			port = int.Parse(tempport);
			
			//connect button
			if (GUI.Button(new Rect(0.0f, 60.0f, 125, 25),"Connect")){
				print ("Connecting to " + ipAddress + " : " + port.ToString());
				Network.Connect(ipAddress, port);
				//StartCoroutine(OnConnect ());
			}
			//host server button
			if (GUI.Button(new Rect(0.0f, 90.0f, 125, 25),"Host")){
				print ("Hosting server on " + ipAddress + " : " + port.ToString());
				Network.InitializeServer(maxConnections, port, false);
				//StartCoroutine(OnConnect ());
			}
			GUI.EndGroup ();
		}
		else{
			GUI.BeginGroup (new Rect (10, 10, 800, 600));
			if (GUI.Button (new Rect(0.0f, 0.0f, 125, 25), "Disconnect")){
				Network.Disconnect(200);
			}
			GUI.Label(new Rect(0.0f, 30.0f, 100, 25), "IP: " + ipAddress);
			GUI.Label(new Rect(0.0f, 50.0f, 100, 25), "Port: " + port);
			GUI.Label(new Rect(0.0f, 70.0f, 200, 25), "Players: " + (Network.connections.Length + 1));

			int j = 0;
			foreach(NetworkPlayer i in Network.connections){
				GUI.Label(new Rect(0.0f, 70.0f + j * 20, 200, 25), " - " + Network.connections.ToString());
				++j;
			}
			GUI.EndGroup ();


		}

		

			//GUILayout.Label("IP Address");
			//ipAddress = GUILayout.TextField(ipAddress, 20);
			//GUILayout.EndHorizontal();			
			//port textbox
			/*GUILayout.BeginHorizontal();
			GUILayout.Label("Port");
			string tempport;
			tempport = GUILayout.TextField(port.ToString());
			port = int.Parse(tempport);
			GUILayout.EndHorizontal();
			//connect button
			if (GUILayout.Button("Connect")){
				print ("Connecting to " + ipAddress + " : " + port.ToString());
				Network.Connect(ipAddress, port);
				//StartCoroutine(OnConnect ());
			}
			//host server button
			if (GUILayout.Button("Host")){
				print ("Hosting server on " + ipAddress + " : " + port.ToString());
				Network.InitializeServer(maxConnections, port, false);
				//StartCoroutine(OnConnect ());
			}
		}
		else{
			if (GUILayout.Button ("Disconnect")){
				Network.Disconnect(200);
			}*/
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
}



















