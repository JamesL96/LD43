using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
        explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if(!(other.gameObject.layer == LayerMask.GetMask("Aiming")))
        {
            explode();
        }
    }

    void explode()
    {
        Destroy(gameObject);
    }
}
