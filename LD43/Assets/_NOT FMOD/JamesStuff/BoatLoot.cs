using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLoot : MonoBehaviour {
    public List<GameObject> loot;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<Loot>() && !loot.Contains(collision.gameObject))
            loot.Add(collision.gameObject);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (loot.Contains(collision.gameObject))
            loot.Remove(collision.gameObject);
    }
}
