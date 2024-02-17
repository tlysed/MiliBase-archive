using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loaderSystem : MonoBehaviour
{
    private Animator animator;
    private int levelToLoad;
    private bool levelLoaded = false;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        loadingScreen.SetActive(true);
    }
    private void Update()
    {
        if (!levelLoaded)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (GameObject.FindGameObjectWithTag("RoomSpawner").GetComponent<spawnerRooms>().spawned)
                {
                    loadingScreen.SetActive(false);
                    animator.SetTrigger("loading");
                    levelLoaded = true;
                }
            }
            else
            {
                loadingScreen.SetActive(false);
                animator.SetTrigger("loading");
                levelLoaded = true;
            }
        }
    }
    public void UnLoadingLevel(int level)
    {
        levelToLoad = level;
        animator.SetTrigger("unloading");
    }
    public void UnLoadingComplete() 
    {
        SceneManager.LoadScene(levelToLoad);
        StartCoroutine(levelTransition());
    }
    IEnumerator levelTransition()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
