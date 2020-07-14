using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleBackgroundMusic : MonoBehaviour {
    public AudioMixer masterMixer;
    public string parameterName = "MasterVolume";
    
    

    private void Start () {
        Globals.prevMasterVol = GetBackgroundLevel ();
    }
    public void ToggleMusic () {
        Debug.Log ("VOL");
        float volume = GetBackgroundLevel ();

        if (volume != Globals.minVol) {
            masterMixer.SetFloat (parameterName, Globals.minVol); //BackgroundMusic
        } else {
            masterMixer.SetFloat (parameterName, Globals.prevMasterVol);
        }
    }

    private float GetBackgroundLevel () {
        float value;
        bool result = masterMixer.GetFloat (parameterName, out value);
        Debug.Log ("Result: " + result + "Value: " + value);
        if (result) {
        return value;
        } else {
            return 0f;
        }
    }

    public void SetSound (float soundLevel) {
        var actualSoundLevel = calculatedB(soundLevel);
        masterMixer.SetFloat (parameterName, actualSoundLevel);
        Globals.prevMasterVol = soundLevel;
    }

    // Max volume is +20 but we limit to 0, otherwise we will introduce distorsion
    private float calculatedB (float val) {
        float normalizedValue = Mathf.InverseLerp (0f, 1f, val); // Input from Slider 0 - 1
        return Mathf.Lerp (Globals.minVol, 0f, normalizedValue); // Convert linear to dB
    }

}