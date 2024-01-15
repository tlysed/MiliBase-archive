using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponChanger : MonoBehaviour
{
    public GameObject weapon;
    public List<Sprite> sprites;
    private Image image;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if(weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.pistol)
        {
            image.sprite = sprites[0];
        }
        else if (weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.shotgun)
        {
            image.sprite = sprites[1];
        }
        else if (weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.rifle)
        {
            image.sprite = sprites[2];
        }
        else if (weapon.GetComponent<GunSystem>().weapomType == weapomTypeEnum.heavy)
        {
            image.sprite = sprites[3];
        }
    }
}
