using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    private GameObject[] targets;
    private bool attacking;

    private void Update()
    {
        if (gameObject.tag == "Friendly")
            targets = GameObject.FindGameObjectsWithTag("Enemy");
        if (gameObject.tag == "Enemy")
            targets = GameObject.FindGameObjectsWithTag("Friendly");

        float closeDist = Mathf.Infinity;
        GameObject closestTarget = null;
        foreach(GameObject t in targets)
        {
            if(Vector3.Distance(transform.position, t.transform.position) < closeDist)
            {
                closestTarget = t;
                closeDist = Vector3.Distance(transform.position, t.transform.position);
            }
        }

        if (closestTarget != null && !GetComponent<StandUp>().standingUp)
            Attack(closestTarget);
    }

    void Attack(GameObject target)
    {
        Vector3 prevRot = transform.eulerAngles;
        transform.LookAt(target.transform.position);
        transform.eulerAngles = new Vector3(prevRot.x, transform.eulerAngles.y, prevRot.z);

        if (!attacking)
            StartCoroutine(AttackSequence(target));
    }

    IEnumerator AttackSequence(GameObject target)
    {
        attacking = true;

        yield return new WaitForSeconds(Random.Range(3f, 5f));

        //fire animation
        int x = Random.Range(1, 10);

        //call death animation of target;
        if (x == 5)
        {
            if (target != null)
            {
                target.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
                target.GetComponent<StandUp>().enabled = false;
                target.GetComponent<Renderer>().material.color = Color.red;
                target.GetComponent<AI>().StopAllCoroutines();
                //DEBUG
                Debug.DrawLine(transform.position, target.transform.position, Color.green, 0.5f);

                if (target.tag == "Friendly")
                    Destroy(target.GetComponent<AI>());
                else
                    Destroy(target, 3);
            }
        }
        //DEBUG
        else if (target != null)
            Debug.DrawLine(transform.position, target.transform.position, Color.red, 0.5f);

        attacking = false;
    }
}
