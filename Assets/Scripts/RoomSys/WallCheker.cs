using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCheker : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private GameObject wall;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Instantiate(wall, spawnPoint[0].position, Quaternion.identity);
            Instantiate(wall, spawnPoint[1].position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
