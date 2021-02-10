using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CombineWheelRotation : MonoBehaviour
{
    public int id = 0;
    public static event Action<string, int> Rotated = delegate { };
    private bool coroutineAllowed;
    private int numberShown;
    private int direction = 0;
    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
        numberShown = 1;
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

        for (int i = 0; i < 36; i++)
        {
            transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;
        numberShown += 1;

        if(numberShown > 9)
        {
            numberShown = 0;
        }
        
        Rotated(name, numberShown);
    }
}
