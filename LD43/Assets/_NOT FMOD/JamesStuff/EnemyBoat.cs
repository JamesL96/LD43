using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoat : MonoBehaviour {
    public GameObject enemyPiratePrefab;
    public GameObject lootPrefab;

    private void Awake()
    {
        int pirateCount = Random.Range(3, 6);
        for(int i = 0; i < pirateCount; i++)
        {
            GameObject ep = Instantiate(enemyPiratePrefab, transform.position + new Vector3(0, 1.5f, i), Quaternion.identity);
            ep.transform.SetParent(gameObject.transform);
        }
    }
}
