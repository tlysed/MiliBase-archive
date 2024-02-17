using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerInfo : MonoBehaviour
{
    [Header("Health")]

    public float health;//100%=100 HP
    [SerializeField] private int timeWaitingHeal;

    private float safeTime;

    [Header("Movement")]

    [SerializeField] private float speed;
    [SerializeField] public static bool isMobile;
    [SerializeField] private Joystick _walkJoystick;
    [SerializeField] private Joystick _shootJoystick;


    [Header("Weapon")]

    [SerializeField] public bool safeZone = false;
    [SerializeField] private GameObject weapon;

    [Header("UI")]

    [SerializeField] private GameObject interFaceImage;
    private bool ended = false;
    [SerializeField] private int levelToLoad;

    private Rigidbody2D rd;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private Animator anim;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        safeTime = -timeWaitingHeal;

        
        if (!isMobile)
        {
            _walkJoystick.gameObject.SetActive(false);
            _shootJoystick.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        //                                      Health

        if (health > 100) health = 100;
        else if (health <= 0)
        {
            if (!ended) StartCoroutine(EndWindow());
            health = -1;
        }
        if (Time.time - safeTime > timeWaitingHeal) Healing();
        if(!safeZone) interFaceImage.GetComponent<Image>().color = new Color(105f,0,0, Mathf.InverseLerp(100,-1,health));
        
        //                                      Movement
        if(!isMobile)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//движение

            var mousePosition = Input.mousePosition;
            if (Camera.main.enabled)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
                float rotateZ = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
            }
        }
        else if(isMobile)
        {
            float rotateZ;
            moveInput = new Vector2(_walkJoystick.Horizontal, _walkJoystick.Vertical);//движение
            if (Mathf.Abs(_shootJoystick.Horizontal) > 0 || Mathf.Abs(_shootJoystick.Horizontal) > 0)
            {
                rotateZ = Mathf.Atan2(_shootJoystick.Vertical, _shootJoystick.Horizontal) * Mathf.Rad2Deg;
                if(!safeZone) weapon.GetComponent<GunSystem>().Shoot();
            }
            else
            {
                rotateZ = Mathf.Atan2(_walkJoystick.Vertical, _walkJoystick.Horizontal) * Mathf.Rad2Deg;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        }
        moveVelocity = moveInput.normalized * speed;
        if(moveVelocity != new Vector2(0,0))
        {
            anim.SetTrigger("walking");
            StartCoroutine(Walking());
        }

        //                                      Player Interaction

        if (Input.GetMouseButton(0) && !safeZone && !isMobile)
        {
            weapon.GetComponent<GunSystem>().Shoot();
        }

        if ((Input.GetKeyDown(KeyCode.R) && !anim.GetBool("right_Click") && !anim.GetBool("left_Click") && !safeZone) || weapon.GetComponent<GunSystem>().bullets == 0 && weapon.GetComponent<GunSystem>().maxBullets != 0)
        {
            if(GunSystem.weapomType == weapomTypeEnum.pistol)
            {
                anim.SetBool("reload_Pistol", true);
            }
            else if (GunSystem.weapomType == weapomTypeEnum.shotgun)
            {
                anim.SetBool("reload_Shotgun", true);
                weapon.GetComponent<GunSystem>().isReloading = true;
            }
            else if (GunSystem.weapomType == weapomTypeEnum.rifle)
            {
                anim.SetBool("reload_Ak", true);
            }
            else if (GunSystem.weapomType == weapomTypeEnum.heavy)
            {
                anim.SetBool("reload_Heavy", true);
            }
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
    public void ShotGunReloadForeArmFromPlayer()
    {
        weapon.GetComponent<GunSystem>().ShotGunReloadForeArm();
    }
    private void Healing()
    {
        if (health < 100) health += Time.deltaTime * 10;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        safeTime = Time.time;
    }
    IEnumerator Walking()
    {
        PlayerStatistics.Distance++;
        yield return new WaitForSeconds(1);
    }
    IEnumerator EndWindow()
    {
        GameObject.FindGameObjectWithTag("EndWindow").transform.GetChild(0).gameObject.SetActive(true);
        FinalWindow.turnBasedWindow(levelToLoad);
        rd.constraints = RigidbodyConstraints2D.FreezePosition;
        ended = true;
        yield return new WaitForSeconds(1);
    }
}
