using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneTeleportSystem : MonoBehaviour
{
    [Min(-1)] public int levelToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatistics>().TakePoints(50);
            if (levelToLoad != -1)
            {
                collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                GameObject.FindGameObjectWithTag("LoaderCanvas").GetComponent<loaderSystem>().UnLoadingLevel(levelToLoad);
            }
        }
    }
}
