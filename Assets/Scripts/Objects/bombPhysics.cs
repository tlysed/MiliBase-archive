using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombPhysics : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    [SerializeField] private int health = 30;
    [SerializeField] private float distance;
    [SerializeField] private GameObject effect;

    [SerializeField] private LayerMask solid;

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, solid);
        if (hitInfo.collider != null)
        {
            Destroy(Instantiate(effect, gameObject.transform.position, Quaternion.identity), 3f);
            if (hitInfo.collider.CompareTag("Car"))
            {
                hitInfo.collider.GetComponent<CarInfo>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        Destroy(gameObject, 60f);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
