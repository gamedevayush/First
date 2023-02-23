using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			if (PlayerMovement.Instance != null)
			{
				PlayerMovement.Instance.UseMagnet();
				Destroy(gameObject);
			}
		}
	}
}
