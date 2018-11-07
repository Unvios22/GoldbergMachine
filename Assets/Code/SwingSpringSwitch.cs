using System.Collections;
using UnityEngine;

namespace Code {
	public class SwingSpringSwitch : MonoBehaviour {

		[SerializeField] private HingeJoint _swingJoint;
		[SerializeField] private float _waitTime = 1f;

		private void Start() {
			_swingJoint = this.GetComponentInChildren<HingeJoint>();
		}

		private void OnTriggerEnter(Collider other) {
			_swingJoint.useSpring = false;
		}

		private void OnTriggerExit(Collider other) {
			StartCoroutine(ReturnToStartingPosition());
		}

		IEnumerator ReturnToStartingPosition() {
			yield return new WaitForSeconds(_waitTime);
			_swingJoint.useSpring = true;
		}
	}
}
