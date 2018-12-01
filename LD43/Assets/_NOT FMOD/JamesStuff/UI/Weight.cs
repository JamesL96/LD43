using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weight : MonoBehaviour {
    private GameObject weightUI;

    private void Awake()
    {
        weightUI = GameObject.Find("WeightUI");
    }

    void OnMouseOver ()
    {
        weightUI.GetComponent<RectTransform>().transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, GetComponent<Collider>().bounds.extents.y * 2, 0));
        weightUI.GetComponent<Text>().text = "Weight: " + GetComponent<Rigidbody>().mass * 100;
	}

    private void OnMouseExit()
    {
        weightUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
    }
}
