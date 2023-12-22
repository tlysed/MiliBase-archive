using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float health;//100%=100 HP
    public float speed;

    public GameObject weaponOBJ;

    private Rigidbody2D rd;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Animator anim;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//движение
        moveVelocity = moveInput.normalized * speed;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotateZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

        if (Input.GetMouseButton(0))
        {
            weaponOBJ.GetComponent<GunSystem>().Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && anim.GetBool("right_Click") ==false && anim.GetBool("left_Click") == false)
        {
            anim.SetTrigger("start_Reload");
            weaponOBJ.GetComponent<GunSystem>().isReadyShoot = false;
        }

        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("right_Click", true);
        }
        else
        {
            anim.SetBool("right_Click", false);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("left_Click", true);
        }
        else
        {
            anim.SetBool("left_Click", false);
        }

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
        weaponOBJ.GetComponent<GunSystem>().Reload();
    }

}
