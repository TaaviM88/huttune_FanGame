using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineLid : MonoBehaviour, IInteractable, ITryUseItem<Item>
{
    public WashingMachineManager manager;
    WashMachineLidState state = WashMachineLidState.ClosedNoDetergentAdded;
    [SerializeField]
    bool lidClosed = true;
    bool canChageState = true;
    public bool detergentAdded = false;
    public Item requiredItemToOpen;
    public string hint = "";

    // Start is called before the first frame update
    void Start()
    {
        canChageState = manager.canChangeState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {   
        if(manager.puzzleState > WashingMachinePuzzleState.NoPower)
        {
            switch (state)
            {
                case WashMachineLidState.ClosedNoDetergentAdded:
                    if (canChageState)
                    {
                        manager?.TriggerAnime("TriggerLid");
                        lidClosed = false;

                        if (!detergentAdded)
                        {
                            state = WashMachineLidState.OpenNoDetergentAdded;
                        }
                        else
                        {
                            state = WashMachineLidState.OpenDetergentAdded;
                        }
                    }
                    break;
                case WashMachineLidState.OpenNoDetergentAdded:
                    if (canChageState)
                    {
                        manager?.TriggerAnime("TriggerLid");
                        lidClosed = true;
                        if (detergentAdded)
                        {
                            manager.ChangePuzzleState(WashingMachinePuzzleState.DetergentOn);
                            state = WashMachineLidState.ClosedDetergentAdded;
                        }else
                        {
                            state = WashMachineLidState.ClosedNoDetergentAdded;
                        }
                    }
                    break;
                case WashMachineLidState.OpenDetergentAdded:
                    if (canChageState)
                    {

                    }
                    break;
                case WashMachineLidState.ClosedDetergentAdded:
                    if (canChageState)
                    {
                        if(detergentAdded)
                        {
                            UpdateJournal("Detergent is already added.");
                            if(manager.puzzleState < WashingMachinePuzzleState.DetergentOn)
                            {
                                manager.ChangePuzzleState(WashingMachinePuzzleState.DetergentOn);
                            }
                        }
                        else
                        {
                            state = WashMachineLidState.ClosedNoDetergentAdded;
                        }
                     
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            UpdateJournal("No power so I can't open the lid");
        }

    }

    public bool TryItem(Item usedItem)
    {
        if(!lidClosed)
        {
            if (requiredItemToOpen.scriptableItem.objName == usedItem.scriptableItem.objName && !detergentAdded)
            {
                detergentAdded = true;
                return true;
            }

            return false;
        }
        else
        {
            return false;
        }
       
    }

    public void UpdateJournal(string newHint)
    {
        //tmp
        print(newHint);
    }
}
