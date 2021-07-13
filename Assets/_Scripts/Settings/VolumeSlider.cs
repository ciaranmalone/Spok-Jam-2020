using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour {
    [SerializeField] AudioMixer mixer;
    [SerializeField] string paramName;
    
    public void SetVolume(float sliderValue) => mixer.SetFloat(paramName, Mathf.Log10(sliderValue) * 20);
}