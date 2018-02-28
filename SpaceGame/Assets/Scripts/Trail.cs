using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {
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
					transform.position = raycastHit.point;
               }
           }
       }
#else
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				transform.position = raycastHit.point;
			}
		}
#endif
	}
}
