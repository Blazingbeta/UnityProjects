using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWrap : MonoBehaviour {
	void Update ()
	{
		World.WrapBorder(transform);
	}
}
