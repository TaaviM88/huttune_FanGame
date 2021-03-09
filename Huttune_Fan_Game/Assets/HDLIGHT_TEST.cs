using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
public class HDLIGHT_TEST : MonoBehaviour
{
    HDAdditionalLightData lightData;
    public float FadeDistance = 1;
    // Start is called before the first frame update
    void Start()
    {
        lightData = GetComponent<HDAdditionalLightData>();
        lightData.volumetricFadeDistance = FadeDistance;
    }

    // Update is called once per frame
    void Update()
    {
        lightData.volumetricFadeDistance = FadeDistance;
    }
}
