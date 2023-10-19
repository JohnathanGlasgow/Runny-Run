// this unity class is used to control the settings menu
// it has options to toggle the post processing effects bloom, lens distortion, and film grain
// it also has a slider to control the volume of the game
// it is attached to the SettingsMenu game object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class SettingsMenu : MonoBehaviour
{
    // the post processing volume
    public Volume postProcessVolume;

    private Bloom bloom; // Reference to the Bloom effect
    private FilmGrain filmGrain; // Reference to the Film Grain effect

    // ref to lens distortion
    private LensDistortion lensDistortion;



    private void Start()
    {
        // Initialize the post-processing components
        postProcessVolume.profile.TryGet(out bloom);
        postProcessVolume.profile.TryGet(out filmGrain);
        postProcessVolume.profile.TryGet(out lensDistortion);
    }

    public void BloomToggle(Toggle t) {
        bloom.active = t.isOn;
    }

    public void GrainToggle(Toggle t) {
        filmGrain.active = t.isOn;
    }

    public void DistortionToggle(Toggle t) {
        lensDistortion.active = t.isOn;
    }
}