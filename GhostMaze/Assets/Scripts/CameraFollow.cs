using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform followTarget;
	[SerializeField] float smoothTime = 0.3F;
	[SerializeField] Vector3 followOffset;
	private Vector3 velocity = Vector3.zero;
	void Update ()
	{
		Vector3 targetPosition = followTarget.position + followOffset;//followTarget.TransformPoint(followOffset);
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
	}
}
