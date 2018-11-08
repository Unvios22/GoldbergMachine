using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactivateGravity : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			other.attachedRigidbody.angularDrag = 0.05f;
			other.attachedRigidbody.useGravity = true;
		}
	}
}
