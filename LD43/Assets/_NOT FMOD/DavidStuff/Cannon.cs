using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public GameObject target;
    public float speed;
    public GameObject cannonball;
    public GameObject gm;

    public bool activeCannon;
    public float cannonReady;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager");
        cannonReady = 5;
	}
	
	void Update () {
        //check to make sure there's an active cannon
        bool anyActiveCannons = false;
        foreach(Cannon c in FindObjectsOfType<Cannon>())
        {
            if (c.activeCannon)
                anyActiveCannons = true;
        }
        if (!anyActiveCannons)
        {
            print("No cannons active");
            GameObject nextCannon = null;
            float maxZPos = -Mathf.Infinity;
            foreach (Cannon c in FindObjectsOfType<Cannon>())
            {
                if (c.transform.position.z > maxZPos)
                {
                    maxZPos = c.transform.position.z;
                    nextCannon = c.gameObject;
                }
            }
            nextCannon.GetComponent<Cannon>().activeCannon = true;
        }

        if(cannonReady < 5)
            cannonReady += Time.fixedDeltaTime;

        if (gm.GetComponent<GameManager>().GameState == GameManager.State.Action && activeCannon)
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit))
                if (Vector3.Distance(hit.point, transform.position) < 50 && hit.point.x > transform.position.x + 5)
                {
                    transform.LookAt(new Vector3(hit.point.x, 3, hit.point.z));
                    if (Input.GetMouseButtonDown(0) && cannonReady >= 5)
                    {
                        StartCoroutine(Fire());
                    }
                }
        }
	}

    IEnumerator Fire()
    {
        GameObject temp = Instantiate(cannonball, transform.position + transform.forward * 4, Quaternion.identity);
        temp.GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.CANNON_FIRE, GetComponent<Transform>().position);
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.PIRATES_YELL, GetComponent<Transform>().position);
        cannonReady = 0;

        yield return new WaitForSeconds(0.1f);
        DetermineNextCannon();
    }

    void DetermineNextCannon()
    {
        float minDist = Mathf.Infinity;
        GameObject nextCannon = null;
        foreach (Cannon c in FindObjectsOfType<Cannon>())
        {
            if (c.transform.position.z < transform.position.z && Vector3.Distance(c.transform.position, transform.position) < minDist)
            {
                minDist = Vector3.Distance(c.transform.position, transform.position);
                nextCannon = c.gameObject;
            }
        }
        if (nextCannon == null)
        {
            float maxZPos = -Mathf.Infinity;
            foreach (Cannon c in FindObjectsOfType<Cannon>())
            {
                if (c.transform.position.z > maxZPos)
                {
                    maxZPos = c.transform.position.z;
                    nextCannon = c.gameObject;
                }
            }
        }

        activeCannon = false;
        nextCannon.GetComponent<Cannon>().activeCannon = true;
    }
}
