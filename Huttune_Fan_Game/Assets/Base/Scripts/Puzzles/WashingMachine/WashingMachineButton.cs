using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingMachineButton : MonoBehaviour, IInteractable
{
    public WashingMachineManager manager;
    bool isPressed = false;

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
        print("Yritetään käynnistää pesukonetta");
        manager.StartWashMachine();
    }

}
