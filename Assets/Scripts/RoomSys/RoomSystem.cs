using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystem : MonoBehaviour
{
    [SerializeField] private float difficulty = 3.5f;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private List<GameObject> objectToSpawn;

    [SerializeField] private List<GameObject> SpawnObjectPoints;
    [SerializeField] public List<GameObject> metalDoors;

    [HideInInspector] public bool playerInRoom = false;
    [HideInInspector] public List<GameObject> enemyInRoom;
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("RoomSpawner") != null)
        {
            if (GameObject.FindGameObjectWithTag("RoomSpawner").GetComponent<spawnerRooms>().spawned && playerInRoom)
            {
                for (int i = 0; i < enemyInRoom.Count; i++)
                {
                    enemyInRoom[i].SetActive(true);
                }
                if (enemyInRoom.Count == 0)
                {
                    for (int i = 0; i < metalDoors.Count; i++)
                    {
                        Destroy(metalDoors[i]);
                        metalDoors.RemoveAt(i);
                    }
                }
            }
        }
        if(!playerInRoom)
        {
            for (int i = 0; i < enemyInRoom.Count; i++)
            {
                enemyInRoom[i].SetActive(false);
            }
        }
    }
    public void SpawnEntity()
    {
        for (int i = PlayerStatistics.Levels; i < SpawnObjectPoints.Count; i++)
        {
            Destroy(SpawnObjectPoints[i]); SpawnObjectPoints.RemoveAt(i);
        }
        for (int i = 0; i < SpawnObjectPoints.Count; i++)
        {
            float randCount = Random.Range(0, 11);
            if (randCount > difficulty) { Destroy(SpawnObjectPoints[i]); SpawnObjectPoints.RemoveAt(i); }
        }
        for (int i = 0; i < SpawnObjectPoints.Count; i++)
        {
            float randCount = Random.Range(0, 11);
            if (randCount > difficulty)
            {
                int rand = Random.Range(0, objectToSpawn.Count);
                var obj = Instantiate(objectToSpawn[rand], SpawnObjectPoints[i].transform.position, Quaternion.identity);
                obj.transform.parent = SpawnObjectPoints[i].transform;
            }
            else
            {
                var enemy = Instantiate(enemyToSpawn, SpawnObjectPoints[i].transform.position, Quaternion.identity);
                enemy.transform.parent = SpawnObjectPoints[i].transform;
                enemyInRoom.Add(enemy);
                enemy.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRoom = true;
        if (collision.CompareTag("MetalDoor")) metalDoors.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRoom = false;
    }
}
