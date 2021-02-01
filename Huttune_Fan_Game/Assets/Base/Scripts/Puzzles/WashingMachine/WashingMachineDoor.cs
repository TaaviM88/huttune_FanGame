using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineDoor : MonoBehaviour, IInteractable, ITryUseItem<Item>
{
    public WashingMachineManager manager;
    WashMachineDoorState state = WashMachineDoorState.ClosedNoLoundry;
    public Item requiredItemToOpen;
    bool doorClosed = true;
    bool laundryAdded = false;
    bool canChangeState = true;
    public string hint = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if(manager.puzzleState > WashingMachinePuzzleState.DetergentOn)
        {
            switch (state)
            {
                case WashMachineDoorState.ClosedNoLoundry:
                    if (canChangeState)
                    {
                        manager?.TriggerAnime("TriggerDoor");
                        doorClosed = false;
                        if(!laundryAdded)
                        {
                            state = WashMachineDoorState.OpenNoLaundry;
                        }
                        else
                        {
                            state = WashMachineDoorState.OpenLaundryAdded;
                        }
                    }
                    break;
                case WashMachineDoorState.OpenNoLaundry:
                    if (canChangeState)
                    {
                        manager?.TriggerAnime("TriggerDoor");
                        doorClosed = true;
                        if(laundryAdded)
                        {
                            manager.ChangePuzzleState(WashingMachinePuzzleState.laundryAdded);
                            state = WashMachineDoorState.ClosedLaundryAdded;
                        }
                        else
                        {
                            state = WashMachineDoorState.ClosedLaundryAdded;
                        }
                    }
                    break;
                case WashMachineDoorState.ClosedLaundryAdded:
                    if (canChangeState)
                    {
                        if(laundryAdded)
                        {
                            UpdateJournal("Laundry is already added.");
                            if(manager.puzzleState < WashingMachinePuzzleState.laundryAdded)
                            {
                                manager.ChangePuzzleState(WashingMachinePuzzleState.laundryAdded);
                            }
                        }
                        else
                        {
                            state = WashMachineDoorState.ClosedNoLoundry;
                        }
                    }
                    break;
                case WashMachineDoorState.OpenLaundryAdded:
                    if (canChangeState)
                    {

                    }
                    break;
            }
        }
    }

    public void UpdateJournal(string newHint)
    {
        //tmp
        print(newHint);
    }

    public bool TryItem(Item usedItem)
    {
        if (!doorClosed)
        {
           if(requiredItemToOpen.scriptableItem.objName == usedItem.scriptableItem.objName && !laundryAdded)
            {
                laundryAdded = true;
                return true;
            }
        }

        return false;
    }
}
