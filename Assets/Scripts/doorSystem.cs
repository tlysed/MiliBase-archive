using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSystem : MonoBehaviour
{
    public int health;
    public int speedChangindDoor;

    private HingeJoint2D hj;
    private JointMotor2D hj_jm;
    private GameObject Player;

    private bool stateDoor = false;// false - закрыта, true - открыта

    private void Start()
    {
        hj = GetComponent<HingeJoint2D>();
        hj_jm = hj.motor;
        Player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if(health <= 15)//0<h<15
        {
            hj.useMotor = false;
        }
        if (health <= 0)//h<0
        {
            Destroy(gameObject);
        }
        if(health > 15)
        {
            hj.useMotor = true;
            if (stateDoor == false)
            {
                hj_jm.motorSpeed = speedChangindDoor;
                hj.motor = hj_jm;
            }
            else
            {
                hj_jm.motorSpeed = -speedChangindDoor;
                hj.motor = hj_jm;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void ChangeDoorState()
    {
        if (Vector3.Distance(Player.transform.position, gameObject.transform.position) < 3)
        {
            stateDoor = !stateDoor;
        }
    }
}
