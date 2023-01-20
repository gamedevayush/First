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
	public GameObject StoreMenu;
	public GameObject GameOverMenu;
	public GameObject skinMenu;           //Changed By AYUSHARMA

	public TMP_Text Finisher;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
	
	public void ChangeMenu(string name)
	{
		ResetAll();
		if(name=="gamemenu")
		{
			GameMenu.SetActive(true);
		}
		if(name=="gameover")
		{
			GameOverMenu.SetActive(true);
			Finisher.text="Game Over";
			
		}
		if(name=="finish")
		{
			GameOverMenu.SetActive(true);
			Finisher.text="Congratulations!";
		}
		if(name=="mainmenu")
		{
			MainMenu.SetActive(true);
		}
		if(name=="levelmenu")
		{
			LevelMenu.SetActive(true);
		}
		if(name == "skinMenu")            //Changed By AYUSHARMA
		{
			skinMenu.SetActive(true);
        }
		if(name == "storeMenu")            //Changed By AYUSHARMA
		{
			StoreMenu.SetActive(true);
        }
		
		
		
	}
	
	void ResetAll()
	{
		GameMenu.SetActive(false);
		MainMenu.SetActive(false);
		LevelMenu.SetActive(false);
		GameOverMenu.SetActive(false);
		//FinishMenu.SetActive(false);
		skinMenu.SetActive(false);  
		StoreMenu.SetActive(false); 						//Changed By AYUSHARMA
	}
	
}