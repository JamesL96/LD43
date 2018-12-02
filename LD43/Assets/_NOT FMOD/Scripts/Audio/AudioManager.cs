using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private static FMOD.Studio.EventInstance ambienceInstance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        DontDestroyOnLoad(instance);
    }



    void Start()
    {
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.AMBIENCE);
        PlayAmbience();
    }



    public void PlayAmbience()
    {

        FMOD.Studio.PLAYBACK_STATE _playing;
        ambienceInstance.getPlaybackState(out _playing);
        if (_playing != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            ambienceInstance.start();
        }

    }


}
