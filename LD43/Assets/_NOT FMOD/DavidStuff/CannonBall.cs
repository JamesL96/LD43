using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        explode();
    }

    void explode()
    {
        Destroy(gameObject);
    }
}
