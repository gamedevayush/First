using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinObject : MonoBehaviour
{
    public int SkinNumb;
	public int Costing; 
	public TMP_Text Label;
	public bool Unlocked;
	public string Status;
	public Image Overlay;
	public Sprite LockS;
	public Sprite DoneS;
    void OnEnable()
    {
		Unlocked=false;
        SetSkins();
		Label.text=Costing.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void SetSkins()
	{
		/*if(LevelNumb<=GameManager.Instance.levelUnlocked)
		{
			Unlocked=true;
			if(LevelNumb==GameManager.Instance.levelUnlocked)
			{
				Overlay.GetComponent<Image>().enabled=false;
			}
			else
			{
				Overlay.sprite=DoneS;
			}
		}
		else
		{
			Overlay.sprite=LockS;
		}
		Debug.Log(GameManager.Instance.levelUnlocked);
		*/
	}
	
	public void OnClick()
	{
		if(Unlocked==true)
		{
			Debug.Log("PLAY");
			GameManager.Instance.OnStartStage(this.SkinNumb);
		}
		else
		{
			Debug.Log("LOCKED");
		}
	}
	
	}
