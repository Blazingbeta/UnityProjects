using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTest : MonoBehaviour {
	[SerializeField] GameObject player, ragdoll, forcePoint, coffin, coffindoll, explosion;

	public void Die(float impactAmount, Vector3 direction)
	{
		impactAmount *= 3;
		GetComponent<Collider>().enabled = false;
		player.SetActive(false);
		ragdoll.SetActive(true);
		coffin.SetActive(false);
		coffindoll.SetActive(true);
		explosion.SetActive(true);
		forcePoint.GetComponent<Rigidbody>().AddForce(direction * impactAmount, ForceMode.Impulse);
		coffindoll.GetComponent<Rigidbody>().AddForce(direction * impactAmount, ForceMode.Impulse);
	}
}
