using UnityEngine;

public class Laser : MonoBehaviour {
  public float speed = 10f;
  public float lifetime = 1f;

  void Start() {

    // Set the initial velocity
    GetComponent<Rigidbody>().velocity = transform.rotation * new Vector3(0, speed, 0);

    // Destroy after lifetime expires
    Object.Destroy(gameObject, lifetime);
  }

  void OnTriggerEnter(Collider other) {
    if (other.tag == Constants.HeadTag) {

      // Do damage to player
      var networkPlayer = other.GetComponentInParent<NetworkPlayer>();
      networkPlayer.TakeDamage();

      // Destroy laser and head
      Destroy(other);
      Destroy(gameObject);
    }
  }
}
