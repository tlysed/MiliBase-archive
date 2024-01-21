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
            else if (GunSystem.weapomType == weapomTypeEnum.pistol) plusBullets = 36;
            else if (GunSystem.weapomType == weapomTypeEnum.pistol) plusBullets = 90;
            else if (GunSystem.weapomType == weapomTypeEnum.pistol) plusBullets = 3;
            collision.GetComponentInChildren<GunSystem>().maxBullets += plusBullets;
            Destroy(gameObject);
        }
    }
}
