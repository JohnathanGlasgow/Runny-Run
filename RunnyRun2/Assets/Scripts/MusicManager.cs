using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region Singleton
    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public float InitVolume = 0.15f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = InitVolume;
    }

        public void skipToEnd(){
                    // Calculate the time 3 seconds before the end
            float skipTime = Mathf.Max(0, audioSource.clip.length - 3f);
            
            // Set the audio source time to the calculated skip time
            audioSource.time = skipTime;
            
            // Play the audio from the new position
            audioSource.Play();
    }
}
