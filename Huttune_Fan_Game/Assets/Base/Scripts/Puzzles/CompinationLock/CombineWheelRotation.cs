﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CombineWheelRotation : MonoBehaviour
{
    public int id = 0;
    public static event Action<string, int> Rotated = delegate { };
    private bool coroutineAllowed;
    public int numberShown = 1;
    public AudioSource parentAudiosource;
    public AudioClip soundFX;
    private int direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
        //numberShown = 1;
    }

    public void RotateWheelDirection(int dir)
    {
        if(coroutineAllowed)
        {
            direction = dir;
            StartCoroutine(RotateWheel());
        }
    }

    //private void OnMouseDown()
    //{
    //    if(coroutineAllowed)
    //    {
    //        StartCoroutine(RotateWheel());
    //    }
    //}

    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;
        parentAudiosource.PlayOneShot(soundFX); 
        if (direction > 0)
        {
            for (int i = 0; i < 36; i++)
            {
                transform.Rotate(-1, 0, 0);
               
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (int i = 0; i < 36; i++)
            {
                transform.Rotate(+1, 0, 0);

                yield return new WaitForSeconds(0.05f);
            }
        }

        
        coroutineAllowed = true;

         if (direction > 0)
        {
            numberShown -= 1;
        }
          else
        {
            numberShown += 1;
        }

        if(numberShown > 9)
        {
            numberShown = 0;
        }
        

        if(numberShown < 0)
        {
            numberShown = 9;
        }

        print(name);
        Rotated(name, numberShown);
    }
}
