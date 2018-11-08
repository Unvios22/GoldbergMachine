using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour {

	[SerializeField] private GameObject _speherePrefab;
	[SerializeField] private float _spawnInterval;
	[SerializeField] private int _spawnAmount;
	private int _spawnedAmount;


	private void Start() {
		StartCoroutine(Spawn());
	}

	private IEnumerator Spawn() {
		while (_spawnedAmount < _spawnAmount) {
			SpawnBall();
			_spawnedAmount++;
			yield return new WaitForSeconds(_spawnInterval);
		}
	}

	private void SpawnBall() {
		Instantiate(_speherePrefab,transform.position, Quaternion.identity);
	}
}
