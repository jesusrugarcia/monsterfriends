using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource tap;
    public AudioSource menu;
    public AudioSource close;
    public GameController controller;


    public void playTap(){
        if (!controller.menuOpened){
            tap.volume = controller.save.effectVolume;
            tap.time = 0.3f;
            tap.Play();
        }
    }

    public void playMenu(){
        menu.volume = controller.save.effectVolume;
        menu.time = 0.2f;
        menu.Play();
        
    }

    public void closeMenu(){
        close.volume = controller.save.effectVolume;
        close.time = 0.2f;
        close.Play();
    }
}
