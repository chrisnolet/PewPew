using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
  public GameObject laserPrefab;

  public GameObject sphere;

  [SyncVar(hook = "OnPositionOffsetChanged")] private Vector3 positionOffset;
  [SyncVar(hook = "OnRotationOffsetChanged")] private Quaternion rotationOffset;

  void Start() {

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
    sphere.transform.position = value;
  }

  void OnRotationOffsetChanged(Quaternion value) {
    sphere.transform.rotation = value;
  }
}
