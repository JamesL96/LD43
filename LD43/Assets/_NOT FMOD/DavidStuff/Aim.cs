using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    public GameObject target;
    public GameObject cam;
    private Camera c;
	// Use this for initialization
	void Start () {
        c = cam.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(c.ScreenPointToRay(Input.mousePosition), out hit, 100f, 1 << 9))
        {
            target.transform.position = hit.point;
        }
	}
}
