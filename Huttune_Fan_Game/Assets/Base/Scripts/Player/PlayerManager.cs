using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public Camera playerCamera { get; set; }

    public PlayerEnumManager enumManager { get; set; }

    public LayerMask all;
    public LayerMask normal;

    public bool canMove { get; set; }
    public bool canLookAround { get; set; }
    public bool canChangeActionState { get; set; }
    public bool isHoldingItem { get; set; }

    public float puzzleModeFOV = 30f;
    float orignalFOV;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform eachChild in transform)
        {
            playerCamera = eachChild.GetComponent<Camera>();
            if (playerCamera != null)
            {
                break;
            }
        }

        enumManager = GetComponent<PlayerEnumManager>();

        TurnOffGhostVision();
        canMove = true;
        canLookAround = true;
        canChangeActionState = true;
        enumManager.actionState = PlayerActionState.nothing;
        orignalFOV = playerCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enumManager.actionState)
        {
            case PlayerActionState.nothing:

                break;
            case PlayerActionState.puzzle:

                break;
            case PlayerActionState.reading:

                break;
            case PlayerActionState.peeping:

                break;
            case PlayerActionState.inspecting:

                break;
            default:

                break;
        }

        //if(Input.GetButtonDown("Fire3"))
        //{
        //    ToggleMovement();
        //}

        //if(Input.GetButtonDown("Fire1"))
        //{
        //    TurnOnGhostVision();
        //}

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    TurnOffGhostVision();
        //}
    }
    public void TurnOnGhostVision()
    {
        playerCamera.cullingMask = all;
    }

    public void TurnOffGhostVision()
    {
        playerCamera.cullingMask = normal;
    }

    public void ToggleMovement()
    {
        canMove = canMove ? false : true;
    }

    public void ChangeCameraFOVPuzzleFOV()
    {
        playerCamera.fieldOfView = puzzleModeFOV;
    }

    public void ChangeCameraFOVBack()
    {
        playerCamera.fieldOfView = orignalFOV;
    }
}
