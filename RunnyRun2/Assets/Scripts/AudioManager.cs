using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioData
{
    public string Name;
    public AudioClip Clip;
    [Range(0f,1f)] public float InitVolume;
    [Range(0.1f,3f)] public float Pitch;
}

public class AudioUnit
{
    public AudioSource Player;
    public float InitVolume;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float initSFXLevel = 0.5f;
    #region Singleton
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    [SerializeField] private List<AudioData> audioData;
    //private List<AudioSource> players;
    private Dictionary<string, AudioUnit> audioDictionary;

    private void Start()
    {
        // foreach audioData in audioData
        // add an audiosource to AudioManagers gameobject
        // set the clip to the audioData clip as well as the Initvolume and pitch
        // add the audiosource to the dictionary with the name as the key
        //players = new List<AudioSource>();
        audioDictionary = new Dictionary<string, AudioUnit>();
        foreach (AudioData data in audioData)
        {
            AudioSource player = gameObject.AddComponent<AudioSource>();
            player.clip = data.Clip;
            player.volume = data.InitVolume;
            player.pitch = data.Pitch;
            audioDictionary.Add(data.Name, new AudioUnit { Player = player, InitVolume = data.InitVolume });
        }
    }

    public void Play(string name)
    {
        // find the audiosource with the name
        // play the audiosource
        audioDictionary[name].Player.Play();
    }

    public void ChangeSFXVolume(float volume)
    {
        foreach (AudioUnit unit in audioDictionary.Values)
        {
            unit.Player.volume = (unit.InitVolume * volume) / initSFXLevel;
        }
    }



}
