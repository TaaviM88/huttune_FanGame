using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProcessingActivate : MonoBehaviour
{
    Volume volume;
    private ColorAdjustments colorAdjustments;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out colorAdjustments);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            colorAdjustments.active = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            colorAdjustments.active = false;
        }

    }
}
