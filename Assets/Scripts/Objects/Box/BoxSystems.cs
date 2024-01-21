using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSystems : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private List<GameObject> spawnObjects;
    [SerializeField] private Transform spawnPoint;

    void Update()
    {
        if (health <= 0)
        {
            int rand = Random.Range(0, spawnObjects.Count);
            Instantiate(spawnObjects[rand], spawnPoint.position, spawnPoint.rotation);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
