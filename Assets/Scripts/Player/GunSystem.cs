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
    [HideInInspector] public bool isReloading = false;

    [SerializeField] private TextMeshProUGUI amountBulletText;

    [SerializeField] private GameObject objBullet;
    [SerializeField] private GameObject heavyBullet;
    private Transform shootPoint;

    [SerializeField] private List<GameObject> models;

    public static weapomTypeEnum weapomType;
    private void Start()
    {
        if (weapomType == weapomTypeEnum.pistol)
        { 
            maxBullets = 54; 
            bullets = 18;

            for (int i = 0, modelIndex = 0; i < models.Count; i++)
            {
                if (i != modelIndex) models[i].SetActive(false);
                else
                {
                    models[i].SetActive(true);
                    shootPoint = models[i].transform.GetChild(0);
                }
            }
        }
        else if (weapomType == weapomTypeEnum.shotgun)
        { 
            maxBullets = 72; 
            bullets = 9;

            for (int i = 0, modelIndex = 1; i < models.Count; i++)
            {
                if (i != modelIndex) models[i].SetActive(false);
                else
                {
                    models[i].SetActive(true);
                    shootPoint = models[i].transform.GetChild(0);
                }
            }
        }
        else if (weapomType == weapomTypeEnum.rifle)
        { 
            maxBullets = 90;
            bullets = 30;
            for (int i = 0, modelIndex = 2; i < models.Count; i++)
            {
                if (i != modelIndex) models[i].SetActive(false);
                else
                {
                    models[i].SetActive(true);
                    shootPoint = models[i].transform.GetChild(0);
                }
            }
        }
        else if (weapomType == weapomTypeEnum.heavy) 
        {
            maxBullets = 9; 
            bullets = 1;

            for (int i = 0, modelIndex = 3; i < models.Count; i++)
            {
                if (i != modelIndex) models[i].SetActive(false);
                else
                {
                    models[i].SetActive(true);
                    shootPoint = models[i].transform.GetChild(0);
                }
            }
        }
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
            else if(!isReadyShoot && !isReloading)
            {
                amountBulletText.text = "Reloading";
                amountBulletText.color = Color.red;
            }
        }
    }
    public void Shoot()
    {
        if(isReadyShoot && !isReloading)
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
                        GetComponentInParent<Animator>().SetBool("reload_Shotgun_Forearm", true);
                        isReloading = true;
                    }
                    else if (weapomType == weapomTypeEnum.rifle)
                    {
                        Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
                        bullets -= 1;
                        timeBtwShots = startTimeBtwShots[2];
                    }
                    else if (weapomType == weapomTypeEnum.heavy)
                    {
                        Instantiate(heavyBullet, shootPoint.position, shootPoint.rotation);
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
    public void ShotGunReloadForeArm()
    {
        GetComponentInParent<Animator>().SetBool("reload_Shotgun_Forearm", false);
        isReloading = false;
    }
    public void Reload()
    {
        int relBullets = 0;
        if (weapomType == weapomTypeEnum.pistol)
        {
            relBullets = 18 - bullets;
        }
        else if (weapomType == weapomTypeEnum.shotgun)
        {
            relBullets = 9 - bullets;
        }
        else if (weapomType == weapomTypeEnum.rifle)
        {
            relBullets = 30 - bullets;
        }
        else if (weapomType == weapomTypeEnum.heavy)
        {
            relBullets = 1 - bullets;
        }

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
        GetComponentInParent<Animator>().SetBool("reload_Pistol", false);
        GetComponentInParent<Animator>().SetBool("reload_Shotgun", false);
        GetComponentInParent<Animator>().SetBool("reload_Ak", false);
        GetComponentInParent<Animator>().SetBool("reload_Heavy", false);
        isReadyShoot = true;
        isReloading = false;
    }
    public void ChangeWeapon(int modelIndex)
    {
        for (int i = 0; i < models.Count; i++)
        {
            if (i != modelIndex) models[i].SetActive(false);
            else
            {
                models[i].SetActive(true);
                shootPoint = models[i].transform.GetChild(0);
            }
        }
    }
}
