using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseManager : MonoBehaviour
{
    int Coins;
	public TMP_Text CostText;
	int skinNumb; 
	int cost;
    public void Purchase(int Cost,int no)
	{
		CostText.text=Cost.ToString();
		skinNumb=no;
		cost=Cost;
	}
	
	public void Yes()
	{
		GameManager.Instance.DecreaseCoin(cost);
		GameManager.Instance.unlockSkin(skinNumb);
		ResetValues();
		AdmobController.Instance.ShowInterstitialAd();
		
	}
	public void ResetValues()
	{
		skinNumb=0;
		cost=0;
	}
}
