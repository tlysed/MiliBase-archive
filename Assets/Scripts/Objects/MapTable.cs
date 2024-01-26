using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTable : MonoBehaviour
{
    [SerializeField] private GameObject MapCanvas;
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void mapOpen(bool _bool)
    {
        if (_bool)
        {
            MapCanvas.transform.GetChild(0).gameObject.SetActive(true);
            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            MapCanvas.transform.GetChild(0).gameObject.SetActive(false);
            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapOpen(true);
        }
    }
}
