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
     public int bullets;//30-автоматы/6-дробовики/1-тяжелое/18-пистолет

    [SerializeField] private List<float> startTimeBtwShots;
    private float timeBtwShots;

    [HideInInspector] public bool isReadyShoot = true;

    [SerializeField] private TextMeshProUGUI amountBulletText;

    [SerializeField] private GameObject objBullet;
    [SerializeField] private Transform shootPoint;

    public static weapomTypeEnum weapomType;
    private void Start()
    {
        if (weapomType == weapomTypeEnum.pistol) { maxBullets = 54; bullets = 18; }
        else if (weapomType == weapomTypeEnum.shotgun) { maxBullets = 48; bullets = 6; }
        else if (weapomType == weapomTypeEnum.rifle) { maxBullets = 90; bullets = 30; }
        else if (weapomType == weapomTypeEnum.heavy) { maxBullets = 9; bullets = 1; }
    }
    private void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>().safeZone)
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
                        Instantiate(objBullet, shootPoint.position, Quaternion.Euler(shootPoint.rotation.eulerAngles.x, shootPoint.rotation.eulerAngles.y, shootPoint.rotation.eulerAngles.z - 15));
                        Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
                        Instantiate(objBullet, shootPoint.position, Quaternion.Euler(shootPoint.rotation.eulerAngles.x, shootPoint.rotation.eulerAngles.y, shootPoint.rotation.eulerAngles.z + 15));
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
        GetComponentInParent<Animator>().SetBool("start_reload", false);
        isReadyShoot = true;
    }
}
