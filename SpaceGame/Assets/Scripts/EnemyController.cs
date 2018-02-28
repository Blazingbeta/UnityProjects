using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	[SerializeField] float m_fadeTime = 1.0f;
	[SerializeField] float m_moveSpeed = 1.0f;
	Color m_currentColor;
	SpriteRenderer m_spawnSprite;
	SpriteRenderer m_sprite;

	bool m_isActive = false;

	void Start ()
	{
		transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
		transform.position = World.GetRandomSpawnPos();

		m_sprite      = transform.GetChild(0).GetComponent<SpriteRenderer>();
		m_spawnSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
		m_currentColor = Color.white;
		m_currentColor.a = 0.0f;
		m_spawnSprite.color = m_currentColor;
		m_sprite.color = m_currentColor;
		StartCoroutine(FadeIn());
	}
	void Update()
	{
		if (!m_isActive) return;
		transform.position += transform.right * m_moveSpeed * Time.deltaTime;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(m_isActive && other.gameObject.tag == "Bullet")
		{
			Destroy(other.gameObject);
			World.gm.KillEnemy(gameObject);
		}
	}
		IEnumerator FadeIn()
	{
		float m_currentFade = 0.0f;
		while (m_currentFade < 1)
		{
			yield return null;
			m_currentFade = Mathf.Clamp01(m_currentFade + (Time.deltaTime / m_fadeTime));
			m_currentColor.a = m_currentFade;
			m_spawnSprite.color = m_currentColor;
		}
		m_isActive = true;
		m_sprite.color = m_currentColor;
		m_spawnSprite.enabled = false;
		GetComponent<BoxCollider2D>().enabled = true;
		World.ap.PlayBeam();
		GetComponent<ParticleSystem>().Play();
	}

}
