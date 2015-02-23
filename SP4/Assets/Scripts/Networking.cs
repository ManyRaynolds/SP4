using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour {

	public string ipAddress = "127.0.0.1";
	public int port = 25167;
	public int maxConnections = 10;

	public GameObject GameController;

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
				StartCoroutine(OnConnect ());
			}
			//host server button
			if (GUILayout.Button("Host")){
				print ("Hosting server on " + ipAddress + " : " + port.ToString());
				Network.InitializeServer(maxConnections, port, false);
				StartCoroutine(OnConnect ());
			}
		}
		else{
			if (GUILayout.Button ("Disconnect")){
				Network.Disconnect(200);
			}
		}
	}
	IEnumerator OnConnect(){
		yield return new WaitForSeconds (1);
		if (Network.peerType == NetworkPeerType.Connecting) {
			StartCoroutine (OnConnect ());
		} 
		else {
			GameObject go = GameController.GetComponent<Game>().UnitBuildingPrefabs[0];
			GameObject go1 = Network.Instantiate(go, go.transform.position, go.transform.rotation, 0) as GameObject;
			GameController.GetComponent<Game>().unitBuildingList.Add(go1);
		}
	}
}



















