using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenaryVariation : MonoBehaviour {
	[SerializeField] float m_scaleChange = 0.2f;
	void Start ()
	{
		transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 5.0f);
		Vector3 newScale = Vector3.zero;
		newScale.x = (1 + Random.Range(0.0f, m_scaleChange * 2) - m_scaleChange) * transform.localScale.x;
		newScale.y = (1 + Random.Range(0.0f, m_scaleChange * 2) - m_scaleChange) * transform.localScale.y;
		newScale.z = (1 + Random.Range(0.0f, m_scaleChange * 2) - m_scaleChange) * transform.localScale.z;
		transform.localScale = newScale;
	}
}
