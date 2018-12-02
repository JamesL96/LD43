using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public GameObject target;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().isKinematic = true;
        float dx = Vector2.Distance(new Vector2(transform.position.x,transform.position.z), new Vector2(target.transform.position.x,target.transform.position.z));
        float Vx = Vector3.ProjectOnPlane(transform.forward.normalized, Vector3.up).magnitude*speed;
        float d = .5f * Physics.gravity.magnitude * Mathf.Pow(dx / Vx, 2);
        transform.LookAt(target.transform.position + Vector3.up*d);
        transform.Rotate(Vector3.left * 90f);
	}
}
