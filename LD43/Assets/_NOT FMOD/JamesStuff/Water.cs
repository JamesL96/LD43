using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friendly" || other.gameObject.tag == "Enemy")
            Destroy(other.gameObject, 3);
    }
}
