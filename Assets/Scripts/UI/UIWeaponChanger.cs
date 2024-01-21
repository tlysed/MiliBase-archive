using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponChanger : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private List<Sprite> sprites;
    private Image image;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if(GunSystem.weapomType == weapomTypeEnum.pistol)
        {
            image.sprite = sprites[0];
        }
        else if (GunSystem.weapomType == weapomTypeEnum.shotgun)
        {
            image.sprite = sprites[1];
        }
        else if (GunSystem.weapomType == weapomTypeEnum.rifle)
        {
            image.sprite = sprites[2];
        }
        else if (GunSystem.weapomType == weapomTypeEnum.heavy)
        {
            image.sprite = sprites[3];
        }
    }
}
