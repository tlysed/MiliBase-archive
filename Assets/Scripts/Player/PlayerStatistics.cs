using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float waitTime;
    private float realWaitTime = -5;

    [Header("Statistic")]

    public static float MaxPoints = 100;
    public static float Points = 0;
    public static int Kills = 0;
    public static int Levels = 1;
    public static int Races = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerScore;

    private void Update()
    {

        if (Points >= MaxPoints)
        {
            MaxPoints += 50 * Levels;
            Points = 0;
            Levels++;
            realWaitTime = Time.time;
        }
        if (Time.time - realWaitTime < waitTime)
        {
            playerScore.text = "NEW LEVEL";
            playerScore.color = Color.red;
        }
        else
        {
            playerScore.text = "Points:" + Points.ToString() + "/" + MaxPoints.ToString();
            playerScore.color = Color.white;
        }
    }
    public void TakePoints(int points)
    {
        Points += points;
    }
}
