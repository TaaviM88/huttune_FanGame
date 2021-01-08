using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    
    private Vector3 localForward;
    private Vector3 mouseRef;

    private bool draggin = false;
    private bool autoRotating = false;
    
    private float sensitivity = 0.4f;
    private float speed = 300f;
    private Vector3 rotation;

    private Quaternion targetQuaternion;

    private  ReadRubiksCube readCube;
    private  RubiksCubeState cubeState;

    // Start is called before the first frame update
    void Start()
    {
        readCube = GetComponentInParent<ReadRubiksCube>();
        cubeState = GetComponentInParent<RubiksCubeState>();
    }

   
    void LateUpdate()
    {
        if(draggin && !autoRotating)
        {
            SpinSide(activeSide);
            if(Input.GetMouseButtonUp(0))
            {
                draggin = false;
                RotateToRightAngle();
            }
        }
        if(autoRotating)
        {
            AutoRotate();
        }
    }

    private void SpinSide(List<GameObject> side)
    {
        //reset the rotation
        rotation = Vector3.zero;

        //current mouse position minus the last mouse position
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);


        if(side == cubeState.front)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }

        if (side == cubeState.up)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }

        if (side == cubeState.down)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }

        if (side == cubeState.left)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }

        if (side == cubeState.right)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.back)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        //rotate
        transform.Rotate(rotation, Space.Self);

        //store mouse
        mouseRef = Input.mousePosition;

    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        draggin = true;
        //Create a vector to rotate around
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;

    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        autoRotating = true;
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    }
    private void AutoRotate()
    {
        draggin = false;
        var step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        //if within one degree, set angle to target angle and end the rotation
        if(Quaternion.Angle(transform.localRotation,targetQuaternion) <=1)
        {
            transform.localRotation = targetQuaternion;
            //unparent the little cubes
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            RubiksCubeState.autoRotating = false;
            autoRotating = false;
            draggin = false;
        }
    }
}
