using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum weapomTypeEnum
{
    pistol,
    shotgun,
    rifle,
    heavy
}

public class GunSystem : MonoBehaviour
{
    [HideInInspector] public int maxBullets;
    [HideInInspector] public int bullets;//30-��������/6-���������/1-�������/18-��������

    public List<float> startTimeBtwShots;
    private float timeBtwShots;

    [HideInInspector] public bool isReadyShoot = true;

    public TextMeshProUGUI amountBulletText;

    public GameObject objBullet;
    public Transform shootPoint;

    public weapomTypeEnum weapomType;
    private static weapomTypeEnum weapomTypeStatic;
    private void Start()
    {
        if (weapomType == weapomTypeEnum.pistol) { maxBullets = 54; bullets = 18; }
        else if (weapomType == weapomTypeEnum.shotgun) { maxBullets = 18; bullets = 6; }
        else if (weapomType == weapomTypeEnum.rifle) { maxBullets = 90; bullets = 30; }
        else if (weapomType == weapomTypeEnum.heavy) { maxBullets = 3; bullets = 1; }
    }
    private void Update()
    {
        if (isReadyShoot)
        {
            amountBulletText.text = bullets.ToString() + "/" + maxBullets.ToString();
            amountBulletText.color = Color.white;
        }
        else
        {
            amountBulletText.text = "Reloading"; 
            amountBulletText.color = Color.red;
        }
    }
    public void Shoot()
    {
        if(isReadyShoot == true)
        {
            if(bullets>0)
            {
                if (timeBtwShots <= 0)
                {
                    if (weapomType == weapomTypeEnum.pistol)
                    {
                        Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
                        bullets -= 1;
                        timeBtwShots = startTimeBtwShots[0];
                    }
                    else if (weapomType == weapomTypeEnum.shotgun)
                    {
                        Instantiate(objBullet, shootPoint.position, Quaternion.Euler(shootPoint.localEulerAngles.x, shootPoint.localEulerAngles.y, shootPoint.localEulerAngles.z - 75));
                        Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
                        Instantiate(objBullet, shootPoint.position, Quaternion.Euler(shootPoint.localEulerAngles.x, shootPoint.localEulerAngles.y, shootPoint.localEulerAngles.z - 105));
                        bullets -= 3;
                        timeBtwShots = startTimeBtwShots[1];
                    }
                    else if (weapomType == weapomTypeEnum.rifle)
                    {
                        Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
                        bullets -= 1;
                        timeBtwShots = startTimeBtwShots[2];
                    }
                    else if (weapomType == weapomTypeEnum.heavy)
                    {
                        Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
                        bullets -= 1;
                        timeBtwShots = startTimeBtwShots[3];
                    }
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
        int relBullets = 0;
        if (weapomType == weapomTypeEnum.pistol) relBullets = 18 - bullets;
        else if (weapomType == weapomTypeEnum.shotgun) relBullets = 6 - bullets;
        else if (weapomType == weapomTypeEnum.rifle) relBullets = 30 - bullets; 
        else if (weapomType == weapomTypeEnum.heavy) relBullets = 1 - bullets;

        if (maxBullets - relBullets > 0)
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
    private void OnDestroy()
    {
        weapomTypeStatic = weapomType;
    }
    private void OnEnable()
    {
        weapomType = weapomTypeStatic;
    }
}
