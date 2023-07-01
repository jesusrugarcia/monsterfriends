using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] music;
    public GameController controller;

    public int current = 0;

    void Start()
    {
        setVolume();
        current = UnityEngine.Random.Range(0, music.Length);
        source.clip = music[current];
        source.Play();
    }

    public void setVolume(){
        source.volume = controller.save.musicVolume;
    }

    void FixedUpdate()
    {
        if(!source.isPlaying){
            current ++;
            if (current >= music.Length){
                current = 0;
            }
            source.clip = music[current];
            source.Play();
        }
    }
}
