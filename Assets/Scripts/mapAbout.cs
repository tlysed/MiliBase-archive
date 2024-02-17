using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mapAbout : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI placeHolder;
    [SerializeField] private TextMeshProUGUI needHolder;
    private int levelToLoad;
    private GameObject loaderCanvas;
    public GameObject curentMapAbout;
    private void Start()
    {
        loaderCanvas = GameObject.FindGameObjectWithTag("LoaderCanvas");

        StartCoroutine(changeNeeded());
    }
    public void ChangePlaceHolder(GameObject @object)
    {
        curentMapAbout = @object;
        placeHolder.text = curentMapAbout.GetComponent<stringAboutMap>().TextAbout;
        levelToLoad = curentMapAbout.GetComponent<stringAboutMap>().LoadInt;
        if (curentMapAbout.GetComponent<stringAboutMap>().need)
        {
            needHolder.text = curentMapAbout.GetComponent<stringAboutMap>().NeedAbout;
        }
        else needHolder.text = "";
    }
    public void LoadLevel()
    {
        if (curentMapAbout.GetComponent<stringAboutMap>().need)
        {
            if(PlayerStatistics.Points >= curentMapAbout.GetComponent<stringAboutMap>().needPoints)
            {
                loaderCanvas.GetComponent<loaderSystem>().UnLoadingLevel(levelToLoad);
            }
        }
        else
        {
            loaderCanvas.GetComponent<loaderSystem>().UnLoadingLevel(levelToLoad);
        }
    }
    IEnumerator changeNeeded()
    {
        if (PlayerStatistics.Points >= curentMapAbout.GetComponent<stringAboutMap>().needPoints)
        {
            curentMapAbout.GetComponent<stringAboutMap>().need = false;
            yield return null;
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(changeNeeded());
    }
}
