using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CarTable : MonoBehaviour
{
    [SerializeField] private GameObject CarCanvas;
    [SerializeField] private GameObject physicalObject;
    [SerializeField] private TextMeshProUGUI ammountDetails;

    [SerializeField] private List<GameObject> upgradeButtons;

    private int[] maxTimeUpgrade = { 2, 2, 2 };

    private GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(PlayerStatistics.Points < 400)
        {
            physicalObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        ammountDetails.text = "Количество деталей: " + CarInfo.details.ToString();
    }
    public void carOpen(bool _bool)
    {
        if (_bool)
        {
            CarCanvas.transform.GetChild(0).gameObject.SetActive(true);
            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            CarCanvas.transform.GetChild(0).gameObject.SetActive(false);
            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            carOpen(true);
        }
    }
    public void Upgrade(int _numberOfUpgrade)
    {
        if (maxTimeUpgrade[_numberOfUpgrade] > 0)
        {
            if (CarInfo.details > 0)
            {
                if (_numberOfUpgrade == 0 && CarInfo.maxDistance < 200)
                {
                    CarInfo.maxDistance += 50;
                    YandexGame.savesData.maxDistance = CarInfo.maxDistance;
                }
                else if (_numberOfUpgrade == 1 && CarInfo.carSpeed < 3)
                {
                    CarInfo.carSpeed++;
                    YandexGame.savesData.carSpeed = CarInfo.carSpeed;
                }
                else if (_numberOfUpgrade == 2 && CarInfo.armorThickness < 2)
                {
                    CarInfo.armorThickness++;
                    YandexGame.savesData.armorThickness = CarInfo.armorThickness;
                }

                CarInfo.details--;
                maxTimeUpgrade[_numberOfUpgrade]--;
                ammountDetails.text = "Количество деталей: " + CarInfo.details.ToString();
            }
            else
            {
                StartCoroutine(errorUpgrade());
            }

            if (maxTimeUpgrade[_numberOfUpgrade] == 0) upgradeButtons[_numberOfUpgrade].gameObject.GetComponent<Button>().interactable = false;
        }
    }
    IEnumerator errorUpgrade()
    {
        ammountDetails.text = "ДЕТАЛЕЙ НЕ ХВАТАЕТ";
        yield return new WaitForSeconds(1);
        ammountDetails.text = "Количество деталей: " + CarInfo.details.ToString();
    }
}
