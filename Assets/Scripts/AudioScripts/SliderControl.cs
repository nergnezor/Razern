﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour {

        public string parameterName = "MasterVolume";
        public AudioMixer masterMixer;
        public Slider mSlider;
        private bool muteBtnPtrDw = false; // Avoid pointer down fire every frame

    public void SetSLiderPosPtrUp(){
        muteBtnPtrDw = false;
    }
    public void SetSliderPosPtrDw(){
        if (muteBtnPtrDw) return; // Avoid pointer down fire every frame
        muteBtnPtrDw= true;
        SetSLiderPos();
    }
    private void SetSLiderPos(){
        mSlider.value = CalculateFromDB(GetBackgroundLevel());
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

        private float CalculateFromDB (float val) {
        float normalizedValue = Mathf.InverseLerp (Globals.minVol, 0f, val); // Input from Slider 0 - 1
        return Mathf.Lerp (0f, 1f, normalizedValue); // Convert linear to dB
    }
}