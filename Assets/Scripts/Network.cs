﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.VR;

public class Network : MonoBehaviour {
  public string networkAddress = "192.168.43.174";

  void Start () {

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
