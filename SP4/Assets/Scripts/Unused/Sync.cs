using UnityEngine;
using System.Collections;

public class Sync : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Network.peerType != NetworkPeerType.Disconnected) {
			Network.Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!networkView.isMine) {
		}
	}
}
