using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    private GameObject[] targets;
    [HideInInspector]
    public bool attacking;

    public GameObject splashPrefab;
    public Transform gunshotPos;

    private void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 5)
            FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.WILHELM, GetComponent<Transform>().position);

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
        else
        {
            Vector3 prevRot = transform.eulerAngles;
            transform.LookAt(Vector3.zero);
            transform.eulerAngles = new Vector3(prevRot.x, transform.eulerAngles.y, prevRot.z);
        }
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
        GameObject gunShot = Instantiate(splashPrefab, gunshotPos.position, Quaternion.identity);
        gunShot.GetComponent<ParticleSystem>().startColor = Color.black;
        gunShot.transform.localScale = new Vector3(0.1f, 0.1f, 0.2f);
        if(target)
            gunShot.transform.LookAt(target.transform.position + target.transform.forward);
        Destroy(gunShot, 3);

        //call death animation of target
        if (x == 5)
        {
            if (target)
            {
                target.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
                target.GetComponent<StandUp>().enabled = false;
                target.GetComponent<AI>().StopAllCoroutines();
                target.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
                target.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
                target.GetComponent<LineRenderer>().SetPosition(2, Vector3.zero);
                GameObject blood = Instantiate(splashPrefab, target.transform.position, Quaternion.identity);
                blood.GetComponent<ParticleSystem>().startColor = Color.red;
                blood.transform.LookAt(target.transform.position + target.transform.forward);
                Destroy(blood, 3);

                //IF WE WANT LASERS UNCOMMENT
                //StartCoroutine(DrawLaser(true, target.transform.position));

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
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.red, 0.5f);
            //IF WE WANT LASERS UNCOMMENT
            //StartCoroutine(DrawLaser(false, target.transform.position));
        }

        attacking = false;
    }

    IEnumerator DrawLaser(bool hit, Vector3 targetPos)
    {
        if (!hit)
        {
            int r = Random.Range(1, 3);
            Vector3 missOffset = Vector3.zero;
            if (r == 1)
                missOffset = new Vector3(Random.Range(0.5f, 1), Random.Range(1, 2), 0);
            if (r == 2)
                missOffset = new Vector3(Random.Range(-0.5f, -1), Random.Range(1, 2), 0);
            targetPos += (transform.right * missOffset.x) + (transform.up * missOffset.y) + (transform.forward * missOffset.z);
        }

        GetComponent<LineRenderer>().SetPosition(0, gunshotPos.position);
        GetComponent<LineRenderer>().SetPosition(1, targetPos);
        if(hit)
            GetComponent<LineRenderer>().SetPosition(2, targetPos);
        else
            GetComponent<LineRenderer>().SetPosition(2, (targetPos - transform.position) * 100);
        yield return new WaitForSeconds(1);
        GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
        GetComponent<LineRenderer>().SetPosition(2, Vector3.zero);
    }
}
