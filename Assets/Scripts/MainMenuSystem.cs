using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using YG.Utils.LB;

public class MainMenuSystem : MonoBehaviour
{
    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            LoadSave();
        }
    }
    public void ResetSave()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
    public void LoadSave()
    {
        PlayerInfo.isMobile = YandexGame.EnvironmentData.isMobile;
        PlayerStatistics.Points = YandexGame.savesData.allPoints;
        LBPlayerData lb = new LBPlayerData();
        if(lb.score < PlayerStatistics.Points)
        {
            YandexGame.NewLeaderboardScores("maxPoints", (int)PlayerStatistics.Points);
        }
        if(YandexGame.savesData.maxDistance > CarInfo.maxDistance) CarInfo.maxDistance = YandexGame.savesData.maxDistance;
        if (YandexGame.savesData.carSpeed > CarInfo.carSpeed) CarInfo.carSpeed = YandexGame.savesData.carSpeed;
        if (YandexGame.savesData.armorThickness > CarInfo.armorThickness) CarInfo.armorThickness = YandexGame.savesData.armorThickness;
        CarInfo.details = YandexGame.savesData.details;
    }
    private void OnEnable() => YandexGame.GetDataEvent += LoadSave;

    private void OnDisable() => YandexGame.GetDataEvent -= LoadSave;
}
