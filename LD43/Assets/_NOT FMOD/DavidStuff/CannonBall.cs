using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
        explode(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if(!(other.gameObject.layer == LayerMask.GetMask("Aiming")))
        {
            explode(false);
        }
    }

    void explode(bool blowup)
    {
        if (blowup)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 5);
            foreach(Collider c in cols)
            {
                if (c.gameObject.GetComponent<Rigidbody>())
                {
                    c.gameObject.GetComponent<Rigidbody>().AddExplosionForce(5, gameObject.transform.position, 5);
                    FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.CANNON_EXPLOSION, GetComponent<Transform>().position);
                }
            }
        }
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.CANNON_COLLIDE, GetComponent<Transform>().position);
        Destroy(gameObject);
    }
}
