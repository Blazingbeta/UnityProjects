using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager i = null;
	[SerializeField] GameObject m_waypoints = null;
	[SerializeField] GameObject m_collectable = null;
	[SerializeField] GameObject[] m_enemies = null;
	[SerializeField] Text m_scoreDisplay = null;
	[SerializeField] GameObject m_pauseMenu = null;
	int m_score = -1;
	int m_lastSpawnIndex = -1;
	void Start () {
		if (i) Debug.LogError("Warning: GameManager created while one already existed.");
		i = this;
		Time.timeScale = 1.0f;
		Random.InitState(System.DateTime.Now.Millisecond);
		Init();
	}
	void Init()
	{
		int playerSpawnIndex = Random.Range(0, m_waypoints.transform.childCount);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.transform.position = m_waypoints.transform.GetChild(playerSpawnIndex).position;
		m_lastSpawnIndex = playerSpawnIndex;
		CollectScore();
	}
	private void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			TogglePauseMenu();
		}
	}
	public void CollectScore()
	{
		m_score++;
		m_scoreDisplay.text = "Score: " + m_score;
		int waypointIndex = m_lastSpawnIndex;
		while(waypointIndex == m_lastSpawnIndex)
		{
			waypointIndex = Random.Range(0, m_waypoints.transform.childCount);
		}
		Vector3 spawnPos = m_waypoints.transform.GetChild(waypointIndex).position;
		m_collectable.transform.position = spawnPos;
		//Instantiate(m_collectable, spawnPos, Quaternion.identity);

		GameObject enemyToSpawn = m_enemies[Random.Range(0, m_enemies.Length)];
		Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);

		m_lastSpawnIndex = waypointIndex;
	}
	void SpawnNextCollectable()
	{
		int childIndex = Random.Range(0, m_waypoints.transform.childCount);
		Vector3 spawnPos = m_waypoints.transform.GetChild(childIndex).position;
		m_collectable.transform.position = spawnPos;
		//Instantiate(m_collectable, spawnPos, Quaternion.identity);
	}
	void SpawnNextEnemy()
	{
		int waypointIndex = Random.Range(0, m_waypoints.transform.childCount);
		Vector3 spawnPos = m_waypoints.transform.GetChild(waypointIndex).position;
		GameObject enemyToSpawn = m_enemies[Random.Range(0, m_enemies.Length)];
		Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
	}
	void TogglePauseMenu()
	{
		if (m_pauseMenu.activeInHierarchy)
		{
			m_pauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
		}
		else
		{
			m_pauseMenu.SetActive(true);
			Time.timeScale = 0.0f;
		}
	}
	public void ClosePauseMenu()
	{
		if (m_pauseMenu.activeInHierarchy)
		{
			m_pauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
		}
	}
	public void Retry()
	{
		if (m_pauseMenu.activeInHierarchy)
		{
			SceneManager.LoadScene("Dungeon");
		}
	}
	public void QuitGame()
	{
		if (m_pauseMenu.activeInHierarchy)
		{
			Application.Quit();
		}
	}
}
