using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineManager : MonoBehaviour, IInteractable, ITryUseItem<Item>
{
    Animator anime;
    SpawnItem spawnItem;
    public WashingMachinePuzzleState puzzleState = WashingMachinePuzzleState.NoPower;
    public WashingMachineState wmState = WashingMachineState.Idle;
    public string description = "";
    public string hint = "";
    public int id = 0;
    public bool canChangeState = true;
    public bool canChangePuzzleState = true;
    bool powerIsOn = false, detergentAdded = false, laundryadded = false,
        isCalled = false, machineIsOn = false, machineOnFire = false;
    public bool isSolved = false;

    public Item requiredToPutOutFire;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        spawnItem = GetComponent<SpawnItem>();
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
                powerIsOn = true;
                break;
            case WashingMachinePuzzleState.DetergentOn:
                UpdateJournal("Detergent added");
                detergentAdded = true;
                ChangePuzzleState(WashingMachinePuzzleState.laundryNotAdded);
                break;
            case WashingMachinePuzzleState.laundryNotAdded:
                UpdateJournal("There is now laundry");
                laundryadded = true;
                break;
            case WashingMachinePuzzleState.laundryAdded:

                break;
            case WashingMachinePuzzleState.washProgram:

                break;
            case WashingMachinePuzzleState.washMaschineOn:

                break;
            case WashingMachinePuzzleState.washMachineOnFire:
                if(!machineOnFire)
                {
                    //spawn particles
                    //play alarm sound
                    machineOnFire = true;
                }
                
                break;
            case WashingMachinePuzzleState.washMachinFireOut:
                if(!isSolved)
                {
                    //end alarm sound
                    //Kill particles
                    //end animation
                    spawnItem.SpawnKeyItem();
                    isSolved = true;
                }                
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

    public void StartWashMachine()
    {
        //Start Washmachine

        if(isCalled && powerIsOn && detergentAdded && laundryadded)
        {
            TriggerAnime("TriggerTurnOnMachine");
            puzzleState = WashingMachinePuzzleState.washMaschineOn;
            canChangePuzzleState = false;
        }
    }

    public void SetIsCalled(bool b)
    {
        isCalled = b;
    }

    public bool GetIsCalled()
    {
        return isCalled;
    }

    public void SetMachineToFire()
    {
        //Spawn particles
        machineOnFire = true;
        puzzleState = WashingMachinePuzzleState.washMachineOnFire;
    }

    public bool TryItem(Item usedItem)
    {

        if (puzzleState == WashingMachinePuzzleState.washMachineOnFire)
        {
            if(usedItem.name == requiredToPutOutFire.name)
            {
                puzzleState = WashingMachinePuzzleState.washMachinFireOut;
                return true;
            }
            else
            {
                UpdateJournal("Doesn't help");
                return false;
            }
        }

        UpdateJournal("Why? It doesn't do anything.");

        return false;
    }
}

