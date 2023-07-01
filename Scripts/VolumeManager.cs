using UnityEngine.UI;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public Slider slider;
    public GameController controller;
    public AudioSource source;
    public bool isEffect = false;

    void Start()
    {
        slider.onValueChanged.AddListener(setVolume);
        if(isEffect){
            slider.value = controller.save.effectVolume;
        } else {
            slider.value = controller.save.musicVolume;
        }
    }

    public void setVolume(float volume){
        if(isEffect){
            controller.save.effectVolume = volume;
        } else {
            controller.save.musicVolume = volume;
            source.volume = volume;
        }
        controller.saveSaveFile();
    }
}
