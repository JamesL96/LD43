using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    public GameObject splashPrefab;
    public Color waterSplashColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friendly" || other.gameObject.tag == "Enemy")
            Destroy(other.gameObject, 3);

        GameObject waterSplash = Instantiate(splashPrefab, other.transform.position - new Vector3(0, other.GetComponent<Collider>().bounds.extents.y * 2, 0), Quaternion.identity);
        waterSplash.GetComponent<ParticleSystem>().startColor = waterSplashColor;
        waterSplash.transform.LookAt(waterSplash.transform.position + Vector3.up);
        Destroy(waterSplash, 3);
    }
}
