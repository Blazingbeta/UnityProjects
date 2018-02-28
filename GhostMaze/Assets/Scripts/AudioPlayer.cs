using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
	public static AudioPlayer i;
	[SerializeField] AudioClip m_deathSFX = null;
	[SerializeField] AudioClip m_collectSFX = null;
	private void Start()
	{
		i = this;
	}
	public void PlayDeathSFX()
	{
		GetComponent<AudioSource>().PlayOneShot(m_deathSFX);
	}
	public void PlayCollectSFX()
	{
		GetComponent<AudioSource>().PlayOneShot(m_collectSFX);
	}
}
