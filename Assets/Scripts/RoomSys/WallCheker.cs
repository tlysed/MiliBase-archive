using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCheker : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject metalDoor;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            var door = Instantiate(metalDoor, spawnPoint);
            other.GetComponentInParent<RoomSystem>().metalDoors.Add(door);
            Destroy(other.gameObject);
        }
    }

}
