using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoat : MonoBehaviour {
    public GameObject enemyPiratePrefab;
    public GameObject[] lootPrefabs;
    private GameObject loot;

    private Vector3 prevPos;

    private void Awake()
    {
        int pirateCount = Random.Range(5, 10);
        for(int i = 0; i < pirateCount; i++)
        {
            GameObject ep = Instantiate(enemyPiratePrefab, transform.position + new Vector3(-1, 1.5f, (-pirateCount + 1) + (i * 2)), Quaternion.identity);
        }

        GameObject randLoot = lootPrefabs[Random.Range(0, lootPrefabs.Length)];
        loot = Instantiate(randLoot, transform.position + new Vector3(2, 1.5f, Random.Range(-pirateCount, pirateCount)), randLoot.transform.rotation);

        prevPos = transform.position;
    }

    private void Update()
    {
        if(transform.position != prevPos)
        {
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                if (e.GetComponent<AI>())
                {
                    e.transform.position += transform.position - prevPos;
                    e.GetComponent<StandUp>().targetPos += transform.position - prevPos;
                }
            if(loot != null)
                loot.transform.position += transform.position - prevPos;
            prevPos = transform.position;
        }
    }
}
