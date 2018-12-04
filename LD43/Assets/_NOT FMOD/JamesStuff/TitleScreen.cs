using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    int x = 0;

	void Update () {
        if (Input.anyKeyDown)
            x++;

        if (x == 1)
            Camera.main.transform.position = new Vector3(50, 1, -10);

        if (x == 2)
            SceneManager.LoadScene(1);
	}
}
