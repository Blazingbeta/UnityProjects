using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserMovement : MonoBehaviour {
	GameObject m_target;
	[SerializeField] float m_speed = 1.0f;
	void Start () {
		m_speed = Random.Range(0.7f, 1.2f);
		m_target = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () {
		if (m_target.activeInHierarchy)
		{
			Vector3 direction = m_target.transform.position - transform.position;
			direction.Normalize();
			transform.position += direction * Time.deltaTime * m_speed;
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}
}
