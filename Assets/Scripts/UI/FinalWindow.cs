using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class FinalWindow : MonoBehaviour
{
    [SerializeField] private GameObject stats;
    private static GameObject staticStats;
    private static int levelToLoad;
    private void Start()
    {
        staticStats = stats;
    }
    public void LoadLevel()
    {
        GameObject.FindGameObjectWithTag("LoaderCanvas").GetComponent<loaderSystem>().UnLoadingLevel(levelToLoad);
    }

    public static void highwayWindow(int _int)
    {
        levelToLoad = _int;
        staticStats.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "��������: " + CarInfo.distance.ToString();
        staticStats.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "������� �������: " + CarInfo.detailsTaken.ToString();
    }
    public static void turnBasedWindow(int _int)
    {
        levelToLoad = _int;
        staticStats.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "���������: " + PlayerStatistics.Kills.ToString();
        staticStats.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "������� �����: " + PlayerStatistics.TargerPoints.ToString();
        staticStats.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "��������: " + PlayerStatistics.Distance.ToString();
    }

}
