using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CombineLockController : MonoBehaviour, ITogglePuzzle
{
    public static event Action<int, bool> CheckIfPuzzleIsSolve = delegate { };
    public static event Action<int, int[]> UpdateHint = delegate { };
    public int id = 0;
    public bool randomizeAnswer = false;
    private int[] results;
    public int[] correctCombination;
    public bool isSolved {  get; private set; }
    public bool openLock { get; private set;}
    // Start is called before the first frame update
    void Awake()
    {

    results = new int[] { 0, 0, 0 };
        RandomizeCorrectAnswer();
        //correctCombination = new int[] { 3, 7, 9 };
        CombineWheelRotation.Rotated += CheckResults;
        isSolved = false;
        openLock = false;
        DisablePuzzle();
    }

    private void RandomizeCorrectAnswer()
    {
        if(randomizeAnswer)
        {
            for (int i = 0; i < correctCombination.Length; i++)
            {
                correctCombination[i] = UnityEngine.Random.Range(0, 9);
            }
        }


        UpdateHint(id, correctCombination);
    }

    private void CheckResults(string wheelName, int number)
    {
        if(isSolved)
        {
            return;
        }

        switch(wheelName)
        {
            case "Wheel1":
                results[0] = number;
                break;
            case "Wheel2":
                results[1] = number;
                break;
            case "Wheel3":
                results[2] = number;
                break;
        }
        if(results[0] == correctCombination[0] && results[1] == correctCombination[1] && results[2] == correctCombination[2])
        { 
            Debug.Log("Auki");
            isSolved = true;
            openLock = true;
            CheckIfPuzzleIsSolve(id, openLock);
            Destroy(gameObject);
        }
        else
        {
            isSolved = false;
            openLock = false;
        }
    }

    private void OnDestroy()
    {
        CombineWheelRotation.Rotated -= CheckResults;
    }

    public void DisablePuzzle()
    {
        
        foreach (Transform child in transform)
        {
            child.GetComponent<CombineWheelRotation>().enabled = false;
        }
    }

    public void EnablePuzzle()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<CombineWheelRotation>().enabled = true;
        }
    }

    public bool IsPuzzleSolved()
    {
        return isSolved;
    }
}
