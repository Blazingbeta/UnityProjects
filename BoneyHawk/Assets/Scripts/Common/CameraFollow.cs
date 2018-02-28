using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	[SerializeField] Transform m_followTarget;
	private Vector3 m_offset;
	[SerializeField] public float m_smoothTime;
	Vector3 m_velocity = Vector3.zero;
	private void Start()
	{
		m_offset = transform.position - m_followTarget.transform.position;
	}
	void Update ()
	{
		Vector3 targetPos = m_followTarget.transform.position + m_offset;
		Debug.DrawLine(transform.position, m_followTarget.transform.position);
		Debug.DrawLine(transform.position, targetPos, Color.green);
		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref m_velocity, m_smoothTime);
	}
}
