using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, ITryUseItem<Item>, IInteractable
{
    Animator anime;
    public bool isLocked = false;
    public DoorState doorState = DoorState.Close;
    public DoorLockType lockType = DoorLockType.None;
    public string description = "";
    public string hintIfDoorIsLocked = "";
    public Item requiredItemToOpen;
    public int id = 0;
    bool isMoving = false;

    
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        if(lockType == DoorLockType.Puzzle)
        {
            CombineLockController.CheckIfPuzzleIsSolve += CheckResults;
        }
    }

    private void CheckResults(int idPuzzle, bool isSolved)
    {
        if(idPuzzle != id)
        {
            return;
        }

        if(isSolved)
        {
            isLocked = false;
            doorState = DoorState.Close;
            print("puzzle on ratkaistu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("Is unlocked" + puzzlelockController.openLock);
    }

    public bool TryItem(Item usedItem)
    {
        if(lockType == DoorLockType.Key)
        {
            if (isLocked)
            {
                if (requiredItemToOpen.scriptableItem.objName == usedItem.scriptableItem.objName)
                {
                    isLocked = false;
                    doorState = DoorState.Close;
                    print("Door is open now");
                    return true;
                }

                return false;
            }
            else
            {
                print("Door was open already");
                return false;
            }
        }

        return false;
    }

    public void Interact()
    {
        switch (doorState)
        {
            case DoorState.Close:
               
                if(!isMoving && !isLocked)
                {
                    anime.SetTrigger("Open");
                    doorState = DoorState.Open;
                }

                break;
            case DoorState.Open:
                
                if(!isMoving && !isLocked)
                {
                    anime.SetTrigger("Close");
                    doorState = DoorState.Close;
                }
                
                break;
            case DoorState.Locked:
                
                print($"{hintIfDoorIsLocked}");

                break;
            case DoorState.Moving:
                break;
            default:
                break;
        }
    }

    public void ToggleMoving()
    {
        isMoving = isMoving ? false : true;
    }

    private void OnDestroy()
    {
        CombineLockController.CheckIfPuzzleIsSolve -= CheckResults;
    }
}
