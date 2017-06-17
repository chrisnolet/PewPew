using UnityEngine;
using UnityEngine.Networking;
using HoloToolkit.Unity.InputModule;

public class NetworkPlayer : NetworkBehaviour, IInputClickHandler {
  public GameObject laserPrefab;
  public GameObject head;
  public GameObject laserSpawn;
  public float maxFireWaitTime = 0.20f;

  #pragma warning disable 0414
  [SyncVar(hook = "OnPositionOffsetChanged")] private Vector3 positionOffset;
  [SyncVar(hook = "OnRotationOffsetChanged")] private Quaternion rotationOffset;
  #pragma warning restore 0414

  private float fireWaitTimer;

  private bool canFire {
    get {
      return (fireWaitTimer <= 0);
    }
  }

  public void TakeDamage() {
    CmdTakeDamage();
  }

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

      // Decrease fire wait timer
      if (fireWaitTimer > 0) {
        fireWaitTimer -= Time.deltaTime;
      }

      // Input events
      if (Input.GetButton("Fire1") && canFire) {
        Fire();
      }
    }
  }

  public void OnInputClicked(InputClickedEventData eventData) {

    // Fire on HoloLens input click
    if (isLocalPlayer && canFire) {
      Fire();
    }
  }

  void Fire() {
    var laserRotation = transform.rotation * Quaternion.Euler(90, 0, 0);

    // Spawn local instance
    Instantiate(laserPrefab, laserSpawn.transform.position, laserRotation);

    // Instruct the server to spawn on other clients
    CmdFire(laserSpawn.transform.position, laserRotation);

    // Play sound effect for local player laser fire
    // TODO(CN): Play sound

    // Force wait before firing again
    fireWaitTimer = maxFireWaitTime;
  }

  [Command]
  void CmdSetPositionAndRotationOffset(Vector3 position, Quaternion rotation) {

    // Update syncvars on server
    positionOffset = position;
    rotationOffset = rotation;
  }

  [Command]
  void CmdFire(Vector3 position, Quaternion rotation) {

    // Spawn on the clients
    RpcFire(position, rotation);
  }

  [Command]
  void CmdTakeDamage() {
    // TODO(CN): Add syncvar for hit points and decrement here

    RpcTakeDamage();
  }

  [ClientRpc]
  void RpcFire(Vector3 position, Quaternion rotation) {

    // Instantiate laser on clients
    if (!isLocalPlayer) {
      Instantiate(laserPrefab, position, rotation);

      // TODO(CN): Play sound
    }
  }

  [ClientRpc]
  void RpcTakeDamage() {

    // Play sound effect for local player taking damage
    if (isLocalPlayer) {
      // TODO(CN): Play sound
    }
  }

  void OnPositionOffsetChanged(Vector3 value) {

    // Add position offset for remote players only
    if (!isLocalPlayer) {
      head.transform.localPosition = value;
    }
  }

  void OnRotationOffsetChanged(Quaternion value) {

    // Add rotation offset for remote players only
    if (!isLocalPlayer) {
      head.transform.localRotation = value;
    }
  }
}
