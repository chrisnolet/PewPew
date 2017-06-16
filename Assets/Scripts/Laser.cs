using UnityEngine;

public class Laser : MonoBehaviour {
  public float speed = 10f;

  void Start() {

    // Set the initial velocity
    GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
  }
}
