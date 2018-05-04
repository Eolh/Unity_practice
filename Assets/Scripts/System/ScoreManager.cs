using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // add this code when use UI 

public class ScoreManager : MonoBehaviour
{

    public int scorePoint = 0;

    private Text scoreTx;

    void Awake()
    {
        scoreTx = GameObject.Find("NormalUI").transform.FindChild("scoreTx").GetComponent<Text>();
        scoreTx.text = "SCORE : 0";
        StartCoroutine("PlusScoreRoutine");
    }

    public void PlusScore(int plusPoint)
    {
        scorePoint += plusPoint; // increase score

        if (plusIng)
        {
            StopCoroutine("PlusValue");
        }
        StartCoroutine("PlusValue");
        // scoreTx.text = "SCORE : " + scorePoint.ToString("N0"); // change text 
    }

    IEnumerator PlusScoreRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // give score point per sec
            PlusScore(100);
        }
    }

    bool plusIng = false;
    float tempPer;
    float pastScorePoint;
    IEnumerator PlusValue()
    {
        plusIng = true;
        tempPer = 0;
        while (tempPer < 1f)
        {
            tempPer += 0.1f;
            if (pastScorePoint < scorePoint)
            {
                pastScorePoint = (int)Mathf.Lerp(pastScorePoint, scorePoint, tempPer);

                scoreTx.text = "SCORE : " + pastScorePoint.ToString("N0"); // change text
            }
            yield return new WaitForSeconds(0.033f);
        }
        pastScorePoint = scorePoint;
        scoreTx.text = "SCORE : " + scorePoint.ToString("N0"); // text change to final score

        plusIng = false;
    }
}
