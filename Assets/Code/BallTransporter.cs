using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTransporter : MonoBehaviour{

	private bool _attract;
	private bool _stuck;
	private Transform _transform;
	private Rigidbody _rigidbody;
	private Rigidbody _ballRigidbody;
	private Vector3 _startPosition;
	private Vector3 _dropPosition;

	private void Start(){
		_attract = true;
		_transform = transform;
		_rigidbody = _transform.GetComponent<Rigidbody>();
		_dropPosition = GameObject.Find("Sphere Spawner").transform.position;
	}

	private void Update(){
		if (_attract){
			var collisions = Physics.OverlapSphere(_transform.position,10f);
			if (collisions == null){return;}
			foreach (var collision in collisions){
				if (collision.CompareTag("Player")){
					_ballRigidbody = collision.GetComponent<Rigidbody>();
					_attract = false;
					StartCoroutine(AttractBall(collision.transform));
					break;
				}	
			}
		}
	}

	private IEnumerator AttractBall(Transform ball){
		while (!_stuck){
			var vector = _transform.position - ball.position;
			var length = vector.magnitude;
			var direction = vector.normalized;
			_ballRigidbody.AddForce(200f * direction);
			yield return new WaitForEndOfFrame();	
		}
		yield return null;

	}

	private void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")){
			var joint = gameObject.AddComponent<FixedJoint>();
			joint.connectedBody = _ballRigidbody;
			_stuck = true;
			StartCoroutine(ReturnToStart());
		}
	}

	private IEnumerator ReturnToStart(){
		_ballRigidbody.isKinematic = true;
		_rigidbody.isKinematic = true;
		float elapsedTime = 0;
		while (elapsedTime < 2) {
			_transform.position = Vector3.Lerp(_startPosition, _dropPosition, (elapsedTime / 2));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
