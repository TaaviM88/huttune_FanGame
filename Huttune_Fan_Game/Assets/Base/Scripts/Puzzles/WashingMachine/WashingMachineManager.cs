using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineManager : MonoBehaviour, IInteractable
{
    Animator anime;
    public WashingMachinePuzzleState puzzleState = WashingMachinePuzzleState.NoPower;
    public WashingMachineState wmState = WashingMachineState.Idle;
    public string description = "";
    public string hint = "";
    public int id = 0;
    public bool canChangeState = true;
    public bool canChangePuzzleState = true;
    bool powerIsOn = false, detergentAdded = false, laundryadded = false;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        switch (puzzleState)
        {
            case WashingMachinePuzzleState.NoPower:
                UpdateJournal("No power?");
                break;
            case WashingMachinePuzzleState.PowerOn:
                UpdateJournal("Power is on");
                break;
            case WashingMachinePuzzleState.DetergentOn:
                UpdateJournal("Detergent added");
                ChangePuzzleState(WashingMachinePuzzleState.laundryNotAdded);
                break;
            case WashingMachinePuzzleState.laundryNotAdded:
                UpdateJournal("There is now laundry");
                break;
            case WashingMachinePuzzleState.laundryAdded:

                break;
            case WashingMachinePuzzleState.washProgram:

                break;
            case WashingMachinePuzzleState.washMaschineOn:

                break;
            case WashingMachinePuzzleState.washMachineOnFire:

                break;
            case WashingMachinePuzzleState.washMachinFireOut:

                break;
        }
    }

    public void ChangePuzzleState(WashingMachinePuzzleState newPuzzleState)
    {
        if (!canChangePuzzleState)
        {
            return;
        }

        //puzzleState = newPuzzleState;
        if(puzzleState < newPuzzleState)
        {
            puzzleState = newPuzzleState;
        }
    }

    public void UpdateJournal(string newHint)
    {
        //tmp
        print(newHint);
    }

    public void TriggerAnime(string triggerName)
    {
        anime.SetTrigger(triggerName);

    }
}
