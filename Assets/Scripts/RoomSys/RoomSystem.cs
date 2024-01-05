using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RoomSystem : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public List<GameObject> objectToSpawn;

    public List<GameObject> SpawnObjectPoints;

     public bool playerInRoom = false;
     public List<GameObject> enemyInRoom;
    private void Start()
    {
        for(int i = 0;i < SpawnObjectPoints.Count; i++)
        {
            float randCount = Random.Range(0,11);
            if(randCount < 4.5f ) { Destroy(SpawnObjectPoints[i]); SpawnObjectPoints.RemoveAt(i); }
        }
        for (int i = 0; i < SpawnObjectPoints.Count; i++)
        {
            float randCount = Random.Range(0, 11);
            if (randCount < 4.5f)
            {
                int rand = Random.Range(0, objectToSpawn.Count - 1);
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
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("RoomSpawner").GetComponent<spawnerRooms>().spawned && playerInRoom)
        {
            for (int i = 0; i < enemyInRoom.Count; i++)
            {
                enemyInRoom[i].SetActive(true);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRoom = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRoom = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerInRoom = false;
    }
}
