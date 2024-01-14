using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCheker : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject metalDoor;
    
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
