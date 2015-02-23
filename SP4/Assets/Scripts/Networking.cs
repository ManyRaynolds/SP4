using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour {

	public string ipAddress = "127.0.0.1";
	public int port = 25167;
	public int maxConnections = 10;

	public 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		if (Network.peerType == NetworkPeerType.Disconnected) {
			//ip text box
			GUILayout.BeginHorizontal();
			ipAddress = GUILayout.TextField(ipAddress);
			GUILayout.Label("IP Address");
			GUILayout.EndHorizontal();			
			//port textbox
			GUILayout.BeginHorizontal();
			string tempport;
			tempport = GUILayout.TextField(port.ToString());
			port = int.Parse(tempport);
			GUILayout.Label("Port");
			GUILayout.EndHorizontal();
			//connect button
			if (GUILayout.Button("Connect")){
				print ("Connecting to " + ipAddress + " : " + port.ToString());
				Network.Connect(ipAddress, port);
				OnConnect ();
			}
			//host server button
			if (GUILayout.Button("Host")){
				print ("Hosting server on " + ipAddress + " : " + port.ToString());
				Network.InitializeServer(maxConnections, port, false);
				OnConnect ();
			}
		}
		else{
			if (GUILayout.Button ("Disconnect")){
				Network.Disconnect(200);
			}
		}
	}
	void OnConnect(){
		GameObject[] go = FindObjectsOfType<GameObject> ();
		foreach(GameObject go1 in go){
			Network.Instantiate(go1.gameObject, go1.gameObject.transform.position, go1.gameObject.transform.rotation, 0);
		}
	}
}



















