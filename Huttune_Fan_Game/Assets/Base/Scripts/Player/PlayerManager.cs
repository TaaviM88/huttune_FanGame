using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public Camera playerCamera { get; set; }

    public PlayerEnumManager enumManager { get; set; }

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
