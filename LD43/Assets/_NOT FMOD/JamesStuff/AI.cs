using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    private GameObject[] targets;
    [HideInInspector]
    public bool attacking;

    public GameObject bloodSplatterPrefab;

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
            if(t != null)
                if(Vector3.Distance(transform.position, t.transform.position) < closeDist && t.GetComponent<AI>())
                {
                    if (t.GetComponent<AI>().enabled == true)
                    {
                        closestTarget = t;
                        closeDist = Vector3.Distance(transform.position, t.transform.position);
                    }
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
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.GUN_FIRE, GetComponent<Transform>().position);

        //call death animation of target;
        if (x == 5)
        {
            if (target != null)
            {
                target.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
                target.GetComponent<StandUp>().enabled = false;
                target.GetComponent<Renderer>().material.color = Color.red;
                target.GetComponent<AI>().StopAllCoroutines();
                GameObject blood = Instantiate(bloodSplatterPrefab, target.transform.position, Quaternion.identity);
                blood.GetComponent<ParticleSystem>().startColor = Color.red;
                blood.transform.LookAt(target.transform.position + target.transform.forward);
                Destroy(blood, 3);

                //DEBUG
                Debug.DrawLine(transform.position, target.transform.position, Color.green, 0.5f);

                if (target.tag == "Friendly")
                    target.GetComponent<AI>().enabled = false;
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
