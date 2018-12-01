using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUp : MonoBehaviour {
    [HideInInspector]
    public bool standingUp;

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.localEulerAngles.x) > 1 || Mathf.Abs(transform.localEulerAngles.z) > 1)
            standingUp = true;
        else
            standingUp = false;

        if (standingUp)
            transform.localRotation = Quaternion.Slerp(transform.localRotation, new Quaternion(0.0F, transform.rotation.y, 0.0F, transform.rotation.w), Time.fixedDeltaTime * 10);
    }

}
