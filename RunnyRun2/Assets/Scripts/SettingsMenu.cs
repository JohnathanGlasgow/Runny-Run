using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


/// <summary>
// This unity class is used to control the settings menu.
// It has options to toggle the post processing effects bloom, lens distortion, and film grain.
// It also has a slider to control the volume of the game.
// It is attached to the SettingsMenu game object.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    public Volume postProcessVolume;

    private Bloom bloom;
    private FilmGrain filmGrain;
    private LensDistortion lensDistortion;

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        // Initialize the post-processing components
        postProcessVolume.profile.TryGet(out bloom);
        postProcessVolume.profile.TryGet(out filmGrain);
        postProcessVolume.profile.TryGet(out lensDistortion);

        // set the slider value to the current sfx level
        sfxSlider.value = SFXManager.Instance.InitSFXLevel;
        // set the slider value to the current music level
        musicSlider.value = MusicManager.Instance.InitVolume;
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