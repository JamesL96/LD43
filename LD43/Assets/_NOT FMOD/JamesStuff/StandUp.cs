using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandUp : MonoBehaviour {
    public bool standingUp;

    public Vector3 targetPos;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void FixedUpdate()
    {
        targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);

        if (Mathf.Abs(transform.localEulerAngles.x) > 1 || Mathf.Abs(transform.localEulerAngles.z) > 1)
            standingUp = true;
        else
            standingUp = false;

        if (standingUp)
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0.0F, transform.rotation.y, 0.0F, transform.rotation.w), Time.fixedDeltaTime * 10);
        else if (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * 3);
        }
    }

}
