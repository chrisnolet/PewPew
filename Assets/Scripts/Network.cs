using UnityEngine;
using UnityEngine.Networking;

public class Network : MonoBehaviour {
    public bool isHost;
    public string networkAddress = "192.168.43.174";

	// Use this for initialization
	void Start () {
        if (isHost) {
            NetworkManager.singleton.StartHost();
        }   else {
            NetworkManager.singleton.networkAddress = networkAddress;
            NetworkManager.singleton.StartClient();
        }
	}
}
