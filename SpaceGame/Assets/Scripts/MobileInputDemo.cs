using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputDemo : MonoBehaviour {

	[SerializeField] GameObject m_gameObject = null;

	void Update ()
	{
#if UNITY_ANDROID
		foreach (UnityEngine.Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit))
				{
					GameObject go = Instantiate(m_gameObject, raycastHit.point, Quaternion.identity);
					Destroy(go, 4.0f);
				}
			}
		}
#else
       if (Input.GetMouseButtonDown(0))
       {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit raycastHit;
           if (Physics.Raycast(ray, out raycastHit))
           {
               GameObject go = Instantiate(m_gameObject, raycastHit.point, Quaternion.identity);
               Destroy(go, 4.0f);
           }
       }
#endif
	}
}
