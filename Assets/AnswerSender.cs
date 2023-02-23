using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSender : MonoBehaviour
{
    public int self;
	public bool isCorrect;
	
    public void Correcthai()
    {
		isCorrect=true;
	}

 void OnTriggerEnter(Collider col)
	{
		if(col.tag=="Player")
		{	if(col.gameObject.GetComponent<PlayerMovement>().isPowered!=true)
			{
				if(isCorrect)
				{
					LevelManager.Instance.Correct();
					col.gameObject.GetComponent<PlayerMovement>().Correct();
					Destroy(gameObject);
				}
				else
				{
					col.gameObject.GetComponent<PlayerMovement>().Die();
					Invoke("GameOver",2f);
					GetComponent<MeshRenderer>().enabled = false;
				}
			
			
			}
		}
		
	}
	
	void GameOver()
	{
		LevelManager.Instance.GameOver();
		Destroy(gameObject);
	}

	
	
}
