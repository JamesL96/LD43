using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public enum State {Action, Recovery, Transition, Respawn}
    [Header("Game Status")]
    public State GameState;

    [Header("Boat Status")]
    public float boatWeight;
    public float maxWeight = 3500;

    public float percentHeavy;

    [Header("Wave Manager")]
    public int wave = 0;
    public GameObject enemyShipPrefab;
    private GameObject enemyShip;
    public Vector3 targetEnemyShipPos;

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

            if (FindObjectsOfType<Loot>().Length == FindObjectOfType<BoatLoot>().loot.ToArray().Length)
                GameState = State.Transition;

            camTargetPos = new Vector3(0, 11, -20);
        }
        if(GameState == State.Transition)
        {
            //this doesn't do anything yet
            //it should bring in new boat and such

            GameObject[] enemyBoats = GameObject.FindGameObjectsWithTag("EnemyBoat");
            foreach(GameObject eb in enemyBoats)
            {
                eb.transform.position -= Vector3.up * Time.fixedDeltaTime;
                Destroy(eb, 3);
            }

            camTargetPos = new Vector3(0, 11, -20);

            if (enemyBoats.Length == 0)
                GameState = State.Respawn;
        }
        if(GameState == State.Respawn)
        {
            //friendly pirate revive
            GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
            foreach (GameObject f in friendlies)
            {
                if (f.GetComponent<AI>())
                {
                    f.GetComponent<AI>().enabled = true;
                    f.GetComponent<StandUp>().enabled = true;
                    f.GetComponent<Renderer>().material.color = Color.white;
                }
            }

            camTargetPos = new Vector3(-10, 11, -20);

            if (!enemyShip)
                enemyShip = Instantiate(enemyShipPrefab, new Vector3(10, 0, -50), Quaternion.identity);
            enemyShip.transform.position = Vector3.MoveTowards(enemyShip.transform.position, targetEnemyShipPos, Time.fixedDeltaTime * 5);
            if (Vector3.Distance(enemyShip.transform.position, targetEnemyShipPos) < 0.1f)
            {
                wave++;
                enemyShip = null;
                GameState = State.Action;
            }
        }


            boatWeight = 0;
            foreach (GameObject bo in FindObjectOfType<Boat>().boatObjs)
                boatWeight += bo.GetComponent<Rigidbody>().mass * 100;

            percentHeavy = boatWeight / maxWeight;

            if (percentHeavy >= 0.999f)
                GameOver();

            FindObjectOfType<Slider>().value = percentHeavy;

        GameObject.Find("BoatWeightText").GetComponent<Text>().text = "Boat Weight: " + boatWeight + "/" + maxWeight;
    }

    void GameOver()
    {
        print("Game Over");
    }
}
