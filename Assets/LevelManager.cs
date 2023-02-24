using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
	private static LevelManager _instance;
	int points;
	public Scorer scorer;
	public TMP_Text CoinCounter;
	float lerpval;
	public int coinFactor;
	AudioSource ass;

	public static LevelManager Instance { get { return _instance; } }


	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	public void OnEnable()
	{
		ass = GetComponent<AudioSource>();
		coinFactor = 1;
		points = 0;
		CoinCounter.text = points.ToString();


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
		points = points + (20*coinFactor);
		updateScore();
	}
	public void AddCoin()
	{
		points=points+(1*coinFactor);
		updateScore();
		ass.Play();
	}



	public void GameOver()
	{
		GameManager.Instance.IncreaseCoin(points);
		scorer.updateScore(points);
		GameManager.Instance.OnEndStage();
		MenuManager.Instance.ChangeMenu("gameover");
		int g = Random.Range(0, 3);
		if (g == 1)
		{
			AdmobController.Instance.ShowInterstitialAd();
		}
	}

	public void updateScore()
	{
		CoinCounter.text = ((int)points).ToString();
	}
	public void Coin2x()
	{
		coinFactor = 2;
		PlayerMovement.Instance.Use2x();
		StartCoroutine(ResetFactor());
	}

	IEnumerator ResetFactor()
	{
		yield return new WaitForSeconds(10f);
		coinFactor = 1;
	}

}