using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float m_speed;

	Animator m_animator;
	void Start()
	{
		m_animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
	}

	void Update()
	{
		Vector3 velocity = Vector3.zero;
		Vector3 rotate = Vector3.zero;

		velocity.z = Input.GetAxis("Vertical") * m_speed;
		rotate.y = Input.GetAxis("Horizontal");

		rotate *= 60 * Time.deltaTime * m_speed;
		transform.rotation *= Quaternion.Euler(rotate);

		transform.position += transform.rotation * velocity * Time.deltaTime;

		m_animator.SetBool("Walk", !Mathf.Approximately(velocity.sqrMagnitude, 0.0f));
	}
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			AudioPlayer.i.PlayDeathSFX();
			gameObject.SetActive(false);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Powerup")
		{
			AudioPlayer.i.PlayCollectSFX();
			GameManager.i.CollectScore();
			//Destroy(other.gameObject);
			//Coin SFX here
		}
	}
}
