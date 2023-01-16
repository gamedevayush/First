using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenuCamera;
    public GameObject GameCamera;
	public GameObject moveScript;
	public GameObject Ground;
	public GameObject GroundMesh;
	public SkinnedMeshRenderer playerSkin; //Variable Created By Ayusharma for Changing material on Skinned Mesh Renderer

	public int CurrLevel;
	public int levelUnlocked;
	public int coins;
	public int power;
	public TMP_Text coinText;
	public bool canVibrate;
	public int currentSkinNumber = 1;  //Variable Created By Ayusharma
	public string allSkinState = "0000000000";

	
	
	private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    void Start()
    {
		levelUnlocked=1;
		canVibrate=true;
		power=2;
        MainMenuCamera.SetActive(true);
		GameCamera.SetActive(false);
		moveScript.GetComponent<PlayerMovement>().enabled=false;
		MenuManager.Instance.ChangeMenu("mainmenu");
		PostStart();
    }
	
	void PostStart()
	{
		levelUnlocked=PlayerPrefs.GetInt("Level",1);
		coins=PlayerPrefs.GetInt("Coins",0);
		power=PlayerPrefs.GetInt("Power",1);
		coinText.text=coins.ToString();
	}
	
	void SaveLevel(int data)
	{
		PlayerPrefs.SetInt("Level",data);
		PostStart();
	}
	void SaveCoin(int data)
	{
		PlayerPrefs.SetInt("Coins",data);
		PostStart();
	}
	
	void SavePower(int data)
	{
		PlayerPrefs.SetInt("Power",data);
		PostStart();
	}
	
	public void VibrationMode()
	{
		if(canVibrate)
		{
			canVibrate=false;
		}
		else
		{
			canVibrate=true;
		}
		
	
	}
	public void SoundMode()
	{
	if(AudioListener.volume == 0)
	{
		AudioListener.volume=1;
	}
	else
	{
		AudioListener.volume=0;
	}

	}
    public void OnStartStage(int data)
	{
		CurrLevel=data;
		MenuManager.Instance.ChangeMenu("gamemenu");
		MainMenuCamera.SetActive(false);
		GameCamera.SetActive(true);
		moveScript.GetComponent<PlayerMovement>().enabled=true;
		
	}

	//Below three functions are Created By  Ayusharma
	public void OnSaveSkin(int skinNumber,Texture tex)
    {
		currentSkinNumber = skinNumber;
		unlockSkin(skinNumber);
		ChangeSkin(skinNumber,tex);
    }

	public void unlockSkin(int skinNumber)
    {
		string temp = "";
		int i = 0;
		for(i =0;i<allSkinState.Length;i++)
        {
			if(skinNumber == i+1)
            {
				temp = temp + "1";
            }
			else
            {
				temp = temp +allSkinState[i];
            }
        }

		allSkinState = temp;
    }

	public void ChangeSkin(int skinNumber,Texture tex)
    {
		playerSkin.material.mainTexture = (tex);
    }

	

	
	
	public void OnEndStage()
	{
	moveScript.GetComponent<PlayerMovement>().enabled=false;
	moveScript.GetComponent<PlayerMovement>().enabled=true;
	 foreach(Transform child in Ground.transform)
	 {
		 Destroy(child.gameObject);
	 }
	 moveScript.GetComponent<PlayerMovement>().enabled=false;
	 moveScript.transform.Find("Char").gameObject.SetActive(true);
	 GameObject g=Instantiate(GroundMesh);
	 g.name="GroundMesh";
	 g.transform.parent=Ground.transform;
	MainMenuCamera.SetActive(true);
		GameCamera.SetActive(false);
	}
	
	public void IncreaseCoin(int pointIn)
	{
		coins=coins+pointIn;
		SaveCoin(coins);
	}
	
	public void DecreaseCoin(int pointIn)
	{
		coins=coins-pointIn;
		SaveCoin(coins);
	}
	public void IncreaseLevel()
	{
		if(levelUnlocked==CurrLevel)
		{
		SaveLevel(levelUnlocked+1);
		}
		
	}
	
		
	
}
