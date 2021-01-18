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
        playerCamera.cullingMask = normal;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            playerCamera.cullingMask = all;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            playerCamera.cullingMask = normal;
        }


    }
}
