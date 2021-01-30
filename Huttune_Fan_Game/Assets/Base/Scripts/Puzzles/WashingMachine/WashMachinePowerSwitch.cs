using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashMachinePowerSwitch : MonoBehaviour,IInteractable, ITryUseItem<Item>
{
    public WashingMachineManager manager;
    public bool isLocked = true;
    public string description = "";
    public string hint = "";
    public Item requiredItemToOpen;
    //public int id = 0;
    bool isSolved = false;

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
        //Add journal script stuff
        print(hint);
       
    }

    public bool TryItem(Item usedItem)
    {
        if (requiredItemToOpen.scriptableItem.objName == usedItem.scriptableItem.objName)
        {
            SolvePuzzle();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SolvePuzzle()
    {
        isLocked = false;
        isSolved = true;
        manager?.ChangePuzzleState(WashingMachinePuzzleState.PowerOn);
    }
}


