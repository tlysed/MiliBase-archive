using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneTeleportSystem : MonoBehaviour
{
    [Min(0)] public int levelToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) GameObject.FindGameObjectWithTag("LoaderCanvas").GetComponent<loaderSystem>().UnLoadingLevel(levelToLoad);
    }
}
