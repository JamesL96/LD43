using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public GameObject target;
    public float speed;
    public GameObject cannonball;
    public GameObject gm;
	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {

        if (gm.GetComponent<GameManager>().GameState == GameManager.State.Action)
        {
            transform.LookAt(target.transform.position);
            transform.Rotate(Vector3.right * 90f);
            if (Input.GetMouseButtonDown(0))
            {
                fire();
            }
        }
	}

    void fire()
    {
        GameObject temp = Instantiate(cannonball, transform.position + transform.up*2, Quaternion.identity);
        temp.GetComponent<Rigidbody>().velocity = transform.up.normalized * speed;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.CANNON_FIRE, GetComponent<Transform>().position);
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.PIRATES_YELL, GetComponent<Transform>().position);
    }
}
