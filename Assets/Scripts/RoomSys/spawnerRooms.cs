using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class spawnerRooms : MonoBehaviour
{
    [SerializeField] private GameObject playerRoom;
    [SerializeField] private List<GameObject> startSpawnPoints;

    [SerializeField] public List<Rooms> topRoom;
    [SerializeField] public List<Rooms> downRoom;
    [SerializeField] public List<Rooms> leftRoom;
    [SerializeField] public List<Rooms> rightRoom;

    [System.Serializable]
    public class Rooms
    {
        public List<GameObject> rooms;
    }

    [HideInInspector] public List<GameObject> finalRooms;
    [HideInInspector] public List<GameObject> allRoom;

    [SerializeField] private int minRooms;
    [SerializeField] private int maxRooms;

    [HideInInspector] public int amountRooms = 0;

    [SerializeField] private GameObject Portal;

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
        if (spawned)
        {
            Destroy(playerRoom);
        }

        if (!spawned && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
        if(!spawned && allRoom.Count == amountRooms && GameObject.FindGameObjectsWithTag("RoomSpawnPoint").Length == 0)
        {
            if (finalRooms.Count >= 2)
            {
                for(int i = 0; i < allRoom.Count; i++)
                {
                    allRoom[i].GetComponent<RoomSystem>().SpawnEntity();
                }
                Instantiate(Portal, finalRooms[0].transform).GetComponent<sceneTeleportSystem>().levelToLoad = 1;
                Camera.main.transform.position = new Vector3(finalRooms[1].transform.position.x, finalRooms[1].transform.position.y, Camera.main.transform.position.z);
                GameObject.FindGameObjectWithTag("Player").transform.position = finalRooms[1].transform.position;
                spawned = true;
            }
            else if (finalRooms.Count == 1 && !spawned)
            {
                int rand = Random.Range(0, allRoom.Count);
                for (int i = 0; i < allRoom.Count; i++)
                {
                    if(i != rand) allRoom[i].GetComponent<RoomSystem>().SpawnEntity();
                }
                Instantiate(Portal, finalRooms[0].transform).GetComponent<sceneTeleportSystem>().levelToLoad = 1;
                Camera.main.transform.position = new Vector3(allRoom[rand].transform.position.x, allRoom[rand].transform.position.y, Camera.main.transform.position.z);
                GameObject.FindGameObjectWithTag("Player").transform.position = allRoom[rand].transform.position;
                spawned = true;
            }
            else
            {
                GameObject.FindGameObjectWithTag("LoaderCanvas").GetComponent<loaderSystem>().UnLoadingLevel(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Перезапуск");
            }
        }
    }
}
