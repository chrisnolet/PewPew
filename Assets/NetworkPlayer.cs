using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
  private GameObject laserPrefab;

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
    NetworkServer.Spawn(laserPrefab);
    RpcFire();
  }

  [ClientRpc]
  void RpcFire() {

  }
}
