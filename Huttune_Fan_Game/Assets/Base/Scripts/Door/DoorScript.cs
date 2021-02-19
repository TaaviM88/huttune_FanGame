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
    AudioSource audioSource;
    public AudioClip audioHandleClank;
    public AudioClip audioDoorOpenSqueak;
    public AudioClip audioDoorCloseSqueak;
    public AudioClip audioDoorCloseSlam;
    public AudioClip audioDoorLocked;
    public AudioClip audioUnlock;
    bool isMoving = false;
    bool isFirstRun = true;
    
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        if(lockType == DoorLockType.Puzzle)
        {
            CombineLockController.CheckIfPuzzleIsSolve += CheckResults;
        }

        audioSource = GetComponent<AudioSource>();
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

    public void PlayAudioDoorHandleClank()
    {
        audioSource.PlayOneShot(audioHandleClank);
    }

    public void PlayAudioDoorOpenSqueak()
    {
        audioSource.PlayOneShot(audioDoorOpenSqueak);
    }


    public void IsFirstRun()
    {
        isFirstRun = false;
    }

    public void PlayAudioDoorCloseSqueak()
    {
        if(isFirstRun)
        {
            return;
        }
        audioSource.PlayOneShot(audioDoorCloseSqueak);
    }

    public void PlayAudioDoorCloseSlam()
    {
        if (isFirstRun)
        {
            return;
        }
        audioSource.PlayOneShot(audioDoorCloseSlam);
    }

    public void PlayAudioDoorLocked()
    {
        if (isFirstRun)
        {
            return;
        }
        audioSource.PlayOneShot(audioDoorLocked);
    }

    public void PlayAudioDoorUnlocked()
    {
        if (isFirstRun)
        {
            return;
        }
        audioSource.PlayOneShot(audioUnlock);
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
