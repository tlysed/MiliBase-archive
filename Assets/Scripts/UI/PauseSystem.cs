using EmeraldPowder.CameraScaler;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider mobileScaleSlider;
    [SerializeField] private TextMeshProUGUI volumePercent;
    [SerializeField] private TextMeshProUGUI mobileScalePercent;

    private static bool postProcessing = true;
    [SerializeField] private Toggle postProcessingToggle;

    private new Camera camera;
    private void Awake()
    {
        camera = Camera.main;
        camera.GetComponent<PostProcessVolume>().enabled = postProcessing;
        postProcessingToggle.isOn = postProcessing;

        qualityDropdown.value = QualitySettings.GetQualityLevel();
        volumePercent.text = (100 * volumeSlider.value).ToString("F0") + "%";
        mobileScalePercent.text = (100 * mobileScaleSlider.value).ToString("F0") + "%";
    }
    public void ChangePostProccesing(bool var)
    {
        camera.GetComponent<PostProcessVolume>().enabled = var;
        postProcessing = var;
    }
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
    public void ChangeVolume()
    {
        volumePercent.text = (100 * volumeSlider.value).ToString("F0") + "%";
    }
    public void ChangeMobileScale()
    {
        mobileScalePercent.text = (100 * mobileScaleSlider.value).ToString("F0") + "%";
        camera.GetComponent<CameraScaler>().MatchWidthOrHeight = mobileScaleSlider.value;

    }
}
