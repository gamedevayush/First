using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RewardSystem : MonoBehaviour
{
    public GameObject notificationdot;
    public GameObject NoMoreRewards;
    public GameObject Rewards;

    bool rewardAvailable = false;

    public string url = "www.google.com";
    public string urlDate = "http://worldclockapi.com/api/json/est/now";
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
    }

    void RewardNotAvailable()
    {
        notificationdot.SetActive(false);
        NoMoreRewards.SetActive(true);
        Rewards.SetActive(false);
    }

    public void OnClickReward()
    {
        GameManager.Instance.IncreaseCoin(400);
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
        WWW www = new WWW(urlDate);
        yield return www;

        string[] splitDate = www.text.Split(new string[] { "currentDateTime\":\"" }, StringSplitOptions.None);
        sDate = splitDate[1].Substring(0, 10);

        CompareDate(sDate);
    }

    private void CompareDate(string date)
    {
        string SavedDate=PlayerPrefs.GetString("LastDate");
        if (SavedDate!=date)
        {
            Debug.Log("Available");
            RewardAvailable();
        }
    }






    private IEnumerator GetDate()
    {
        WWW www = new WWW(urlDate);
        yield return www;

        string[] splitDate = www.text.Split(new string[] { "currentDateTime\":\"" }, StringSplitOptions.None);
        tDate = splitDate[1].Substring(0, 10);

        SaveDate(tDate);
    }

    private void SaveDate(string date)
    {
        PlayerPrefs.SetString("LastDate", date);
       
    }
}
