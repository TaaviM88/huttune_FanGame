using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    float blendValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        skin = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(TimerRoutine());

    }

    private IEnumerator TimerRoutine()
    {
        blendValue = UnityEngine.Random.Range(0,100);
        skin.SetBlendShapeWeight(0, blendValue);
        yield return new WaitForSeconds(0.5f); //code pauses for 5 seconds                              
    }
}
