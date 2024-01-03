using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;


public class spawnerRooms : MonoBehaviour
{
    public List<GameObject> startSpawnPoints;

    public List<Rooms> topRoom;
    public List<Rooms> downRoom;
    public List<Rooms> leftRoom;
    public List<Rooms> rightRoom;
    [System.Serializable]
    public class Rooms
    {
        public List<GameObject> rooms;
    }

    [HideInInspector] public List<GameObject> finalRooms;
    [HideInInspector] public int finalRoomsAmount;

    public int minRooms;
    public int maxRooms;

    [HideInInspector] public int nowRoom = 1;
    [HideInInspector] public int amountRooms;

    public GameObject Portal;

    [HideInInspector] public bool spawned = false;
    private void Awake()
    {
        amountRooms = Random.Range(minRooms, maxRooms);
        while (startSpawnPoints.Count != 1)
        {
            int rand = Random.Range(0, startSpawnPoints.Count);
            Destroy(startSpawnPoints[rand]);
            startSpawnPoints.RemoveAt(rand);
        }
    }
    private void Update()
    {
        if (finalRoomsAmount - amountRooms >= 2 && !spawned)
        {
            Instantiate(Portal, finalRooms[0].transform);
            Camera.main.transform.position = new Vector3(finalRooms[1].transform.position.x, finalRooms[1].transform.position.y, Camera.main.transform.position.z);
            GameObject.FindGameObjectWithTag("Player").transform.position = finalRooms[1].transform.position;
            spawned = true;
        }
        else if (finalRoomsAmount - amountRooms == 1 && !spawned)
        {
            Instantiate(Portal, finalRooms[0].transform);
            spawned = true;
        }
    }
}
