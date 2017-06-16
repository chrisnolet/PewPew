using UnityEngine;

public class Laser : MonoBehaviour {
  public float speed = 10f;
  public float lifetime = 1f;

  void Start() {

    // Set the initial velocity
    GetComponent<Rigidbody>().velocity = new Vector3(0, speed, 0); // May require: transform.rotation * new Vector3(0, speed, 0);

    // Destroy after lifetime expires
    Object.Destroy(gameObject, lifetime);
  }
}
