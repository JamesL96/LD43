using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    public float value;
    public bool randomize;

    private void Awake()
    {
        Randomize();
    }

    private void Update()
    {
        if (randomize)
        {
            Randomize();
            randomize = false;
        }
    }

    void Randomize()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        value = Random.Range(100, 1000);
        GetComponent<Rigidbody>().mass = value / 100;
    }
}
