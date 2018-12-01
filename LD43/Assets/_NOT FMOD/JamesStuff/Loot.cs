using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    public Vector2 valueRange;
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
        value = Mathf.RoundToInt(Random.Range(valueRange.x, valueRange.y));
        GetComponent<Rigidbody>().mass = value / 100;
        gameObject.name = "Loot(" + value + ")";
    }
}
