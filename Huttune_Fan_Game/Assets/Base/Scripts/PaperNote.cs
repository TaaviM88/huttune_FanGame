using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperNote : MonoBehaviour, IInteractable
{
    [TextArea(3, 10)]
    public string description = "";

    public void Interact()
    {
        Journal.Instance?.Log(description);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
