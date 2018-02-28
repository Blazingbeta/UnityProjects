using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {
	[SerializeField] float m_speed, m_turnSpeed, m_maxTurn;
	[SerializeField] AnimationCurve m_turnFalloff;
	[SerializeField] CanvasGroup m_dieGroup, m_winGroup;
	[SerializeField] AudioClip m_sfx;

	float m_currentRotation = 0.0f;

	public bool m_gameOver = false;
	void Start () {
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if (m_gameOver) return;
		float inputRotation = Input.GetAxis("Horizontal");
		if (inputRotation > 0)
		{
			float turnMultiplier = (m_currentRotation > 0) ? 1 - (Mathf.Abs(m_currentRotation) / m_maxTurn) : 1.0f;
			m_currentRotation += (inputRotation * m_turnSpeed * Time.deltaTime) * m_turnFalloff.Evaluate(turnMultiplier);
		}
		else if(inputRotation < 0)
		{
			float turnMultiplier = (m_currentRotation < 0) ? 1 - (Mathf.Abs(m_currentRotation) / m_maxTurn) : 1.0f;
			m_currentRotation += (inputRotation * m_turnSpeed * Time.deltaTime) * m_turnFalloff.Evaluate(turnMultiplier);
		}
		Vector3 velocity = Vector3.zero;
		transform.rotation = Quaternion.Euler(0.0f, m_currentRotation, 0.0f);
		velocity.z = m_speed * Time.deltaTime;
		transform.position += transform.rotation * velocity;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (m_gameOver) return;
		if (collision.gameObject.tag == "Hazard")
		{
			GameOver();
		}
		if(collision.gameObject.tag == "Win")
		{
			GameWin();
		}
	}
	void GameOver()
	{
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<RagdollTest>().Die(m_speed, transform.forward);
		m_gameOver = true;
		StartCoroutine(DeathSlowdown());
		Camera.main.GetComponent<AudioSource>().Stop();
		Camera.main.GetComponent<AudioSource>().PlayOneShot(m_sfx);
	}
	void GameWin()
	{
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<RagdollTest>().Die(m_speed, transform.forward);
		m_gameOver = true;
		StartCoroutine(DeathSlowdown());
		StartCoroutine(WinTimers());
		Camera.main.GetComponent<AudioSource>().Stop();
		Camera.main.GetComponent<AudioSource>().PlayOneShot(m_sfx);
	}
	IEnumerator WinTimers()
	{
		float currentTime = 0.0f;
		CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
		while (currentTime < 1.0f)
		{
			currentTime += Time.deltaTime/2;
			cam.m_smoothTime = Mathf.Lerp(cam.m_smoothTime, 0.0f, currentTime);
			m_winGroup.alpha = currentTime;
			yield return null;
		}
		cam.m_smoothTime = 0.0f;
		yield return new WaitForSecondsRealtime(3.0f);
		currentTime = 0.0f;

		Color startColor = Camera.main.backgroundColor;
		Color endColor = Color.black;

		while(currentTime < 1.0f)
		{
			currentTime += Time.deltaTime / 5.0f;
			Camera.main.backgroundColor = Color.Lerp(startColor, endColor, currentTime);
			yield return null;
		}
		m_dieGroup.alpha = 0.0f;
		Camera.main.backgroundColor = Color.black;
	}
	IEnumerator DeathSlowdown()
	{
		float currentTime = 0.0f;
		while (currentTime < 1.0f)
		{
			currentTime += Time.unscaledDeltaTime;
			Time.timeScale = 0.3f * currentTime;
			Time.fixedDeltaTime = 0.3f * 0.02f * currentTime;
			yield return null;
		}
		Time.timeScale = 0.3f;
		Time.fixedDeltaTime = 0.3f * 0.02f;

		yield return new WaitForSecondsRealtime(1.0f);
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f;
		yield return new WaitForSeconds(1.0f);
		currentTime = 0.0f;
		while(currentTime < 1.0f)
		{
			currentTime += Time.deltaTime;
			m_dieGroup.alpha = currentTime;
			yield return null;
		}
	}
}
