using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour {
	NavMeshAgent m_agent;
	GameObject m_target;
	void Start ()
	{
		m_agent = GetComponent<NavMeshAgent>();
		m_target = GameObject.FindGameObjectWithTag("Player");
		float currentSpeed = m_agent.speed;
		currentSpeed = Random.Range(currentSpeed - 0.3f, currentSpeed + 0.3f);
		m_agent.speed = currentSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_agent.destination = m_target.transform.position;
	}
}
