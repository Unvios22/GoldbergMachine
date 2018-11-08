using UnityEngine;

namespace Code {
	public class ReturnToStartPositionTrigger : MonoBehaviour {

		public delegate void Trigger();
		public event Trigger TriggerEvent;

		private void OnTriggerEnter(Collider other) {
			if (TriggerEvent != null) TriggerEvent();
		}
	}
}
