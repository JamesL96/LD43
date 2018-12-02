using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonUI : MonoBehaviour {
    public GameObject targetCannon;

	void Update ()
    {
        if (targetCannon == null)
            Destroy(gameObject);

        if (targetCannon.GetComponent<Cannon>().cannonReady < 5)
        {
            GetComponent<RectTransform>().transform.position = Camera.main.WorldToScreenPoint(targetCannon.transform.position + Vector3.up);
            float percentReady = targetCannon.GetComponent<Cannon>().cannonReady / 5;
            print(percentReady);
            GetComponent<Slider>().value = percentReady;
        }
        else
            GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
	}
}
