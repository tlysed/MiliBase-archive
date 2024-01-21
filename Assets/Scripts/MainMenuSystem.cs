using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private Toggle isMobileToggle;
    private void Awake()
    {
        isMobileToggle.isOn = PlayerInfo.isMobile;
    }
    public void ChangeControlType(bool var)
    {
        PlayerInfo.isMobile = var;
    }
}
