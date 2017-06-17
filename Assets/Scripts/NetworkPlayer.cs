using UnityEngine;
using UnityEngine.Networking;
using HoloToolkit.Unity.InputModule;

public class NetworkPlayer : NetworkBehaviour {
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

  void Start() {

    // Local player only
    if (isLocalPlayer) {

      // Attach player to the main camera
      transform.SetParent(Camera.main.transform, false);

      // Listen for HoloLens input events
      if (InputManager.Instance) {
        InputManager.Instance.AddGlobalListener(gameObject);
      }
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

  void Fire() {
    CmdFire();
    fireWaitTimer = maxFireWaitTime;
  }

  void OnInputClicked(InputClickedEventData eventData) {

    // Fire on HoloLens input click
    if (canFire) {
      Fire();
    }
  }

  void OnDestroy() {

    // Remove HoloLens input event listener
    if (isLocalPlayer && InputManager.Instance) {
      InputManager.Instance.RemoveGlobalListener(gameObject);
    }
  }

  [Command]
  void CmdSetPositionAndRotationOffset(Vector3 position, Quaternion rotation) {

    // Update syncvars on server
    positionOffset = position;
    rotationOffset = rotation;
  }

  [Command]
  void CmdFire() {
    var laserRotation = transform.rotation * Quaternion.Euler(90, 0, 0);
    var laser = Instantiate(laserPrefab, laserSpawn.transform.position, laserRotation);

    // Spawn on the clients
    NetworkServer.Spawn(laser);
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
