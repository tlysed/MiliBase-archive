using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyControl : MonoBehaviour
{
    [Header("Health")]

    public float health;//100%=100 HP
    public GameObject damageEffect;

    [Header("Movement")]

    public float speed;
    public bool isTriggered = false;
    public GameObject[] pointsOfPath;

    private int correctPoint = 0;
    private float waitTime;

    [Header("Weapon")]

    public enemyTypeEnum enemyType;
    public enum enemyTypeEnum
    {
        nearby,
        armed,
        heavy
    }

    public float startTimeBtwShots;
    private float timeBtwShots;

    public GameObject weapon_GUN;
    public Transform shootPoint;
    public GameObject objBullet;

    public GameObject weapon_MELEE;
    public int meleeDamage;

    public int heavyDamage;

    private Animator anim;
    void Start()
    {
        weapon_GUN.SetActive(false);
        weapon_MELEE.SetActive(false);

        anim = GetComponent<Animator>();

        waitTime = Random.Range(3, 5);

        int rand = Random.Range(0, 2);
        if (rand == 0) { enemyType = enemyTypeEnum.nearby; health = 150; speed = 5; weapon_MELEE.SetActive(true); }
        else if (rand == 1) { enemyType = enemyTypeEnum.armed; health = 100; speed = 4; weapon_GUN.SetActive(true); }
        else { enemyType = enemyTypeEnum.heavy; health = 500; speed = 1; }
    }
       
    void Update()
    {
        if (isTriggered == false)
        {
            if (pointsOfPath.Length != 0) FollowPoints(pointsOfPath[correctPoint]);
            if(Vector3.Distance(pointsOfPath[correctPoint].transform.position, transform.position) < 1)
            { 
                if (waitTime > 0)
                {
                    waitTime -= Time.deltaTime;
                }
                else
                {
                    if (correctPoint == pointsOfPath.Length - 1)
                    {
                        correctPoint = 0;
                        waitTime = Random.Range(3, 5);
                    }
                    else
                    {
                        correctPoint++;
                        waitTime = Random.Range(3, 5);
                    }
                }
                
            }
        }
        else if(isTriggered == true)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (enemyType == enemyTypeEnum.nearby)
            {
                if(Vector3.Distance(Player.transform.position, gameObject.transform.position) < 3.5f) MeleeAttack();
            }
            else if (enemyType == enemyTypeEnum.armed)
            {
                Shoot();
            }
            
            else if (enemyType == enemyTypeEnum.heavy)
            {
                //if (Vector3.Distance(Player.transform.position, gameObject.transform.position) < 4f) //nothing
            }
            Follow(Player);
        }

        //-------------HEALTH-------------
        if(health < 0)
        {
            gameObject.GetComponentInParent<RoomSystem>().enemyInRoom.Remove(gameObject);
            Destroy(Instantiate(damageEffect, gameObject.transform.position, Quaternion.identity), 1f);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>().TakePoints(1);
        health -= damage;
    }
    public void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(objBullet, shootPoint.position, shootPoint.rotation);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public void MeleeAttack()
    {
        if (timeBtwShots <= 0)
        {
            anim.SetTrigger("attack");
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public void OnMeleeAttack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, transform.position) < 3.25f)
        {
            player.GetComponent<PlayerInfo>().TakeDamage(meleeDamage);
        }
    }
    void FollowPoints(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed / 2 * Time.deltaTime);

        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(Vector3.forward * angle);
        transform.rotation = Quaternion.Slerp(transform.localRotation, newRotation, speed/1.5f * Time.deltaTime);
    }
    void Follow(GameObject target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) > 3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed / 1.5f * Time.deltaTime);
        }

        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(Vector3.forward * angle);
        transform.rotation = Quaternion.Slerp(transform.localRotation, newRotation, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
        }

    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }*/
}
