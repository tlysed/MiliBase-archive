using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highwayLogic : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform centerHighway;

    [SerializeField] private GameObject enemyCar;
    [SerializeField] private GameObject blockOfCenter;

    private void Start()
    {
        StartCoroutine(spawnCenter());
        StartCoroutine(spawnEnemyCar());
    }
    IEnumerator spawnCenter()
    {
        Destroy(Instantiate(blockOfCenter, centerHighway), 3f);
        yield return new WaitForSeconds(4 - CarInfo.carSpeed);
        StartCoroutine(spawnCenter());
    }
    IEnumerator spawnEnemyCar()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyCar, spawnPoints[rand]);
        yield return new WaitForSeconds((4 - CarInfo.carSpeed)/2);
        StartCoroutine(spawnEnemyCar());
    }
}
