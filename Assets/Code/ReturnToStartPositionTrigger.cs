using UnityEngine;

namespace Code {
	public class ReturnToStartPositionTrigger : MonoBehaviour {

		public delegate void Trigger();
		public event Trigger TriggerEvent;

		private void OnTriggerEnter(Collider other) {
			if (!other.CompareTag("Player")) { return; }
			if (TriggerEvent != null) TriggerEvent();
			other.attachedRigidbody.useGravity = false;
			other.attachedRigidbody.angularDrag = 0f;
		}
	}
}
