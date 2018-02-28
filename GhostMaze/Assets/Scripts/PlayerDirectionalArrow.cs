using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionalArrow : MonoBehaviour {
	[SerializeField] Transform m_positionMimic = null;
	[SerializeField] Transform m_pointTowards = null;
	[SerializeField] [Range(0.5f, 5.0f)] float m_turnSpeed = 1.0f;
	void LateUpdate () {
		transform.position = m_positionMimic.position;
		Vector3 direction = Vector3.zero;
		direction = m_pointTowards.position - transform.position;
		transform.rotation =
			Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction.normalized), Time.deltaTime * m_turnSpeed);
	}
}
