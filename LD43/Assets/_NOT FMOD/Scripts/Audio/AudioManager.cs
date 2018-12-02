using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private static FMOD.Studio.EventInstance ambienceInstance;
    private static FMOD.Studio.EventInstance musicInstance;


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
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.MUSIC);
        PlayAmbience();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            PlayMusic();
        }

        if (Input.GetKey(KeyCode.Keypad0))
        {
            StopMusic();
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            SetMusicParameter(musicInstance, FMODPaths.CREW_NUMBER, 5);
        }

        if (Input.GetKey(KeyCode.Keypad3))
        {
            SetMusicParameter(musicInstance, FMODPaths.CREW_NUMBER, 15);
        }
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

    public void PlayMusic()
    {

        FMOD.Studio.PLAYBACK_STATE _playing;
        musicInstance.getPlaybackState(out _playing);
        if (_playing != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            musicInstance.start();
        }

    }

    public void StopMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }


    void SetMusicParameter(FMOD.Studio.EventInstance musicInstance, string name, int value)
    {
        FMOD.Studio.ParameterInstance parameter;
        musicInstance.getParameter(name, out parameter);

        parameter.setValue(value);
    }

}



