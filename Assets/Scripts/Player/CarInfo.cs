using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using static UnityEngine.AudioSettings;

public class CarInfo : MonoBehaviour
{
    [Header("Car")]

    [SerializeField] private int levelToLoad;

    [SerializeField] private int bullets = 90; // 90

    [SerializeField] private float startTimeBtwShots;

    private float timeBtwShots;

    [Header("Car Upgrade")]

    [SerializeField] private int correctDistance;
    private int neededDistance = 600;

    public static int maxDistance = 100;
    public static int carSpeed = 1;
    public static int armorThickness = 0;
    public static int details = 0;

    public static int distance;
    public static int detailsTaken;

    [Header("Transform/GameObject")]

    [SerializeField] private Transform pointOfAttachment;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject soundOfGuns;

    [SerializeField] private Joystick _aimJoystick;
    [SerializeField] private Button _shootButton;

    [SerializeField] private GameObject timeLine;
    [SerializeField] private Slider visualDistance;

    private bool buttonPressed = false;

    private void Start()
    {
        correctDistance = maxDistance * carSpeed;
        distance = 0;
        detailsTaken = 0;
        StartCoroutine(Driving());

        if (!PlayerInfo.isMobile)
        {
            _aimJoystick.gameObject.SetActive(false);
            _shootButton.gameObject.SetActive(false);
        }
    }
    private void Update()
    {

        if (!PlayerInfo.isMobile)
        {
            var mousePosition = Input.mousePosition;
            if (Camera.main.enabled)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
                float rotateZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

                if (rotateZ <= 125 && rotateZ >= 55)
                {
                    pointOfAttachment.rotation = Quaternion.Euler(0f, 0f, rotateZ);
                }
            }

        }
        else if (PlayerInfo.isMobile)
        {
            pointOfAttachment.rotation = Quaternion.Euler(0f, 0f, 90 - Mathf.Sign(_aimJoystick.Horizontal) * Mathf.Lerp(0, 35, Mathf.Abs(_aimJoystick.Horizontal)));
        }

        if (Input.GetMouseButton(0) && !PlayerInfo.isMobile)
        {
             Shoot();
        }
        else if (PlayerInfo.isMobile && buttonPressed)
        {
             Shoot();
        }
        visualDistance.value = distance;
    }
    public void Shoot()
    {
        if (bullets == 0)
        {
            StartCoroutine(Reload());
            bullets = -1;
        }
        else if (bullets > 0) 
        { 
            if (timeBtwShots <= 0)
            {
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                Destroy(Instantiate(soundOfGuns, shootPoint), 3f);
                bullets -= 1;
                timeBtwShots = startTimeBtwShots;
            }
            else timeBtwShots -= Time.deltaTime;
        }
    }
    public void TakeDamage(int damage)
    {
        correctDistance -= damage * (1 - (armorThickness / 2));
    }
    public void PressedButton(bool _bool)
    {
        buttonPressed = _bool;
    }
    IEnumerator Reload()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(4.460f);
        bullets = 90;
    }
    IEnumerator Driving()
    {
        if (distance % 50 == 0 && distance != 0)
        { 
            details++; detailsTaken++; 
            if(YandexGame.SDKEnabled) YandexGame.savesData.details = details;
        }

        yield return new WaitForSeconds(0.5f);
        if (correctDistance > 0)
        {
            correctDistance--;
            neededDistance--;
            distance++;
            StartCoroutine(Driving());
        }
        else
        {
            if (neededDistance == correctDistance) timeLine.GetComponent<timeline>().goodEndStart();
            else StartCoroutine(EndWindow());
            yield return null;
        }
    }
    IEnumerator EndWindow()
    {
        timeLine.GetComponent<timeline>().badEndStart();
        GameObject.FindGameObjectWithTag("EndWindow").transform.GetChild(0).gameObject.SetActive(true);
        FinalWindow.highwayWindow(levelToLoad);
        yield return new WaitForSeconds(1);
    }
}
