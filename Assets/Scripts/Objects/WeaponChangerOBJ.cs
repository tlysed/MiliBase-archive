using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChangerOBJ : MonoBehaviour
{
    private enum WeaponType
    {
        pistol,
        shotgun,
        rifle,
        heavy,
        none
    }
    [SerializeField] private WeaponType type;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private GameObject image;
    [SerializeField] private TextMeshProUGUI text;

   private void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (type == WeaponType.pistol) image.GetComponent<SpriteRenderer>().sprite = sprites[0];
        else if (type == WeaponType.shotgun) image.GetComponent<SpriteRenderer>().sprite = sprites[1];
        else if (type == WeaponType.rifle) image.GetComponent<SpriteRenderer>().sprite = sprites[2];
        else if (type == WeaponType.heavy) image.GetComponent<SpriteRenderer>().sprite = sprites[3];

        if(PlayerStatistics.Points < 0 && type == WeaponType.pistol)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Нужно очков : 0";
        }
        else if (PlayerStatistics.Points < 200 && type == WeaponType.shotgun)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Нужно очков : 200";
        }
        else if (PlayerStatistics.Points < 350 && type == WeaponType.rifle)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Нужно очков : 350";
        } 
        else if (PlayerStatistics.Points < 500 && type == WeaponType.heavy)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Нужно очков : 500";
        }
        else
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "";
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeWeaponType(collision.gameObject);
        }
    }
    private void ChangeWeaponType(GameObject player)
    {

        if (type == WeaponType.pistol && PlayerStatistics.Points >= 0)
        {
            GunSystem.weapomType = weapomTypeEnum.pistol;
            player.GetComponentInChildren<GunSystem>().ChangeWeapon(0);
        }
        else if (type == WeaponType.shotgun && PlayerStatistics.Points >= 200)
        {
            GunSystem.weapomType = weapomTypeEnum.shotgun;
            player.GetComponentInChildren<GunSystem>().ChangeWeapon(1);
        }
        else if (type == WeaponType.rifle && PlayerStatistics.Points >= 350)
        {
            GunSystem.weapomType = weapomTypeEnum.rifle;
            player.GetComponentInChildren<GunSystem>().ChangeWeapon(2);
        }
        else if (type == WeaponType.heavy && PlayerStatistics.Points >= 500)
        {
            GunSystem.weapomType = weapomTypeEnum.heavy;
            player.GetComponentInChildren<GunSystem>().ChangeWeapon(3);
        }
    }
}
