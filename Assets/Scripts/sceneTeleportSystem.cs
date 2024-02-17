using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneTeleportSystem : MonoBehaviour
{
    [Min(-1)] [SerializeField] public int levelToLoad;
    [SerializeField] private GameObject endWindow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatistics>().TakePoints(50);
            if (levelToLoad != -1)
            {
                collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                Instantiate(endWindow, Camera.main.transform);
                FinalWindow.turnBasedWindow(levelToLoad);
            }
        }
    }
}
