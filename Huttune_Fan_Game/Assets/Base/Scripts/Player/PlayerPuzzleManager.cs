using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPuzzleManager : MonoBehaviour
{

    PlayerManager manager;
    private void Start()
    {
        manager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (manager.enumManager.actionState == PlayerActionState.puzzle)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitPuzzleMode();
            }
        }

    }

    public void StartPuzzleInspect(Transform puzzleLocation)
    {
        Camera.main.transform.LookAt(puzzleLocation, Vector3.up);

        manager.canMove = false;
        manager.canLookAround = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ExitPuzzleMode()
    {
        manager.canChangeActionState = true;
        manager.enumManager.actionState = PlayerActionState.nothing;
        manager.canMove = true;
        manager.canLookAround = true;
        Cursor.lockState = CursorLockMode.Locked;       
    }
}
