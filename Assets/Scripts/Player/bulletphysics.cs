using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletphysics : MonoBehaviour
{
    private Rigidbody2D rd;
    public float speed;

    public float lifeTime;
    public float distance;
    public List<int> damage;
    private int realDamage = 5;

    public LayerMask solid;

    public GameObject effect;
    public GameObject weapon;
    void Start()
    {
         weapon = GameObject.FindGameObjectWithTag("Weapon");
        rd = GetComponent<Rigidbody2D>();
        if(weapon != null)
        {
            if (weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.pistol)
            {
                realDamage = damage[0];
            }
            else if (weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.shotgun)
            {
                realDamage = damage[1];
            }
            else if (weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.rifle)
            {
                realDamage = damage[2];
            }
            else if (weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.heavy)
            {
                realDamage = damage[3];
            }
        }
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
            else if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponentInParent<PlayerInfo>().TakeDamage(realDamage);
            }
            Destroy(gameObject);
        }

        Destroy(gameObject, lifeTime);
    }
}
