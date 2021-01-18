using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    CharacterController controller;
    PlayerManager manager;

    [SerializeField]
    private float crouchHeight = 1f;
    [SerializeField]
    private float crouchMoveSpeed = 1f;

    private float originalHeight;
    private float originalCameraHeight;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        manager = GetComponent<PlayerManager>();

        originalHeight = controller.height;
        originalCameraHeight = manager.playerCamera.transform.localPosition.y;

        manager.enumManager.standState = PlayerStandState.Stand;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            //Kyykkyyn
            CrouchDown();
        }

        if(Input.GetButtonUp("Fire2"))
        {
            //pos kyykystä
            StandUp();
        }
    }

    private void StandUp()
    {
        controller.height = originalHeight;
        manager.enumManager.standState = PlayerStandState.Stand;
    }

    private void CrouchDown()
    {
        controller.height = crouchHeight;
        manager.enumManager.standState = PlayerStandState.Crouch;
    }
}
