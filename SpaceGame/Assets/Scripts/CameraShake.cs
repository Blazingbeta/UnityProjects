using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	[SerializeField] float m_shakeAmplitude = 1.0f;
	[SerializeField] float m_shakeRate = 1.0f;

	Vector3 m_position = Vector3.zero;
	Vector3 m_shake = Vector3.zero;
	float m_shakeAmount = 0.0f;
	public static CameraShake cam;
	private void Start()
	{
		cam = this;
		m_position = Vector3.back * 10;
	}
	void LateUpdate()
	{
		transform.position = m_position + m_shake;
	}

	public void Shake(float amount)
	{
		m_shakeAmount = m_shakeAmount + amount;

	}
	private void Update()
	{
		m_shakeAmount = Mathf.Clamp01(m_shakeAmount - Time.deltaTime);

		float time = Time.time * m_shakeRate;
		m_shake.x = m_shakeAmount * m_shakeAmplitude * ((Mathf.PerlinNoise(time, 0.0f) * 2) - 1);
		m_shake.y = m_shakeAmount * m_shakeAmplitude * ((Mathf.PerlinNoise(0.0f, time) * 2) - 1);
		m_shake.z = 0.0f;
	}
}
