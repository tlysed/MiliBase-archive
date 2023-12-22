using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public int maxBullets;//���� �� �������������
    public int bullets;//30-��������/8-���������/5-�������

    public float startTimeBtwShots;
    private float timeBtwShots;

    public bool isReadyShoot = true;

    public GameObject Player;

    public GameObject objBullet;
    public Transform shootPoint;

    public void Shoot()
    {
        if(isReadyShoot == true)
        {
            if(bullets>0)
            {
                if (timeBtwShots <= 0)
                {
                    Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
                    bullets-=1;
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
    }
    public void Reload()
    {
        int relBullets = 30 - bullets;
        if(maxBullets- relBullets > 0)
        {
            maxBullets -= relBullets;
            bullets += relBullets;
        }
        else
        {
            bullets += maxBullets;
            maxBullets = 0;
        }
        isReadyShoot = true;
        return;
    }
}
