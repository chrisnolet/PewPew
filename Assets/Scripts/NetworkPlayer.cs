﻿using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
  public GameObject laserPrefab;

  public GameObject sphere;

  [SyncVar(hook = "OnPositionOffsetChanged")] private Vector3 positionOffset;
  [SyncVar(hook = "OnRotationOffsetChanged")] private Quaternion rotationOffset;

  void Start() {

    // Local player only
    if (isLocalPlayer) {

      // Attach player to the main camera
      transform.SetParent(Camera.main.transform, false);
    }
  }

  void Update() {

    // Local player only
    if (isLocalPlayer) {

      // Input events
      if (Input.GetButton("Fire1")) {
        CmdFire();
      }
    }
  }

  [Command]
  void CmdFire() {
    var laser = Instantiate(laserPrefab, transform.position, transform.rotation); // May require: transform.rotation * Quaternion.Euler(90, 0, 0);

    // Spawn on the clients
    NetworkServer.Spawn(laser);

    RpcFire();
  }

  [ClientRpc]
  void RpcFire() {

  }

  void OnPositionOffsetChanged(Vector3 value) {

    // Add position offset for remote players only
    if (!isLocalPlayer) {
      sphere.transform.localPosition = value;
    }
  }

  void OnRotationOffsetChanged(Quaternion value) {

    // Add rotation offset for remote players only
    if (!isLocalPlayer) {
      sphere.transform.localRotation = value;
    }
  }
}
