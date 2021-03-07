using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasioManager : MonoBehaviour, ITogglePuzzle
{
    public List<Transform> buttons = new List<Transform>();
    Animator anime;
    bool puzzleOn = false;
    public int highlightLayer = 15;
    public List<int> correctSequence = new List<int>();
    public List<int> pressedSequence = new List<int>();
    bool isPlayed = false;
    bool canMoveNextButton = true;
    [SerializeField]
    int currentCursorIndex = 1;
    int originalCursorIndex;
    int pNumberIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<PhoneButton>())
            {
                buttons.Add(child);
            }
        }
        originalCursorIndex = currentCursorIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleOn)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");


            if (canMoveNextButton && horizontal != 0)
            {
                ButtonHandler(horizontal);
                canMoveNextButton = false;
            }

            if (horizontal == 0)
            {
                canMoveNextButton = true;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                PressButton();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                DisablePuzzle();
            }
        }
    }

    private void PressButton()
    {
        pressedSequence.Add(buttons[currentCursorIndex].GetComponent<PhoneButton>().GetNumber());

        anime.SetTrigger($"{buttons[currentCursorIndex].GetComponent<PhoneButton>().cassioKey}");
        

 /*       if (currentCursorIndex == 9)
        {
            anime.SetTrigger($"Button_Star");
        }

        if (currentCursorIndex == 10)
        {
            anime.SetTrigger($"Button_{0}");
        }

        if (currentCursorIndex == 11)
        {
            anime.SetTrigger($"Button_Hash");
        }
*/

        CheckIfCorrectSequance();
    }

    private void CheckIfCorrectSequance()
    {
        if (correctSequence.Count != pressedSequence.Count)
        {
            //Check if we have pressed correct number

            if (correctSequence[pNumberIndex] != pressedSequence[pNumberIndex])
            {
                pressedSequence.Clear();
                pNumberIndex = 0;
                Journal.Instance.Log("I play it wrong, let's start over");
                return;
            }
            else
            {
                pNumberIndex++;
            }

            if (pressedSequence.Count > correctSequence.Count)
            {

                pressedSequence.Clear();
                Journal.Instance.Log("I play it wrong, let's start over");
                return;
            }
            else
            {
                return;
            }
        }

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (correctSequence[i] != pressedSequence[i])
            {
                pressedSequence.Clear();
                print("Väärä numero. Aloitetaan alusta");
                return;
            }
        }

        Journal.Instance.Log("You played correct tune! Yeah!");
        PlayerCorrectSequence();
    }


    private void PlayerCorrectSequence()
    {
        isPlayed = true;

        //Do the next thing
    }

    private void ButtonHandler(float index)
    {
        buttons[currentCursorIndex].gameObject.layer = 0;
        if (currentCursorIndex + index < buttons.Count && currentCursorIndex + index >= 0)
        {
            currentCursorIndex += (int)index;
           
            buttons[currentCursorIndex].gameObject.layer = highlightLayer;
        }
        else
        {
            if (currentCursorIndex + index < 0)
            {
                currentCursorIndex = buttons.Count - 1;
             
                buttons[currentCursorIndex].gameObject.layer = highlightLayer;
            }
            else
            {
                currentCursorIndex = 0;
                buttons[currentCursorIndex].gameObject.layer = highlightLayer;
            }
        }
    }

    public void DisablePuzzle()
    {
        if(puzzleOn)
        {
            puzzleOn = false;
            buttons[currentCursorIndex].gameObject.layer = 0;
            pNumberIndex = 0;
            currentCursorIndex = originalCursorIndex;

            for (int i = 0; i < pressedSequence.Count; i++)
            {
                pressedSequence[i] = 0;
            }

            pressedSequence.Clear();
        }
    }

    public void EnablePuzzle()
    {
        if (!puzzleOn)
        {
           // BoolAnimation(true);
            puzzleOn = true;
            buttons[currentCursorIndex].gameObject.layer = highlightLayer;
        }
    }

    public bool IsPuzzleSolved()
    {
        return isPlayed;
    }
}
