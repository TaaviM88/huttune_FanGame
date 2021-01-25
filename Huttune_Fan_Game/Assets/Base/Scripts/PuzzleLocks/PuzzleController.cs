using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour, ITogglePuzzle
{
    [Header("Tähän varsinaisen puzzlen script")]
    public MonoBehaviour puzzleScript;
    bool isPuzzleSolved = false;
    public void DisablePuzzle()
    {
        puzzleScript.GetComponent<ITogglePuzzle>().DisablePuzzle();
    }

    public void EnablePuzzle()
    {
        puzzleScript.GetComponent<ITogglePuzzle>().EnablePuzzle();
    }

    public bool IsPuzzleSolved()
    {
        return isPuzzleSolved;
    }

    public void PuzzleIsSolved(bool result)
    {
        isPuzzleSolved = result;
    }
}
