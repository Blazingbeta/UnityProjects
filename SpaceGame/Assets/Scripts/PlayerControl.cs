using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	[SerializeField] float m_chargeGainPerSecond = 1.0f;
	[SerializeField] float m_chargeCap = 25.0f;
	[SerializeField] float m_minimumCharge = 5.0f;
	[SerializeField] float m_maxArrowScale = 3.0f;
	[SerializeField] Gradient m_arrowChargeColor;
	[SerializeField] float m_currentCharge = 0.0f;
	[SerializeField] GameObject m_projectile = null;

	Transform m_pointerArrow = null;
	Transform m_scaler = null;
	Rigidbody2D m_rb = null;
	SpriteRenderer m_arrowSprite = null;
	Transform m_playerSprite = null;
	Vector3 m_direction;
	ParticleSystem m_laserParticle = null;
	private void Start()
	{
		m_rb = GetComponent<Rigidbody2D>();
		m_pointerArrow = transform.GetChild(0);
		//m_pointerArrow.gameObject.SetActive(false);
		m_scaler = m_pointerArrow.GetChild(0);
		m_scaler.localScale = Vector3.zero;
		m_arrowSprite = m_scaler.GetComponentInChildren<SpriteRenderer>();
		m_playerSprite = transform.GetChild(1);
		m_laserParticle = GetComponent<ParticleSystem>();
	}
	private void Update()
	{
		#region Input
		if (Input.GetMouseButtonDown(0))
		{
			//m_pointerArrow.gameObject.SetActive(true);
			//m_scaler.localScale = Vector3.zero;
		}
		else if (Input.GetMouseButton(0))
		{
			m_currentCharge += m_chargeGainPerSecond * Time.deltaTime;
			m_currentCharge = Mathf.Min(m_currentCharge, m_chargeCap);

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = transform.position.z;
			m_direction = (mousePos - transform.position).normalized;

			Vector3 newScale = Vector3.one;
			newScale.x = (m_currentCharge / m_chargeCap) * m_maxArrowScale;

			Vector3 newPosition = Vector3.zero;
			newPosition.x = -(newScale.x - 1.0f) / 2.0f;

			m_scaler.localScale = newScale;
			m_scaler.localPosition = newPosition;

			float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
			m_pointerArrow.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

			m_arrowSprite.color = m_arrowChargeColor.Evaluate(m_currentCharge / m_chargeCap);
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (m_currentCharge > m_minimumCharge)
			{
				GameObject projectile = Instantiate(m_projectile, transform.position, m_pointerArrow.rotation);
				projectile.GetComponent<Rigidbody2D>().AddForce(m_direction * m_currentCharge, ForceMode2D.Impulse);
				m_rb.AddForce(-m_direction * m_currentCharge, ForceMode2D.Impulse);
				World.ap.PlayLaser();
				m_laserParticle.Play();
			}

			m_currentCharge = 0.0f;
			m_scaler.localScale = Vector3.zero;

			//m_pointerArrow.gameObject.SetActive(false);
		}
		#endregion

		if (m_rb.velocity != Vector2.zero)
		{
			float velocityAngle = Mathf.Atan2(m_rb.velocity.y, m_rb.velocity.x) * Mathf.Rad2Deg;
			m_playerSprite.localRotation = Quaternion.AngleAxis(velocityAngle, Vector3.forward);
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") GameOver();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "AirCan")
		{
			Destroy(other.gameObject);
			World.gm.CollectAir();
		}
	}
	void GameOver()
	{
		World.gm.GameOver();
	}
}
