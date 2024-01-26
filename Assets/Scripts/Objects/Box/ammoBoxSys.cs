using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoBoxSys : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int plusBullets = 0;
            if (GunSystem.weapomType == weapomTypeEnum.pistol) plusBullets = 54;
            else if (GunSystem.weapomType == weapomTypeEnum.shotgun) plusBullets = 48;
            else if (GunSystem.weapomType == weapomTypeEnum.rifle) plusBullets = 90;
            else if (GunSystem.weapomType == weapomTypeEnum.heavy) plusBullets = 9;
            collision.GetComponentInChildren<GunSystem>().maxBullets += plusBullets;
            Destroy(gameObject);
        }
    }
}
