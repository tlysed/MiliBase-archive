using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    [Header("Time")]
    public float waitTime;
    private float realWaitTime = -5;

    [Header("Statistic")]

    public float minPoints;
    public static float Points = 0;
    public static int Kills = 0;
    public static int Levels = 1;

    [Header("UI")]
    public TextMeshProUGUI playerScore;

    private void Update()
    {

        if (Points >= minPoints)
        {
            minPoints += minPoints/2;
            Points = 0;
            PlayerStatistics.Levels++;
            realWaitTime = Time.time;
        }
        if (Time.time - realWaitTime < waitTime)
        {
            playerScore.text = "NEW LEVEL";
            playerScore.color = Color.red;
        }
        else
        {
            playerScore.text = "Points:" + Points.ToString() + "/" + minPoints.ToString();
            playerScore.color = Color.white;
        }
    }
    public void TakePoints(int points)
    {
        Points += points;
    }
}
