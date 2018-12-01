using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
    public List<GameObject> boatObjs;

    private void Update()
    {
        foreach (GameObject bo in boatObjs)
            if (bo == null)
                boatObjs.Remove(bo);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!boatObjs.Contains(collision.gameObject))
            boatObjs.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (boatObjs.Contains(collision.gameObject))
            boatObjs.Remove(collision.gameObject);
    }
}
