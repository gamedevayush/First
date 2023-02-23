using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCoinController : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			if (LevelManager.Instance != null)
			{
				LevelManager.Instance.Coin2x();
				Destroy(gameObject);
			}
		}
	}
}
