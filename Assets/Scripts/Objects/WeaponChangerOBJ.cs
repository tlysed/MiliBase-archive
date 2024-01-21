using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChangerOBJ : MonoBehaviour
{
    public enum WeaponType
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

        if(PlayerStatistics.Levels <= 0 && type == WeaponType.pistol)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Needed level : 1";
        }
        else if (PlayerStatistics.Levels <= 1 && type == WeaponType.shotgun)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Needed level : 2";
        }
        else if (PlayerStatistics.Levels <= 2 && type == WeaponType.rifle)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Needed level : 3";
        } 
        else if (PlayerStatistics.Levels <= 3 && type == WeaponType.heavy)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
            text.text = "Needed level : 4";
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
            ChangeWeaponType();
        }
    }
    void ChangeWeaponType()
    {
        if (type == WeaponType.pistol && PlayerStatistics.Levels >= 1) { GunSystem.weapomType = weapomTypeEnum.pistol; }
        else if (type == WeaponType.shotgun && PlayerStatistics.Levels >= 2) GunSystem.weapomType = weapomTypeEnum.shotgun;
        else if (type == WeaponType.rifle && PlayerStatistics.Levels >= 3) GunSystem.weapomType = weapomTypeEnum.rifle;
        else if (type == WeaponType.heavy && PlayerStatistics.Levels >= 4) GunSystem.weapomType = weapomTypeEnum.heavy;
    }
}
