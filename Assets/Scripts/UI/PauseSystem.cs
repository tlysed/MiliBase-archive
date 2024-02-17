using EmeraldPowder.CameraScaler;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject musicPrefab;
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [SerializeField] private Slider musicSlider;//musicVol
    [SerializeField] private Slider sfxSlider;//sfxVol
    [SerializeField] private Slider ambienceSlider;//ambienceVol

    [SerializeField] private Slider mobileScaleSlider;

    [SerializeField] private TextMeshProUGUI musicPercent;
    [SerializeField] private TextMeshProUGUI sfxPercent;
    [SerializeField] private TextMeshProUGUI ambiencePercent;
    [SerializeField] private TextMeshProUGUI mobileScalePercent;

    private static bool postProcessing = true;
    private static float sfxVolume = 100;
    private static float musicVolume = 100;
    private static float ambienceVolume = 100;
    private static float mobileScaleFloat;
    [SerializeField] private Toggle postProcessingToggle;

    private new Camera camera;
    private GameObject Player;
    private void Awake()
    {
        camera = Camera.main;
        camera.GetComponent<PostProcessVolume>().enabled = postProcessing;
        postProcessingToggle.isOn = postProcessing;

        qualityDropdown.value = QualitySettings.GetQualityLevel();

        musicPercent.text = (musicVolume).ToString("F0") + "%";
        musicSlider.value = musicVolume / 100;

        sfxPercent.text = (sfxVolume).ToString("F0") + "%";
        sfxSlider.value = sfxVolume / 100;

        ambiencePercent.text = (ambienceVolume).ToString("F0") + "%";
        ambienceSlider.value = ambienceVolume / 100;

        mobileScalePercent.text = (100 * mobileScaleSlider.value).ToString("F0") + "%";

        if (GameObject.FindGameObjectWithTag("MusicSource") == null)
        {
            DontDestroyOnLoad(Instantiate(musicPrefab));
        }
        else
        {
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MusicSource"));
        }

        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void ChangePostProccesing(bool var)
    {
        camera.GetComponent<PostProcessVolume>().enabled = var;
        postProcessing = var;
    }
    public void UnPauseGame()
    {
        panel.SetActive(false);
        if (Player != null) Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }

    public void PauseGame()
    {
        panel.SetActive(true);
        if(Player != null) Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
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

    public void ChangeSFXVolume()
    {
        sfxVolume = 100 * sfxSlider.value;
        sfxPercent.text = (sfxVolume).ToString("F0") + "%";

        if(sfxSlider.value == 0)
        {
            mixer.SetFloat("sfxVol", -80);
        }
        else
        {
            mixer.SetFloat("sfxVol", Mathf.Lerp(-40, 0, sfxSlider.value));
        }
    }
    public void ChangeMusicVolume()
    {
        musicVolume = 100 * musicSlider.value;
        musicPercent.text = (musicVolume).ToString("F0") + "%";

        if (musicSlider.value == 0)
        {
            mixer.SetFloat("musicVol", -80);
        }
        else
        {
            mixer.SetFloat("musicVol", Mathf.Lerp(-40, 0, musicSlider.value));
        }
    }
    public void ChangeAmbienceVolume()
    {
        ambienceVolume = 100 * ambienceSlider.value;
        ambiencePercent.text = (ambienceVolume).ToString("F0") + "%";

        if (ambienceSlider.value == 0)
        {
            mixer.SetFloat("ambienceVol", -80);
        }
        else
        {
            mixer.SetFloat("ambienceVol", Mathf.Lerp(-40, 0, ambienceSlider.value));
        }
    }

    public void ChangeMobileScale()
    {
        mobileScaleFloat = mobileScaleSlider.value;
        mobileScalePercent.text = (100 * (mobileScaleFloat)).ToString("F0") + "%";
        camera.GetComponent<CameraScaler>().MatchWidthOrHeight = mobileScaleFloat;
    }
}
