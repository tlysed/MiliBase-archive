using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("Health")]

    public float health;//100%=100 HP
    public int timeWaitingHeal;
    public GameObject damageEffect;

    private float safeTime;

    [Header("Movement")]

    public float speed;

    [Header("Weapon")]

    public bool safeZone = false;
    public GameObject weapon;

    [Header("UI")]

    public GameObject interFaceImage;

    private Rigidbody2D rd;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Animator anim;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        safeTime = -timeWaitingHeal;
    }


    void Update()
    {
        //                                      Health

        if (health > 100) health = 100;
        else if (health <= 0)
        {
            health = 0;
        }
        if (Time.time - safeTime > timeWaitingHeal) Healing();
        if(!safeZone) interFaceImage.GetComponent<Image>().color = new Color(105f,0,0, Mathf.InverseLerp(100,-1,health));
        
        //                                      Movement
        
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//движение
        moveVelocity = moveInput.normalized * speed;

        var mousePosition = Input.mousePosition;
        if (Camera.main.enabled)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
            float rotateZ = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }

        //                                      Player Interaction

        if (Input.GetMouseButton(0) && !safeZone)
        {
            weapon.GetComponent<GunSystem>().Shoot();
        }
        weapon.SetActive(!safeZone);


        if (Input.GetKeyDown(KeyCode.R) && !anim.GetBool("right_Click") && !anim.GetBool("left_Click") && !safeZone)
        {
            anim.SetTrigger("start_Reload");
            weapon.GetComponent<GunSystem>().isReadyShoot = false;
        }

        if (Input.GetKey(KeyCode.E)) anim.SetBool("right_Click", true);
        else anim.SetBool("right_Click", false);


        if (Input.GetKey(KeyCode.Q)) anim.SetBool("left_Click", true);
        else anim.SetBool("left_Click", false);

        //                                      Interaction

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject.FindWithTag("Door").GetComponent<doorSystem>().ChangeDoorState();
        }
    }

    private void FixedUpdate()
    {
        rd.MovePosition(rd.position + moveVelocity * Time.fixedDeltaTime);
    }
    public void ReloadFromPlayer()
    {
        weapon.GetComponent<GunSystem>().Reload();
    }
    private void Healing()
    {
        if (health < 100) health += Time.deltaTime * 10;
    }
    public void TakeDamage(int damage)
    {
        if (GameObject.FindGameObjectWithTag("RoomSpawner").GetComponent<spawnerRooms>().spawned)
        {
            //Destroy(Instantiate(damageEffect,gameObject.transform.position, Quaternion.identity), 1f);
            health -= damage;
            safeTime = Time.time;
        }
    }
}
