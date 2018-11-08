using UnityEngine;

namespace Code {
	public class BallBooster : MonoBehaviour {
		[SerializeField] private float _boostForce;
	
		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Player")) {
				other.attachedRigidbody.AddForce(_boostForce * Vector3.forward);
			}
		}
	}
}
