using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	[SerializeField] AudioClip m_explosionSFX = null, m_laserSFX = null, m_playerDieSFX = null, m_collectSFX = null, m_beamSFX = null;
	AudioSource m_source;
	// Use this for initialization
	void Start () {
		m_source = GetComponent<AudioSource>();
		World.ap = this;
	}
	public void PlayExplosion()
	{
		m_source.pitch = Random.Range(0.8f, 1.2f);
		m_source.PlayOneShot(m_explosionSFX);
		CameraShake.cam.Shake(0.6f);
	}
	public void PlayLaser()
	{
		m_source.pitch = Random.Range(0.8f, 1.2f);
		m_source.PlayOneShot(m_laserSFX);
		CameraShake.cam.Shake(0.2f);
	}
	public void PlayCollect()
	{
		m_source.pitch = Random.Range(0.8f, 1.2f);
		m_source.PlayOneShot(m_collectSFX);
	}
	public void PlayBeam()
	{
		m_source.pitch = Random.Range(0.8f, 1.2f);
		m_source.PlayOneShot(m_beamSFX);
		CameraShake.cam.Shake(0.2f);
	}
	public void PlayPlayerDeath()
	{
		transform.GetChild(0).GetComponent<AudioSource>().Stop();
		m_source.pitch = 1.0f;
		m_source.PlayOneShot(m_playerDieSFX);
		CameraShake.cam.Shake(0.8f);
	}
}
