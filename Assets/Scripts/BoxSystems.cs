using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSystems : MonoBehaviour
{
    public int health;
    public GameObject spawnObj;
    public Transform spawnPoint;

    void Update()
    {
        if (health <= 0)
        {
            Instantiate(spawnObj, spawnPoint.position, spawnPoint.rotation);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
