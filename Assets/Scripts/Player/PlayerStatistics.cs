using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerStatistics : MonoBehaviour
{
    [Header("Statistic")]

    public static int Kills = 0;
    public static int TargerPoints = 0;
    public static int Distance = 0;

    public static float Points = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI playerScore;

    private void Start()
    {
        Kills = 0;
        TargerPoints = 0;
        Distance = 0;
    }
    private void Update()
    {
        playerScore.text = "Очки:" + Points.ToString();
        playerScore.color = Color.white;
    }
    public void TakePoints(int points)
    {
        Points += points;
        TargerPoints += points;
        if (YandexGame.SDKEnabled)
        {
            YandexGame.savesData.allPoints = Points;
            YandexGame.SaveProgress();
        }
   }
}

