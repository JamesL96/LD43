using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    public GameObject target;
    public GameObject cam;
    private Camera c;
    private GameObject gm;
	// Use this for initialization
	void Start () {
        c = cam.GetComponent<Camera>();
        gm = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {
        if (gm.GetComponent<GameManager>().GameState == GameManager.State.Action)
        {
            RaycastHit hit;
            if (Physics.Raycast(c.ScreenPointToRay(Input.mousePosition), out hit, 100f, 1 << 9))
            {
                target.transform.position = hit.point;
            }
        }
	}
}
