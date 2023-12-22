using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletphysics : MonoBehaviour
{
    private Rigidbody2D rd;
    public float speed;

    public float lifeTime;
    public float distance;
    public int damage;

    public LayerMask solid;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, solid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Box")){
                hitInfo.collider.GetComponent<BoxSystems>().TakeDamage(damage);
            }
            else if (hitInfo.collider.CompareTag("Door"))
            {
                hitInfo.collider.GetComponentInParent<doorSystem>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        
        Destroy(gameObject, lifeTime);
    }
}
