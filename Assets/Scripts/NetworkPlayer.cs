using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
  public GameObject laserPrefab;

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
    var laser = Instantiate(laserPrefab);
    laser.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);

    // Spawn on the clients
    NetworkServer.Spawn(laser);

    RpcFire();
  }

  [ClientRpc]
  void RpcFire() {

  }
}
