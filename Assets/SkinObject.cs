using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinObject : MonoBehaviour
{
    public int SkinNumb;
	public Texture skinTexture;
	public int Costing; 
	public TMP_Text Label;
	public bool Unlocked;
	public string Status;
	public Image Overlay;
	public Image MatImage;
	public Sprite MatSprite;
	public Sprite LockS;
	public Sprite DoneS;
	public bool isActive;
    void OnEnable()
    {
		Unlocked=false;
		Label.color = Color.white;
		Label.text=Costing.ToString();
		if((PlayerPrefs.GetInt("CurrMat",0))==this.SkinNumb)
		{
			GameManager.Instance.ChangeSkin(SkinNumb,skinTexture);
			this.isActive=true;
		}
		else
		{
			this.isActive=false;
		}
		SetSkins();

	
	MatImage.sprite=MatSprite;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void SetSkins()
	{
		if(PlayerPrefs.GetInt("Skin"+SkinNumb,0)==1)
		{
			Unlocked=true;
			Label.text="";
			if(isActive)
			{
				Overlay.sprite=DoneS;
				Label.text = "Active";
					Label.color = Color.green;
				Overlay.GetComponent<Image>().enabled=true;
				GameManager.Instance.currentSkinNumber=SkinNumb;
			}
			else
			{
				Label.text = "Owned";
				Label.color = Color.yellow;
				Overlay.GetComponent<Image>().enabled=false;
			}
		}
		else
		{
			Overlay.sprite=LockS;
		}
		
	}
	
	public void OnClick()
	{
		if(Unlocked==true)
		{
			Debug.Log("Active");
			
			GameManager.Instance.ChangeSkin(SkinNumb,skinTexture);
			MenuManager.Instance.ChangeMenu("mainMenu");
			MenuManager.Instance.ChangeMenu("skinMenu");
		}
		else
		{
			Debug.Log("LOCKED");
			if(GameManager.Instance.coins >= Costing)
            {
				Debug.Log("You Can Purchase this Skin");
				GameManager.Instance.Purchase(SkinNumb,Costing);
            }
			else
            {
				Debug.Log("You Dont have Enough Coins");
            }
		}
	}

	public void AssignCosting()
    {
		Label.text = Costing.ToString();
    }

	
}
