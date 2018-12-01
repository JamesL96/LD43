using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
    public float value;

    private void Awake()
    {
        value = Random.Range(100, 1000);
        GetComponent<Rigidbody>().mass = value / 100;
    }

}
