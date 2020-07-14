using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour {

        public string parameterName = "MasterVolume";
        public AudioMixer masterMixer;
        public Slider mSlider;

    public void setSLiderPos(){
        //Debug.Log("Val from Mixer: " + GetBackgroundLevel());
        mSlider.value = CalculateFromDB(GetBackgroundLevel());

    }

    private float GetBackgroundLevel () {
        float value;
        bool result = masterMixer.GetFloat (parameterName, out value);
        Debug.Log ("Result SLider: " + result + "Value Slider: " + value);
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
