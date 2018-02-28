using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	[SerializeField] float m_fadeThreshholdSqrd = 1.0f;
	[SerializeField] float m_fadeOutTime = 1.0f;
	Rigidbody2D m_rb;
	void Start ()
	{
		m_rb = GetComponent<Rigidbody2D>();
		StartCoroutine(BulletFadeout());
	}
	IEnumerator BulletFadeout()
	{
		while (m_rb.velocity.sqrMagnitude > m_fadeThreshholdSqrd)
		{
			yield return new WaitForSeconds(0.5f);
		}
		SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
		Color color = sprite.color;
		float m_currentAlpha = 1.0f;
		while(m_currentAlpha > 0)
		{
			m_currentAlpha = Mathf.Clamp01(m_currentAlpha - (Time.deltaTime / m_fadeOutTime));
			color.a = m_currentAlpha;
			sprite.color = color;
			yield return null;
		}
		Destroy(gameObject);
	}
}
