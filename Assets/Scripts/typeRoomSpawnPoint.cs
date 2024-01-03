using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.UIElements.ToolbarMenu;

public class typeRoomSpawnPoint : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        Left,
        Right,
        Down,
        Top,
        None
    }

    private spawnerRooms scriptRoom;
    private bool spawned = false;
    private int rand;
    private float waitTime = 3f;

    private void Start()
    {
        scriptRoom = GameObject.FindGameObjectWithTag("RoomSpawner").GetComponent<spawnerRooms>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }
    public void Spawn()
    {
        
        if (!spawned && scriptRoom.nowRoom < scriptRoom.amountRooms)
        {
            if(direction == Direction.Top)
            {
                rand = Random.Range(0, scriptRoom.topRoom[1].rooms.Count);
                Instantiate(scriptRoom.topRoom[1].rooms[rand], transform.position, scriptRoom.topRoom[1].rooms[rand].transform.rotation);
            }
            else if (direction == Direction.Down)
            {
                rand = Random.Range(0, scriptRoom.topRoom[1].rooms.Count);
                Instantiate(scriptRoom.downRoom[1].rooms[rand], transform.position, scriptRoom.downRoom[1].rooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, scriptRoom.topRoom[1].rooms.Count);
                Instantiate(scriptRoom.leftRoom[1].rooms[rand], transform.position, scriptRoom.leftRoom[1].rooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, scriptRoom.topRoom[1].rooms.Count);
                Instantiate(scriptRoom.rightRoom[1].rooms[rand], transform.position, scriptRoom.rightRoom[1].rooms[rand].transform.rotation);
            }
            scriptRoom.nowRoom++;
            spawned = true;
        }
        else if (!spawned && scriptRoom.nowRoom == scriptRoom.amountRooms)
        {
            scriptRoom.finalRoomsAmount = GameObject.FindGameObjectsWithTag("RoomSpawnPoint").Length;
            if (direction == Direction.Top)
            {
                scriptRoom.finalRooms.Add(Instantiate(scriptRoom.topRoom[0].rooms[0], transform.position, scriptRoom.topRoom[0].rooms[0].transform.rotation));
            }
            else if (direction == Direction.Down)
            {
                scriptRoom.finalRooms.Add(Instantiate(scriptRoom.downRoom[0].rooms[0], transform.position, scriptRoom.downRoom[0].rooms[0].transform.rotation));
            }
            else if (direction == Direction.Left)
            {
                scriptRoom.finalRooms.Add(Instantiate(scriptRoom.leftRoom[0].rooms[0], transform.position, scriptRoom.leftRoom[0].rooms[0].transform.rotation));
            }
            else if (direction == Direction.Right)
            {
                scriptRoom.finalRooms.Add(Instantiate(scriptRoom.rightRoom[0].rooms[0], transform.position, scriptRoom.rightRoom[0].rooms[0].transform.rotation));
            }
            spawned = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("RoomSpawnPoint") && other.GetComponent<typeRoomSpawnPoint>().spawned)
        {
            Destroy(gameObject);
        }
    }
}
