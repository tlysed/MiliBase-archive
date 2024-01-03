using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoBoxSys : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<GunSystem>().maxBullets += 30;
            Destroy(gameObject);
        }
    }
}
