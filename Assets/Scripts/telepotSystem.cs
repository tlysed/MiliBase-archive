using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telepotSystem : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position += playerChangePos;
            Camera.main.transform.position += cameraChangePos;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            Destroy(gameObject);
        }
    }
}
