using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootUI : MonoBehaviour {
    public float lootWeight;
    public List<GameObject> boatLoot; 

	void Update ()
    {
        boatLoot.Clear();
        foreach (GameObject bo in FindObjectOfType<Boat>().boatObjs)
            if (bo.GetComponent<Loot>() && !boatLoot.Contains(bo))
                boatLoot.Add(bo);

        lootWeight = 0;
        foreach (GameObject bl in boatLoot)
            lootWeight += bl.GetComponent<Rigidbody>().mass * 100;

        GetComponent<Image>().fillAmount = lootWeight / FindObjectOfType<GameManager>().maxWeight;
        GameObject.Find("LootTargetText").GetComponent<Text>().text = "Loot: " + Mathf.RoundToInt((lootWeight / FindObjectOfType<GameManager>().maxWeight) * 100) + "%";

    }
}
