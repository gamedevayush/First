using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scorer : MonoBehaviour
{
	public TMP_Text finalScore;
    void OnEnable()
    {
        finalScore.text="??";
    }
	
	public void updateScore(int numb)
	{
		
		StartCoroutine(changeValueOverTime(0, numb, 2f));
	}
	
	IEnumerator changeValueOverTime(float fromVal, float toVal, float duration)
{
    float counter = 0f;

    while (counter < duration)
    {
        if (Time.timeScale == 0)
            counter += Time.unscaledDeltaTime;
        else
            counter += Time.deltaTime;

        float val = Mathf.Lerp(fromVal, toVal, counter / duration);
        Debug.Log("Val: " + val);
		finalScore.text=((int)val).ToString();
        yield return null;
    }
}

}
