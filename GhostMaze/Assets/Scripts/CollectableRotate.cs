using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRotate : MonoBehaviour {
	void Start()
	{
		float initialRotation = Random.Range(0, 360.0f);
		transform.Rotate(Vector3.up, initialRotation);
	}
	void Update()
	{
		transform.Rotate(Vector3.up, Time.deltaTime * 90);
	}
}
