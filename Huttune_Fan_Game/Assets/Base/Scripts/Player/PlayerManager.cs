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
    public bool isHoldingItem { get; set;}
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform eachChild in transform)
        {
            playerCamera = eachChild.GetComponent<Camera>();
            if(playerCamera != null)
            {
                break;
            }
        }

        enumManager = GetComponent<PlayerEnumManager>();
        
        TurnOffGhostVision();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
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
}
