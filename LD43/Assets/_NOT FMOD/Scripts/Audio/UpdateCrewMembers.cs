using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCrewMembers : MonoBehaviour {

    private int crewMembers;
    private GameObject[] targets;
    private AudioManager audioManager;
    

    // Use this for initialization
    void Start () {
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {

        targets = GameObject.FindGameObjectsWithTag("Friendly");
        crewMembers = targets.Length;
        audioManager.SetMusicParameter(audioManager.musicInstance, FMODPaths.CREW_NUMBER, crewMembers);
    }

 


}
