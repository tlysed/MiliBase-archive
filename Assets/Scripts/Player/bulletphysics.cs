using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletphysics : MonoBehaviour
{
    private Rigidbody2D rd;
    [SerializeField] private float speed;

    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    [SerializeField] private List<int> damage;
    private int realDamage = 3;
    [SerializeField] private bool enemyBullet;

    [SerializeField] private LayerMask solid;

    [SerializeField] private GameObject effect;
    private GameObject weapon;
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        rd = GetComponent<Rigidbody2D>();
        if (weapon != null && !enemyBullet)
        {
            if (GunSystem.weapomType == weapomTypeEnum.pistol)
            {
                realDamage = damage[0];
            }
            else if (GunSystem.weapomType == weapomTypeEnum.shotgun)
            {
                realDamage = damage[1];
            }
            else if (GunSystem.weapomType == weapomTypeEnum.rifle)
            {
                realDamage = damage[2];
            }
            else if (GunSystem.weapomType == weapomTypeEnum.heavy)
            {
                realDamage = damage[0];
            }
        }
        else realDamage = 3;
    }
    
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, solid);
        if (hitInfo.collider != null)
        {
            Destroy(Instantiate(effect, gameObject.transform.position, Quaternion.identity), lifeTime);
            if (hitInfo.collider.CompareTag("Box")){
                hitInfo.collider.GetComponent<BoxSystems>().TakeDamage(realDamage);
            }
            else if (hitInfo.collider.CompareTag("Door"))
            {
                hitInfo.collider.GetComponentInParent<doorSystem>().TakeDamage(realDamage);
            }
            else if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitInfo.collider.GetComponentInParent<PlayerInfo>().TakeDamage(realDamage);
            }
            else if (hitInfo.collider.CompareTag("Enemy") && !enemyBullet)
            {
                hitInfo.collider.GetComponentInParent<enemyControl>().TakeDamage(realDamage);
            }
            Destroy(gameObject);
        }

        Destroy(gameObject, lifeTime);
    }
}
