using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
	private static LevelManager _instance;
	int points;
	public Scorer scorer;
	

    public static LevelManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
	
	public void OnEnable()
	{
		points=0;
		
		
	}
	
	public void OnLevelFinish()
	{
		scorer.updateScore(points);
		GameManager.Instance.IncreaseLevel();
		GameManager.Instance.IncreaseCoin(points);
		GameManager.Instance.OnEndStage();
		MenuManager.Instance.ChangeMenu("finish");
	}
	public void Correct()
	{
		points=points+20;
		Debug.Log(points);
	}	
	
	                
  

	public void GameOver()
	{
		GameManager.Instance.IncreaseCoin(points);
		scorer.updateScore(points);
		GameManager.Instance.OnEndStage();
		MenuManager.Instance.ChangeMenu("gameover");
		
	}
	
	
}
