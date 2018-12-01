using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public enum State {Action, Recovery, Transition}
    [Header("Game Status")]
    public State GameState;

    [Header("Boat Status")]
    public float boatWeight;
    public float maxWeight = 3500;

    public float percentHeavy;


    private Vector3 camTargetPos;

    void Update()
    {
        Camera.main.transform.LookAt(Vector3.zero);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, camTargetPos, Time.fixedDeltaTime);

        if (GameState == State.Action)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
                GameState = State.Recovery;

            camTargetPos = new Vector3(-25, 11, 0);
        }
        if (GameState == State.Recovery)
        {
            foreach (Loot l in FindObjectsOfType<Loot>())
            {
                if (l.gameObject.tag != "Friendly")
                    l.gameObject.tag = "Friendly";
            }

            camTargetPos = new Vector3(0, 11, -20);
        }
        if(GameState == State.Transition)
        {
            //this doesn't do anything yet
            //it should bring in new boat and such
            GameState = State.Action;
        }


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
