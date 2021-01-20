using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, ITryUseItem<Item>, IInteractable
{
    Animator anime;
    public bool isLocked = false;
    public DoorState doorState = DoorState.Close;
    public string description = "";
    public string hintIfDoorIsLocked = "";
    public Item requiredItemToOpen;
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryItem(Item usedItem)
    {
        if(isLocked)
        {
            if(requiredItemToOpen.scriptableItem.objName == usedItem.scriptableItem.objName)
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
}
