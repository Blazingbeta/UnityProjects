using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
	static float m_leftBorder, m_rightBorder, m_upBorder, m_downBorder;
	static bool m_initialized = false;
	public static GameManager gm = null;
	public static AudioPlayer ap = null;
	static void Init()
	{
		//World Boundry Code
		float dist    = (Vector3.zero - Camera.main.transform.position).z;
		m_leftBorder  = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		m_rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		m_upBorder    = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
		m_downBorder  = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
		m_initialized = true;
	}
	public static void WrapBorder(Transform transform)
	{
		if (!m_initialized) Init();
		if (transform.position.x > m_rightBorder) transform.position += Vector3.right * m_leftBorder  * 2;
		if (transform.position.x < m_leftBorder)  transform.position += Vector3.right * m_rightBorder * 2;
		if (transform.position.y > m_upBorder)    transform.position += Vector3.up    * m_downBorder  * 2;
		if (transform.position.y < m_downBorder)  transform.position += Vector3.up    * m_upBorder    * 2;
	}
	public static Vector3 GetRandomSpawnPos()
	{
		if (!m_initialized) Init();
		Vector3 pos = Vector3.zero;
		pos.x = Random.Range(m_leftBorder + 0.5f, m_rightBorder-0.5f);
		pos.y = Random.Range(m_downBorder + 0.5f, m_upBorder - 0.5f);
		pos.z = 0.0f;
		return pos;
	}
}
