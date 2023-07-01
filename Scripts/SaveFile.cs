using System;
using UnityEngine;

[Serializable]
public class SaveFile 
{
    public float money = 0;
    public float totalMoney = 0;
    public int[] upgrades;
    public float[] prices;
    public float[] friendships;
    public int stage = 0;
    public int world = 0;
    public float click = 1;
    public float clickPrice = 20;
    public int clickLevel = 0;

    [Range(0f,1f)]
    public float musicVolume = 0.75f;
    [Range(0f,1f)]
    public float effectVolume = 0.5f;

    public SaveFile (int length){
        upgrades = new int[length];
        prices = new float[length];
        friendships = new float[length];

        for (int i=0; i< length; i++){
            upgrades[i] = 0;
            prices [i] = 0;
            friendships[i] = 0;
        }
    }
}
