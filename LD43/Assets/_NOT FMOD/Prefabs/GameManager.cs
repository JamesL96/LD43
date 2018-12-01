using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float boatWeight;
    private float maxWeight = 3500;

    public float percentHeavy;

	void Update ()
    {
        boatWeight = 0;
        foreach (GameObject bo in FindObjectOfType<Boat>().boatObjs)
            boatWeight += bo.GetComponent<Rigidbody>().mass * 100;

        percentHeavy = boatWeight / maxWeight;
        if (percentHeavy >= 1)
            GameOver();

        FindObjectOfType<Slider>().value = percentHeavy;
	}

    void GameOver()
    {
        print("Game Over");
    }
}
