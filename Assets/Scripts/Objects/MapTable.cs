using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTable : MonoBehaviour
{
    [SerializeField] private GameObject MapCanvas;

    public void mapOpened(bool _bool)
    {
        if (_bool)
        {
            MapCanvas.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            MapCanvas.transform.GetChild(0).gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapOpened(true);
        }
    }
}
