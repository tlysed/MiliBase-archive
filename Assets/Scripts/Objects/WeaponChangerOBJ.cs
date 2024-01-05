using System.Collections;
using System.Collections.Generic;
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
    public WeaponType type;
    public List<Sprite> sprites;
    public GameObject image;

    private void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (type == WeaponType.pistol) image.GetComponent<SpriteRenderer>().sprite = sprites[0];
        else if (type == WeaponType.shotgun) image.GetComponent<SpriteRenderer>().sprite = sprites[1];
        else if (type == WeaponType.rifle) image.GetComponent<SpriteRenderer>().sprite = sprites[2];
        else if (type == WeaponType.heavy) image.GetComponent<SpriteRenderer>().sprite = sprites[3];

        if(player.GetComponent<PlayerStatistics>().CorrectLevel <= 0 && type == WeaponType.pistol)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
        }
        else if (player.GetComponent<PlayerStatistics>().CorrectLevel <= 1 && type == WeaponType.shotgun)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
        }
        else if (player.GetComponent<PlayerStatistics>().CorrectLevel <= 2 && type == WeaponType.rifle)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
        } 
        else if (player.GetComponent<PlayerStatistics>().CorrectLevel <= 3 && type == WeaponType.heavy)
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 0.1f;
            image.GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            var color = image.GetComponent<SpriteRenderer>().color;
            color.a = 1f;
            image.GetComponent<SpriteRenderer>().color = color;
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
        var player = GameObject.FindGameObjectWithTag("Player");
        if (type == WeaponType.pistol && player.GetComponent<PlayerStatistics>().CorrectLevel >= 1) { player.GetComponentInChildren<GunSystem>().weapomType = weapomTypeEnum.pistol; }
        else if (type == WeaponType.shotgun && player.GetComponent<PlayerStatistics>().CorrectLevel >= 2) player.GetComponentInChildren<GunSystem>().weapomType = weapomTypeEnum.shotgun;
        else if (type == WeaponType.rifle && player.GetComponent<PlayerStatistics>().CorrectLevel >= 3) player.GetComponentInChildren<GunSystem>().weapomType = weapomTypeEnum.rifle;
        else if (type == WeaponType.heavy && player.GetComponent<PlayerStatistics>().CorrectLevel >= 4) player.GetComponentInChildren<GunSystem>().weapomType = weapomTypeEnum.heavy;
    }
}
