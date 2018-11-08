using System;
using System.Collections;
using UnityEngine;

namespace Code {
	public class ReturnToStartPosition : MonoBehaviour {

		[SerializeField] private float _waitTime;
		[SerializeField] private int _returnTime;
		private Vector3 _startingPosition;
		private Quaternion _startingRotation;
		private Rigidbody _rigidbody;
		private Transform _transform;
		
		private void Start() {
			_startingPosition = transform.position;
			_startingRotation = transform.rotation;
			_rigidbody = transform.GetComponent<Rigidbody>();
			_transform = transform;
		}

		private void OnEnable() {
			GameObject.FindWithTag("Trigger").GetComponent<ReturnToStartPositionTrigger>().TriggerEvent += OnTrigger;
		}

		private void OnDisable() {
			if (GameObject.FindWithTag("Trigger") != null) {
				GameObject.FindWithTag("Trigger").GetComponent<ReturnToStartPositionTrigger>().TriggerEvent -= OnTrigger;
			}
		}

		private void OnTrigger(){
			_rigidbody.drag = 0f;
			_rigidbody.angularDrag = 0.05f;
			StartCoroutine(Return(_waitTime,_returnTime));
		}

		private IEnumerator Return(float delay, float lerpTime) {
			yield return new WaitForSeconds(delay);
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
			_rigidbody.isKinematic = true;
			_transform.gameObject.layer = LayerMask.NameToLayer("PhysicsNoCollision");
			
			float elapsedTime = 0;
			Vector3 lerpStartPosition = _transform.position;
			Quaternion lerpStartRotation = _transform.rotation;

			while (elapsedTime < lerpTime) {
				_transform.position = Vector3.Slerp(lerpStartPosition, _startingPosition, (elapsedTime / _returnTime));
				_transform.rotation = Quaternion.Slerp(lerpStartRotation, _startingRotation, elapsedTime / _returnTime);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitForSeconds(0.5f);
			_transform.gameObject.layer = LayerMask.NameToLayer("Default");
			_rigidbody.isKinematic = false;
			_rigidbody.drag = 100000;
			_rigidbody.angularDrag = 100000;
		}
	}
}
