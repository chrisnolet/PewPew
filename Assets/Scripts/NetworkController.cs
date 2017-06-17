using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.VR;

public class NetworkController : MonoBehaviour {
  public string networkAddress = "10.0.1.3";

  void Start () {

    // Increase tolerence for packet loss
    NetworkManager.singleton.connectionConfig.NetworkDropThreshold = 90;
    NetworkManager.singleton.connectionConfig.OverflowDropThreshold = 90;

    // Detect host device
    if (VRDevice.isPresent) {

      // Automatically join game when running on HoloLens
      NetworkManager.singleton.networkAddress = networkAddress;
      NetworkManager.singleton.StartClient();
    }
    else {

      // Automatically host when running on desktop
      NetworkManager.singleton.StartHost();
    }
  }
}
