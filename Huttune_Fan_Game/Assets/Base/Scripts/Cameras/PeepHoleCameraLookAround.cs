using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeepHoleCameraLookAround : MonoBehaviour
{
    public float mouseSensivity = 100f;

    public float minXAngle = -10f;
    public float maxXAngle = 10f;
    public float minYAngle = -10f;
    public float maxYAngle = 10f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    public float mouseX;
    public float mouseY;

    private float origXAngle;
    private float origYAngle;

    // Start is called before the first frame update
    void Awake()
    {
        origXAngle = transform.localRotation.x;
        origYAngle = transform.localRotation.y;
    }

    // Update is called once per framE
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, minXAngle, maxXAngle);
        yRotation = Mathf.Clamp(yRotation, minYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }

    private void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(origXAngle, origYAngle,0);
    }
}
