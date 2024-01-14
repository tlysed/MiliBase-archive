using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider mobileScaleSlider;
    [SerializeField] private TextMeshProUGUI volumePercent;
    [SerializeField] private TextMeshProUGUI mobileScalePercent;
    public void UnPauseGame()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }
    public void Exit(int level)
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("LoaderCanvas").GetComponent<loaderSystem>().UnLoadingLevel(level);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    private void Update()
    {
        volumePercent.text = (100 * volumeSlider.value).ToString("F0") + "%";
        mobileScalePercent.text = (100 * mobileScaleSlider.value).ToString("F0") + "%";
    }
    private void Awake()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }
}
