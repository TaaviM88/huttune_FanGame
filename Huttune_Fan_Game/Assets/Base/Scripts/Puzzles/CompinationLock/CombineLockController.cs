using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CombineLockController : MonoBehaviour, ITogglePuzzle
{
    public static event Action<int, bool> CheckIfPuzzleIsSolve = delegate { };
    public static event Action<int, int[]> UpdateHint = delegate { };
    public List<CombineWheelRotation> wheels = new List<CombineWheelRotation>();
    public int id = 0;
    public int outlineLayer = 15;
    public bool randomizeAnswer = false;
    private int[] results;
    public int[] correctCombination;
    public PuzzleController puzzleController;
    public AudioSource audiosource;
    public AudioClip openSoundFX;
    public bool isSolved {  get; private set; }
    public bool openLock { get; private set;}

    bool isPuzzleOn = false;
    bool canMoveNextWheel = true;
    bool canScrollWheel = true;

    [SerializeField]
    int currentWheelIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform child in transform)
        {
            if(child.GetComponent<CombineWheelRotation>())
            {
                wheels.Add(child.GetComponent<CombineWheelRotation>());
            }

        }
    results = new int[] { 0, 0, 0 };
        RandomizeCorrectAnswer();
        //correctCombination = new int[] { 3, 7, 9 };
        CombineWheelRotation.Rotated += CheckResults;
        isSolved = false;
        openLock = false;
        DisablePuzzle();
    }

    private void Update()
    {
        if (isPuzzleOn)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            ChangeWheel(horizontal);

            ScrollWheel(vertical);

            if (Input.GetKeyDown(KeyCode.E))
            {
                DisablePuzzle();
            }
        }
    }

    private void ScrollWheel(float vertical)
    {
        if (canScrollWheel && vertical != 0)
        {
            wheels[currentWheelIndex].RotateWheelDirection((int)vertical);

            canScrollWheel = false;
        }

        if (vertical == 0)
        {
            canScrollWheel = true;
        }
    }

    private void ChangeWheel(float horizontal)
    {
        wheels[currentWheelIndex].gameObject.layer = 0;
        if (canMoveNextWheel && horizontal != 0)
        {
            if(horizontal > 0)
            {
                currentWheelIndex++;

                if(currentWheelIndex > wheels.Count -1)
                {
                    currentWheelIndex = 0;
                }
            }
            
            if(horizontal < 0)
            {
                currentWheelIndex--;

                if (currentWheelIndex < 0)
                {
                    currentWheelIndex = wheels.Count -1;
                }
            }

            canMoveNextWheel = false;
        }

        if (horizontal == 0)
        {
            canMoveNextWheel = true;
        }

        wheels[currentWheelIndex].gameObject.layer = outlineLayer;
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
            case "LockCylinder1":
                results[0] = number;
                break;
            case "LockCylinder2":
                results[1] = number;
                break;
            case "LockCylinder3":
                results[2] = number;
                break;
            case "LockCylinder4":
                results[3] = number;
                break;
            case "LockCylinder5":
                results[4] = number;
                break;
        }
        if(results[0] == correctCombination[0] && results[1] == correctCombination[1] && results[2] == correctCombination[2])
        {
            Journal.Instance.Log("Door is open");
            audiosource.PlayOneShot(openSoundFX);
            isSolved = true;
            openLock = true;
            CheckIfPuzzleIsSolve(id, openLock);
            puzzleController.PuzzleIsSolved(true);
            Destroy(puzzleController.gameObject,0.2f);
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
            child.gameObject.layer = 0;
            child.GetComponent<CombineWheelRotation>().enabled = false;
        }

        currentWheelIndex = 0;
        canScrollWheel = true;
        canMoveNextWheel = true;
        isPuzzleOn = false;
    }

    public void EnablePuzzle()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<CombineWheelRotation>().enabled = true;
        }

        currentWheelIndex = 0;
        canScrollWheel = true;
        canMoveNextWheel = true;
        isPuzzleOn = true;
    }

    public bool IsPuzzleSolved()
    {
        return isSolved;
    }
}
