using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum State {Action, Recovery, Transition, Respawn, Victory}
    [Header("Game Status")]
    public State GameState;
    public GameObject gameOverUI;
    public GameObject gameOverText;
    private bool gameOver;
    public GameObject winUI;

    [Header("Boat Status")]
    public float boatWeight;
    public float maxWeight = 3500;
    public float percentHeavy;
    public GameObject heavyBoatUI;
    public GameObject heavyBoatTime;
    private bool heavyWarning;

    [Header("Wave Manager")]
    public int wave = 0;
    public GameObject enemyShipPrefab;
    private GameObject enemyShip;
    public Vector3 targetEnemyShipPos;

    private Vector3 camTargetPos;

    private void Awake()
    {
        heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
    }

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
                eb.transform.position -= Vector3.up * Time.fixedDeltaTime * 10;
                Destroy(eb, 3);
            }

            camTargetPos = new Vector3(0, 11, -20);

            if (enemyBoats.Length == 0)
            {
                //friendly pirate revive
                GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
                foreach (GameObject f in friendlies)
                {
                    if (f.GetComponent<AI>())
                    {
                        f.GetComponent<AI>().enabled = true;
                        f.GetComponent<StandUp>().enabled = true;
                    }
                }
                if (FindObjectOfType<LootUI>().lootWeight / maxWeight < 1)
                    GameState = State.Respawn;
                else
                    GameState = State.Victory;
            }
        }
        if(GameState == State.Respawn)
        {
            camTargetPos = new Vector3(-10, 11, -20);

            if (!enemyShip)
                enemyShip = Instantiate(enemyShipPrefab, new Vector3(10, 0, -30), Quaternion.identity);
            enemyShip.transform.position = Vector3.MoveTowards(enemyShip.transform.position, targetEnemyShipPos, Time.fixedDeltaTime * 5);
            if (Vector3.Distance(enemyShip.transform.position, targetEnemyShipPos) < 0.1f)
            {
                wave++;
                enemyShip = null;

                GameState = State.Action;
            }
        }
        if(GameState == State.Victory || Input.GetKeyDown(KeyCode.W))
        {
            winUI.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            //fade out I guess;
            //call ienumerator
            //SceneManager.LoadScene(1);
        }


        boatWeight = 0;
        foreach (GameObject bo in FindObjectOfType<Boat>().boatObjs)
            if(bo.tag != "Captain")
                boatWeight += bo.GetComponent<Rigidbody>().mass * 100;

        percentHeavy = boatWeight / maxWeight;

        if (!GameObject.FindGameObjectWithTag("Captain"))
            GameOver("Your captain has died!");

        if (percentHeavy >= 0.999f)
        {
            if (!heavyWarning)
                StartCoroutine(HeavyWarning());
        }
        else
        {
            heavyWarning = false;
        }

        GameObject.Find("BoatWeightSlider").GetComponent<Slider>().value = percentHeavy;

        GameObject.Find("BoatWeightText").GetComponent<Text>().text = "Boat Weight: " + boatWeight + "/" + maxWeight;

        if (gameOver)
            FindObjectOfType<Boat>().gameObject.transform.position -= new Vector3(0, 0.1f, 0);

        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    IEnumerator HeavyWarning()
    {
        heavyWarning = true;
        heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        int t = 10;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.white;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.white;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.white;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.white;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.white;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.white;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.white;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.red;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.red;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        t--;
        heavyBoatTime.GetComponent<Text>().text = t.ToString();
        heavyBoatTime.GetComponent<Text>().color = Color.red;
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.UI_COUNTDOWN, GetComponent<Transform>().position);
        yield return new WaitForSeconds(1);
        if (percentHeavy < 0.999f)
        {
            heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
            yield return new WaitForSeconds(Mathf.Infinity);
        }
        heavyBoatUI.GetComponent<RectTransform>().transform.position = new Vector3(-1000, -1000, -1000);
        GameOver("Your boat got too heavy and you sank!");
    }

    void GameOver(string message)
    {
        gameOver = true;
        gameOverUI.GetComponent<RectTransform>().transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        gameOverText.GetComponent<Text>().text = message;
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
