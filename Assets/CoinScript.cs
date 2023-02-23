using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
	bool isTouched;
	void Start()
	{

		

	}
    void OnTriggerEnter(Collider col)
    {
		if (col.tag == "Player")
		{
			if (LevelManager.Instance != null)
			{
				LevelManager.Instance.AddCoin();
				isTouched = true;
				StartCoroutine(LerpPosition());
				Destroy(gameObject, 2f);
			}
		}
		else if (col.tag == "PlayerCo")
		{
			Debug.Log("PoweredCoin");
		}
	}
	IEnumerator LerpPosition()
	{
		float time = 0;
		Vector3 startPosition = transform.position;
		while (time < 0.5f)
		{
			transform.position = Vector3.Lerp(startPosition, new Vector3(transform.position.x, 5f, transform.position.z), time / 0.5f);
			time += Time.deltaTime;
			yield return null;
		}
		//transform.position = new Vector3(transform.position.x, 50, transform.position.z);
	}


}
