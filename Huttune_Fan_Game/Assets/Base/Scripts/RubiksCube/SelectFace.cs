using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    RubiksCubeState cubeState;
    ReadRubiksCube readCube;
    int layerMask = 1 << 9;

    // Start is called before the first frame update
    void Start()
    {
        readCube = GetComponent<ReadRubiksCube>();
        cubeState = GetComponent<RubiksCubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !RubiksCubeState.autoRotating)
        {
            //read the current state of the cube
            readCube.ReadState();

            //raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                //Make a list of all the sides (list of face gameobjects)
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };
                //if face hit exists with a side
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face))
                    {
                        //pick it up
                        cubeState.PickUp(cubeSide);
                        //start the side rotation logic
                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
    }
}
