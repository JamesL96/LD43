using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public GameObject target;
    public float speed;
    public GameObject cannonball;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.LookAt(target.transform.position);
        transform.Rotate(Vector3.right * 90f);
        if (Input.GetMouseButtonDown(0))
        {
            fire();
        }
	}

    void fire()
    {
        GameObject temp = Instantiate(cannonball, transform.position + transform.up*2, Quaternion.identity);
        temp.GetComponent<Rigidbody>().velocity = transform.up.normalized * speed;
    }
}
