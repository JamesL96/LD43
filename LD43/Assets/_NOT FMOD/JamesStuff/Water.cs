using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    public GameObject splashPrefab;
    public Color waterSplashColor;
    private Mesh mesh;
    private Vector3[] verts;

    private void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friendly" || other.gameObject.tag == "Enemy")
        {
            other.gameObject.tag = "Untagged";
            Destroy(other.gameObject, 3);
        }

        GameObject waterSplash = Instantiate(splashPrefab, other.transform.position - new Vector3(0, other.GetComponent<Collider>().bounds.extents.y * 2, 0), Quaternion.identity);
        waterSplash.GetComponent<ParticleSystem>().startColor = waterSplashColor;
        waterSplash.transform.LookAt(waterSplash.transform.position + Vector3.up);
        Destroy(waterSplash, 3);
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.WATER_SPLASH, GetComponent<Transform>().position);
    }

    private void Update()
    {
        /*
        Vector3[] newverts = verts;
        for(int i = 0; i < verts.Length; i++)
        {
            newverts[i] = newverts[i] + Vector3.up * Mathf.PerlinNoise(newverts[i].x+Time.time, newverts[i].z+Time.time);
        }
        mesh.SetVertices(new List<Vector3>(newverts));
        */
    }
}
