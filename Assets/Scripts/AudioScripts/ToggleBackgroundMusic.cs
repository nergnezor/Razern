using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleBackgroundMusic : MonoBehaviour {
    public AudioMixer masterMixer;
    public string parameterName = "MasterVolume";
    private bool justMuted = false;
    private bool ptrDown = false;
    
    private void Start () {
        Globals.prevMasterVol = GetBackgroundLevel ();
    }

    public void ToggleMusicPtrUp(){
        ptrDown = false;
    }
    public void ToggleMusicPtrDw(){
        if (ptrDown) return;
        ptrDown = true;
        ToggleMusic();
    }

    private void ToggleMusic() {
        float volume = GetBackgroundLevel ();

        if (volume != Globals.minVol) {
            justMuted = true;
            SetMainMixVolume(Globals.minVol); //BackgroundMusic
        } else {
            SetMainMixVolume(Globals.prevMasterVol);
        }
    }

    private float GetBackgroundLevel () {
        float value;
        bool result = masterMixer.GetFloat (parameterName, out value);
        if (result) {
        return value;
        } else {
            return 0f;
        }
    }

    public void SetSound (float soundLevel) {
        if (justMuted){
            justMuted = false;
        }else{
        var actualSoundLevel = calculatedB(soundLevel);
        SetMainMixVolume(actualSoundLevel);
        Globals.prevMasterVol = actualSoundLevel;
        }
    }

    private void SetMainMixVolume(float val){
        masterMixer.SetFloat (parameterName, val);
    }
    // Max volume is +20 but we limit to 0, otherwise we will introduce distorsion
    private float calculatedB (float val) {
        Debug.Log("Val: " + val + "    LogVal: " + (Mathf.Log10(val) * 20));
        float normalizedValue = Mathf.InverseLerp (0f, 1f, val); // Input from Slider 0 - 1
        return Mathf.Lerp (Globals.minVol, 0f, normalizedValue); // Convert linear to dB
    }

}