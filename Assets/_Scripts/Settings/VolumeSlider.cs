using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    [SerializeField] AudioMixer mixer;
    [SerializeField] string paramName;
    
    public void Start(){
        float startingVol;
        mixer.GetFloat(paramName, out startingVol);
        GetComponentInChildren<Slider>().value = Mathf.Pow(10, startingVol/ 20);


    }

    public void SetVolume(float sliderValue) => mixer.SetFloat(paramName, Mathf.Log10(sliderValue) * 20);
}