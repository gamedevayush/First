using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
	private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }
	public GameObject LevelMenu;
	public GameObject MainMenu;
	public GameObject GameMenu;
	public GameObject SettingsMenu;
	public GameObject GameOverMenu;
	public GameObject skinMenu;
	public GameObject RewardsMenu;

	public GameObject CoinsPlace;
	public TMP_Text Finisher;
	public TMP_Text FinisherBtn;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
		Handheld.Vibrate();
	}
	
	public void ChangeMenu(string name)
	{
		ResetAll();

		if (name == "reward")
		{
			RewardsMenu.SetActive(true);
			CoinsPlace.SetActive(false);
		}

		if (name=="gamemenu")
		{
			GameMenu.SetActive(true);
			CoinsPlace.SetActive(false);
		}
		if(name=="gameover")
		{
			GameOverMenu.SetActive(true);
			Finisher.text="Game Over";
			FinisherBtn.text="Restart";
			CoinsPlace.SetActive(false);
		}
		if(name=="finish")
		{
			GameOverMenu.SetActive(true);
			Finisher.text="Congratulations!";
			FinisherBtn.text = "NEXT";
			CoinsPlace.SetActive(false);
		}
		if(name=="mainmenu")
		{
			MainMenu.SetActive(true);
			CoinsPlace.SetActive(true);
		}
		if(name=="levelmenu")
		{
			LevelMenu.SetActive(true);
			CoinsPlace.SetActive(false);
		}
		if(name == "skinMenu")            //Changed By AYUSHARMA
		{
			skinMenu.SetActive(true);
			CoinsPlace.SetActive(true);
		}
			if(name == "settingsMenu")            //Changed By AYUSHARMA
		{
			SettingsMenu.SetActive(true);
			CoinsPlace.SetActive(false);
		}



	}

	void ResetAll()
	{
		GameMenu.SetActive(false);
		MainMenu.SetActive(false);
		LevelMenu.SetActive(false);
		GameOverMenu.SetActive(false);
		RewardsMenu.SetActive(false);
		skinMenu.SetActive(false);
		SettingsMenu.SetActive(false);
		CoinsPlace.SetActive(true);
	}
	
}