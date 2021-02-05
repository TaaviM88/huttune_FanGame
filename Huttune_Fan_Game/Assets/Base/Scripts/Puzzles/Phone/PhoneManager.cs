using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour, ITogglePuzzle
{
    public Transform pointer;
    public List<PhoneButton> buttons = new List<PhoneButton>();
    Animator anime;
    bool answerOn = false;
    public int[] correctSequence = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
    bool isCalled = false;
    bool canMoveNextButton = true;
    [SerializeField]
    int currentCursorIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        foreach (Transform child in transform)
        {
            if (child.GetComponent<PhoneButton>())
            {
                buttons.Add(child.GetComponent<PhoneButton>());
            }
        }
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
        }
    }

    private void ButtonHandler(float index)
    {
        

        if(currentCursorIndex < buttons.Count - 1)
        {
            currentCursorIndex += (int)index;
        }
        else if(currentCursorIndex <= 0)
        {
            currentCursorIndex = 0;
        }
        else
        {
            currentCursorIndex = 0;
        }
        MovePointer(buttons[currentCursorIndex].gameObject.transform.localPosition);
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
        }
    }

    public void EnablePuzzle()
    {
        if (!answerOn)
        {
            BoolAnimation(true);
            answerOn = true;
        }
    }

    public bool IsPuzzleSolved()
    {
        throw new System.NotImplementedException();
    }
}
