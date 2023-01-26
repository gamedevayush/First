using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RewardSystem : MonoBehaviour
{
    public GameObject notificationdot;
    public GameObject NoMoreRewards;
    public GameObject Rewards;

    public TMP_Text newCoinsGetText;
    public GameObject newCoinsGetBox;
    bool rewardAvailable = false;
    bool isNewDate;
    public string url = "www.google.com";
    public string timeSite = "https://www.gamedevayush.in/indux.php";
    public string sDate = "";
    public string tDate = "";

    void OnEnable()
    {
        RewardNotAvailable();
        StartCoroutine(CheckInternet());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void RewardAvailable()
    {
        notificationdot.SetActive(true);
        NoMoreRewards.SetActive(false);
        Rewards.SetActive(true);
        isNewDate = true;
    }

    void RewardNotAvailable()
    {
        notificationdot.SetActive(false);
        NoMoreRewards.SetActive(true);
        Rewards.SetActive(false);
        isNewDate = false;
    }

    public void OnClickReward()
    {
        GameManager.Instance.IncreaseCoin(500);
        newCoinsGetBox.SetActive(true);
        newCoinsGetText.text = "+500";
        NoMoreRewards.SetActive(true);
        Rewards.SetActive(false);
        notificationdot.SetActive(false);
        StartCoroutine(GetDate());
    }


    private IEnumerator CheckInternet()
    {
        WWW www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("Not Connect Internet");
            RewardNotAvailable();
        }
        else
        {
            Debug.Log("Success Internet");
            StartCoroutine(CheckDate());
        }



    }
    private IEnumerator CheckDate()
    {
        WWW www = new WWW("https://www.gamedevayush.in/indux.php");
        yield return www;
        string[] splitDate = www.text.Split(new string[] { "Date" }, StringSplitOptions.None);
        sDate = splitDate[1].Substring(0, 12);
        Debug.Log("New" + sDate + " Old " + PlayerPrefs.GetString("LastDate"));
        CompareDate(sDate);
    }

    private void CompareDate(string date)
    {
        string SavedDate=PlayerPrefs.GetString("LastDate");
        if (SavedDate!=date)
        {
            Debug.Log("Available");
            isNewDate = true;
            RewardAvailable();
        }
    }






    private IEnumerator GetDate()
    {
        WWW www = new WWW(timeSite);
        yield return www;

        string[] splitDate = www.text.Split(new string[] { "Date" }, StringSplitOptions.None);
        tDate = splitDate[1].Substring(0, 12);

        SaveDate(tDate);
    }

    private void SaveDate(string date)
    {
        PlayerPrefs.SetString("LastDate", date);
     
       
    }
}
