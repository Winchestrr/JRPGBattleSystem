using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfxList;

    public void Start()
    {
        sfxList = GetComponentsInChildren<AudioSource>(); 
    }

    public void playSound(string soundName)
    {
        switch(soundName)
        {
            case "sword":
                sfxList[0].Play();
                break;

            case "heal":
                sfxList[1].Play();
                break;

            case "bonk":
                sfxList[2].Play();
                break;

            case "smash":
                sfxList[3].Play();
                break;

            case "click":
                sfxList[4].Play();
                break;
        }
    }
}
