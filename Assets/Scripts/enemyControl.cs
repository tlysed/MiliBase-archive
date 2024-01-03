using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemyControl : MonoBehaviour
{
    public float health;//100%=100 HP
    public float speed;

    public GameObject[] pointsOfPath;
    private int correctPoint = 0;

    private bool isTriggered = false;
    private float waitTime;

    void Start()
    {
        waitTime = Random.Range(3, 5);
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
            Follow(Player);
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
        if (Vector3.Distance(target.transform.position, transform.position) > 2.5f)
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}
