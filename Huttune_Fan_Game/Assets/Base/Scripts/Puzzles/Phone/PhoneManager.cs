using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour, ITogglePuzzle
{
    public Transform pointer;
    public List<Transform> buttons = new List<Transform>();
    public WashingMachineManager wmManager;
    Animator anime;
    bool answerOn = false;
    public int selectedHightlightLayer = 15;
    public List<int> correctSequence = new List<int>();
    public List<int> pressedSequance = new List<int>();
    //public int[] correctSequence = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
    //public int[] pressedSequance;
    bool isCalled = false;
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
        if(answerOn)
        {
            //telephone number valinta systeemi
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if(canMoveNextButton && horizontal != 0)
            {
                ButtonHandler(horizontal);
                canMoveNextButton = false;
            }

            if(horizontal == 0)
            {
                canMoveNextButton = true;
            }

            if(Input.GetButtonDown("Fire1"))
            {
                PressButton();
            }

            if(Input.GetKeyDown(KeyCode.E))
            {
                DisablePuzzle();
            }
        }
    }

    private void PressButton()
    {
       pressedSequance.Add(buttons[currentCursorIndex].GetComponent<PhoneButton>().GetNumber());
        if(currentCursorIndex < 9)
        {
            anime.SetTrigger($"Button_{currentCursorIndex + 1}");
        }
       
        if(currentCursorIndex == 9)
        {
            anime.SetTrigger($"Button_Star");
        }

        if(currentCursorIndex == 10)
        {
            anime.SetTrigger($"Button_{0}");
        }

        if(currentCursorIndex == 11)
        {
            anime.SetTrigger($"Button_Hash");
        }


       CheckIfCorrectSequance();
    }


    private void CheckIfCorrectSequance()
    {
        
        if (correctSequence.Count != pressedSequance.Count)
        {
            //Check if we have pressed correct number

            if(correctSequence[pNumberIndex] != pressedSequance[pNumberIndex])
            {
                pressedSequance.Clear();
                pNumberIndex = 0;
                print("Väärä numero. Aloitetaan alusta");
                return;
            }
            else
            {
                pNumberIndex++;
            }

            if (pressedSequance.Count > correctSequence.Count)
            {

                pressedSequance.Clear();
                print("Väärä numero. Aloitetaan alusta");
                return;
            }
            else
            {
                return;
            }
           
        }

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (correctSequence[i] != pressedSequance[i])
            {
                pressedSequance.Clear();
                print("Väärä numero. Aloitetaan alusta");
                return;
            }
        }

        print("Oikea numero");
        CallCorrectNumber();
    }

    private void CallCorrectNumber()
    {
        //Play voice and sound
        isCalled = true;
        if(!wmManager.GetIsCalled())
        {
            wmManager.SetIsCalled(isCalled);
        }
    }

    private void ButtonHandler(float index)
    {
        buttons[currentCursorIndex].gameObject.layer = 0;

        if (currentCursorIndex +index < buttons.Count && currentCursorIndex + index >= 0)
        {
            currentCursorIndex += (int)index;
            MovePointer(buttons[currentCursorIndex].GetChild(0).localPosition);
            buttons[currentCursorIndex].gameObject.layer = selectedHightlightLayer;
        }
        else
        {
            if(currentCursorIndex + index < 0)
            {
                currentCursorIndex = buttons.Count -1;
                MovePointer(buttons[currentCursorIndex].GetChild(0).localPosition);
                buttons[currentCursorIndex].gameObject.layer = selectedHightlightLayer;
            }
            else
            {
                currentCursorIndex = 0;
                MovePointer(buttons[currentCursorIndex].GetChild(0).localPosition);
                buttons[currentCursorIndex].gameObject.layer = selectedHightlightLayer;
            }
            
        }

    }


    private void MovePointer(Vector3 buttonTransform)
    {
        Vector3 newPosition = buttonTransform;
        pointer.transform.localPosition = new Vector3(newPosition.x, newPosition.y, pointer.transform.localPosition.z);
    }

    public void TriggerAnimation(string trigger)
    {
        anime.SetTrigger(trigger);
    }

    public void BoolAnimation(bool b)
    {
        anime.SetBool("Answer_On",b);
    }

    public void DisablePuzzle()
    {
        if (answerOn)
        {
            BoolAnimation(false);
            answerOn = false;
            buttons[currentCursorIndex].gameObject.layer = 0;
            pNumberIndex = 0;
            currentCursorIndex = originalCursorIndex;

            for (int i = 0; i <pressedSequance.Count; i++)
            {
                pressedSequance[i] = 0;
            }

            pressedSequance.Clear();
        }
    }

    public void EnablePuzzle()
    {
        if (!answerOn)
        {
            BoolAnimation(true);
            answerOn = true;
            buttons[currentCursorIndex].gameObject.layer = selectedHightlightLayer;
        }
    }

    public bool IsPuzzleSolved()
    {
        return isCalled;
    }
}
