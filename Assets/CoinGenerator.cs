using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    void Start()
    {
        float Feets=0;
        float Posz;
        int NoofCoins=0;
        for (int i = 0; i < 15; i++)
        {
            Posz = 0;
            int rr= Random.Range(0, 2);
            if (rr == 0)
            {
                Posz = -0.38f;
            }
            else if (rr == 1)
            {
                Posz = 0;
            }
            else if (rr == 2)
            {
                Posz = 0.38f;
            }
            for (int j = 0; j < 3; j++)
            {
                if (NoofCoins < 38)
                {
                    if (NoofCoins == 37)
                    {
                        int g = Random.Range(0, 2);
                        if (g == 0)
                        {
                            if (PlayerMovement.Instance.isMagnetCoins == false)
                            {
                                GameObject Power = Instantiate(Resources.Load("Magnet") as GameObject, new Vector3(this.transform.position.x + Feets, 0.07f, Posz), Quaternion.Euler(0, 180f, 90f));
                                Power.transform.parent = this.transform;
                            }
                        }
                        else if (g == 1)
                        {
                            if (PlayerMovement.Instance.isDoublingCoins == false)
                            {
                                GameObject Power = Instantiate(Resources.Load("2xPower") as GameObject, new Vector3(this.transform.position.x + Feets, 0.07f, Posz), Quaternion.Euler(0, -900, 0));
                                Power.transform.localScale = new Vector3(2, 60, 1);
                                Power.transform.parent = this.transform;
                            }
                        }

                    }
                    else {
                        GameObject Coin = Instantiate(Resources.Load("Coin") as GameObject, new Vector3(this.transform.position.x + Feets, 0.07f, Posz), Quaternion.Euler(0, 0, 90f));
                        Coin.transform.parent = this.transform;
                        Coin.name = NoofCoins.ToString();
                    }
                    
                    Feets = Feets + 0.5f;
                    NoofCoins++;
                }
            }
        }
    }

 
}

