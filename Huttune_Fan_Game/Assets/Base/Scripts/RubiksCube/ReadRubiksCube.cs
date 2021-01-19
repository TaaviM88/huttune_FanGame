using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadRubiksCube : MonoBehaviour
{
    public Transform tUp, tDown, tLeft, tRight,tFront,tBack;
    public GameObject emptyGO;
    private int layerMask = 1 << 9;

    RubiksCubeState cubeState;
    CubeMap cubeMap;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();

    private List<GameObject> frontFaces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();

        cubeState = GetComponent<RubiksCubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
        ReadState();
        RubiksCubeState.started = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadState()
    {
        //cubeState =  FindObjectOfType<RubiksCubeState>();
        //cubeMap = FindObjectOfType<CubeMap>();
        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);

        //update the map with found positions
        cubeMap.Set();
        //Check if cube is solved
        cubeMap.IsSolved();
    }

    void SetRayTransforms()
    {
        //populate the ray lists with raycasts eminating from the transform, angled towards the cube
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        backRays = BuildRays(tBack, new Vector3(0, 270, 0));
    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        //ray count is used to name the rays so we can be sure they are in the right order
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();
        //this creates 9 rays in the shape of the side of the cube with ray 0 at the top left and ray 8 at the bottom right
        //|0|1|2|
        //|3|4|5|
        //|6|7|8|

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startpos = new Vector3(rayTransform.localPosition.x + x, 
                    rayTransform.localPosition.y + y, 
                    rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startpos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }


    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();
        frontFaces.Clear();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            //Does the ray intersect any objects in the layerMask?
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //save faces to list
                frontFaces.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.tag);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }

        CheckIfSpecialFacesAreNear();
        return  facesHit;

    }

    public void CheckIfSpecialFacesAreNear()
    {
        for (int i = 0; i < frontFaces.Count; i++)
        {
           if( frontFaces[3].tag  == "SpecialFace" )
            {
                //print(i + "Vitun spessu jöpö palikka löydetty");
            }
        }
    }
}
