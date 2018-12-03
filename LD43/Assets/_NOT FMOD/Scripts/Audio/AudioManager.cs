using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    private FMOD.Studio.EventInstance ambienceInstance;
    public FMOD.Studio.EventInstance musicInstance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;

        }
    }

    void Start()
    {
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.AMBIENCE);
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.MUSIC);
        PlayMusic();
        PlayAmbience();     
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayMusic();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopMusic();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetMusicParameter(musicInstance, FMODPaths.CREW_NUMBER, 5);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetMusicParameter(musicInstance, FMODPaths.CREW_NUMBER, 15);
        }
        */
    }

    public void PlayAmbience()
    {  
        FMOD.Studio.PLAYBACK_STATE _playingAmbience;
        ambienceInstance.getPlaybackState(out _playingAmbience);
        if (_playingAmbience != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            ambienceInstance.start();
        }

    }

    public void PlayMusic()
    {
        FMOD.Studio.PLAYBACK_STATE _playingMusic;
        musicInstance.getPlaybackState(out _playingMusic);
        if (_playingMusic != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
        //    print(FMOD.Studio.PLAYBACK_STATE.PLAYING);
            musicInstance.start();
        }

    }

    public void StopMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       // musicInstance.release();
    }


   public void SetMusicParameter(FMOD.Studio.EventInstance musicInstance, string name, int value)
    {
        FMOD.Studio.ParameterInstance parameter;
        musicInstance.getParameter(name, out parameter);

        parameter.setValue(value);
    }
                
}



