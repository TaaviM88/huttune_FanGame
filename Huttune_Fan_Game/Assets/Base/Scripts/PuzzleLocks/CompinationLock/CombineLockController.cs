using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineLockController : MonoBehaviour
{
    private int[] results, correctCombination;

    // Start is called before the first frame update
    void Start()
    {
        results = new int[] { 0, 0, 0 };
        correctCombination = new int[] { 3, 7, 9 };
        CombineWheelRotation.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch(wheelName)
        {
            case "Wheel1":
                results[0] = number;
                break;
            case "Wheel2":
                results[1] = number;
                break;
            case "Wheel4":
                results[2] = number;
                break;
        }
        if(results[0] == correctCombination[0])
        { 
            Debug.Log("Auki");
        }
    }

    private void OnDestroy()
    {
        CombineWheelRotation.Rotated -= CheckResults;
    }
}
