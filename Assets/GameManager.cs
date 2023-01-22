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
	public int CurrMat;	
	public int levelUnlocked;
	public int coins;
	public int power;
	public TMP_Text coinText;
	public TMP_Text LevelText;
	public bool canVibrate;
	public int currentSkinNumber = 1;  //Variable Created By Ayusharma

	public GameObject PurchaseBox;

	
	
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
		LevelText.text = " ";
		levelUnlocked =1;
		canVibrate=true;
		power=2;
        MainMenuCamera.SetActive(true);
		GameCamera.SetActive(false);
		moveScript.GetComponent<PlayerMovement>().enabled=false;
		MenuManager.Instance.ChangeMenu("skinMenu");
		MenuManager.Instance.ChangeMenu("mainmenu");
		PostStart();
    }
	
	void PostStart()
	{
		levelUnlocked=PlayerPrefs.GetInt("Level",1);
		coins=PlayerPrefs.GetInt("Coins",0);
		power=PlayerPrefs.GetInt("Power",2);
		coinText.text=coins.ToString();
		currentSkinNumber=PlayerPrefs.GetInt("SkinCurr",0);
		
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
		LevelText.text = "LEVEL " + data;
	}

	

	public void unlockSkin(int skinNumber)
    {
		PlayerPrefs.SetInt("Skin"+skinNumber,1);
		MenuManager.Instance.ChangeMenu("skinMenu");
    }

	public void ChangeSkin(int skinNumber,Texture tex)
    {
		playerSkin.material.mainTexture = (tex);
		currentSkinNumber=skinNumber;
		PlayerPrefs.SetInt("CurrMat",skinNumber);
    }
	

	

	public void Purchase(int Skin,int Costing)
	{
		PurchaseBox.SetActive(true);
		PurchaseBox.GetComponent<PurchaseManager>().Purchase(Costing,Skin);
		
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

	public void NextLevel()
	{
		OnStartStage(levelUnlocked);
	}
	
		
	
}
