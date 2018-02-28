using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	//Enemy
	[SerializeField] float m_spawnTime = 5.0f;
	[SerializeField] GameObject m_enemyPrefab = null;
	[SerializeField] AnimationCurve m_difficultyCurve;
	[SerializeField] int m_scoreDifficultyCap = 25;
	[SerializeField] float m_maxSpawnTimeReduction = 2.0f;
	//Air
	[SerializeField] float m_airSpawnTime = 5.0f;
	[SerializeField] float m_airValue = 7.0f;
	[SerializeField] GameObject m_airPrefab = null;
	//UI
	[SerializeField] Text m_scoreText = null;
	[SerializeField] Text m_airText = null;
	[SerializeField] Slider m_airSlider = null;
	[SerializeField] GameObject m_deathScreen;
	[SerializeField] Text m_highscoreText = null;
	//Misc
	[SerializeField] GameObject m_explosion = null;
	//Personal
	bool m_gameRunning = true;
	int m_score = 0;
	float m_airMeter = 10.0f;

	void Awake()
	{
		World.gm = this;
	}
	void Start()
	{
		StartCoroutine(SpawnEnemies());
		StartCoroutine(SpawnAir());
	}
	void Update()
	{
		if (m_gameRunning)
		{
			m_airMeter -= Time.deltaTime/2;
			if (m_airMeter < 0)
			{
				GameOver();
			}
			else
			{
				m_airSlider.value = m_airMeter;
				m_airText.text = "Air: " + (int)m_airMeter;
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene("Main");
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}
	}
	IEnumerator SpawnEnemies()
	{
		while (m_gameRunning)
		{
			SpawnEnemy();
			float difficulty = m_spawnTime - (m_difficultyCurve.Evaluate((float)m_score / m_scoreDifficultyCap)*m_maxSpawnTimeReduction);
			yield return new WaitForSeconds(Mathf.Max(difficulty, m_maxSpawnTimeReduction));
		}
	}
	IEnumerator SpawnAir()
	{
		yield return new WaitForSeconds(2.0f);
		while (m_gameRunning)
		{
			Instantiate(m_airPrefab).transform.position = World.GetRandomSpawnPos();
			yield return new WaitForSeconds(m_airSpawnTime);
		}
	}
	IEnumerator DeathMenuFade()
	{
		CanvasGroup cg = m_deathScreen.GetComponent<CanvasGroup>();
		float alpha = 0.0f;
		yield return new WaitForSeconds(2.0f);
		while(alpha < 1)
		{
			alpha = Mathf.Clamp01(alpha + Time.deltaTime);
			cg.alpha = alpha;
			yield return null;
		}
		cg.alpha = 1.0f;
	}
	public void GameOver()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Instantiate(m_explosion, player.transform.position, Quaternion.identity);
		Destroy(player);
		m_gameRunning = false;

		//Player death fx
		World.ap.PlayPlayerDeath();

		int highscore = Mathf.Max(m_score, PlayerPrefs.GetInt("Highscore", 0));
		m_highscoreText.text = "Highscore: " + highscore;
		PlayerPrefs.SetInt("Highscore", highscore);
		PlayerPrefs.Save();

		StartCoroutine(DeathMenuFade());
	}
	void SpawnEnemy()
	{
		Instantiate(m_enemyPrefab);
	}
	public void KillEnemy(GameObject source)
	{
		if (m_gameRunning)
		{
			m_score++;
			m_scoreText.text = "Score: " + m_score;
			Instantiate(m_explosion, source.transform.position, Quaternion.identity);
			Destroy(source);
			World.ap.PlayExplosion();
		}
	}
	public void CollectAir()
	{
		m_airMeter = Mathf.Min(m_airMeter + m_airValue, 10.0f);
		World.ap.PlayCollect();
	}
}
