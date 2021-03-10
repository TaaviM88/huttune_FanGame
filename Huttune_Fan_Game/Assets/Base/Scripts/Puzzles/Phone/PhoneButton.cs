using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneButton : MonoBehaviour
{
    public int buttonNumber = 0;
    public string cassioKey = "";
    public AudioSource parentAudiosource;
    public AudioClip soundFX;
    public int GetNumber()
    {
        //Play sound
        parentAudiosource.PlayOneShot(soundFX);
        return buttonNumber;
    }

}
