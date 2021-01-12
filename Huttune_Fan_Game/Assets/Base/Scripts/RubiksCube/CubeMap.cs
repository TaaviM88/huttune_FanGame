using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    RubiksCubeState cubeState;
    public Transform up, down, left, right, front, back;

    bool frontIsSolved = false;
    bool backIsSolved = false;
    bool upIsSolved = false;
    bool downIsSolved = false;
    bool leftIsSolved = false;
    bool rightIsSolved = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsSolved()
    {
        CheckIfSolved(cubeState.front, front);
        CheckIfSolved(cubeState.back, back);
        CheckIfSolved(cubeState.left, left);
        CheckIfSolved(cubeState.right, right);
        CheckIfSolved(cubeState.up, up);
        CheckIfSolved(cubeState.down, down);
    }

    public void Set()
    {
        cubeState = FindObjectOfType<RubiksCubeState>();

        UpdateMap(cubeState.front, front);
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.left, left);
        UpdateMap(cubeState.right, right);
        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.down, down);

    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.red;
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            i++;
        }
    }

    void CheckIfSolved(List<GameObject> face, Transform side)
    {
        int i = 0;


        Color solvedColorFront = Color.black;
        Color solvedColorBack = Color.black;
        Color solvedColorUp = Color.black;
        Color solvedColorDown = Color.black;
        Color solvedColorLeft = Color.black;
        Color solvedColorRight = Color.black;

        foreach (Transform map in side)
        {
            if (face[i].name[0] == 'F')
            {
                if(i == 0)
                {
                    solvedColorFront = map.GetComponent<Image>().color;

                }

                if( map.GetComponent<Image>().color != solvedColorFront)
                {
                    frontIsSolved = false;
                }
                else
                {
                    frontIsSolved = true;
                }
                
                
            }
            if (face[i].name[0] == 'B')
            {
                if (i == 0)
                {
                    solvedColorBack = map.GetComponent<Image>().color;
                }

                if (map.GetComponent<Image>().color != solvedColorBack)
                {
                    backIsSolved = false;
                }
                else
                {
                    backIsSolved = true;
                }
            }
            if (face[i].name[0] == 'U')
            {
                if (i == 0)
                {
                    solvedColorUp = map.GetComponent<Image>().color;
                }

                if (map.GetComponent<Image>().color != solvedColorUp)
                {
                    upIsSolved = false;
                }
                else
                {
                    upIsSolved = true;
                }

            }
            if (face[i].name[0] == 'D')
            {
                if (i == 0)
                {
                    solvedColorDown = map.GetComponent<Image>().color;
                }

                if (map.GetComponent<Image>().color != solvedColorDown)
                {
                    downIsSolved = false;
                }
                else
                {
                    downIsSolved = true;
                }
            }
            if (face[i].name[0] == 'L')
            {
                if (i == 0)
                {
                    solvedColorLeft = map.GetComponent<Image>().color;
                }

                if (map.GetComponent<Image>().color != solvedColorLeft)
                {
                    leftIsSolved = false;
                }
                else
                {
                    leftIsSolved = true;
                }
            }
            if (face[i].name[0] == 'R')
            {
                if (i == 0)
                {
                    solvedColorRight = map.GetComponent<Image>().color;
                }

                if (map.GetComponent<Image>().color != solvedColorRight)
                {
                    rightIsSolved = false;
                }
                else
                {
                    rightIsSolved = true;
                }
            }

            i++;
        }

        if(frontIsSolved && backIsSolved && upIsSolved && downIsSolved && leftIsSolved && rightIsSolved)
        {
            print("Rubiks is solved. GG");
        }
        else
        {
            print("Rubiks is not solved. GID GUD");
        }
        //print($"front: {frontIsSolved}");
        //print($"back: {backIsSolved}");
        //print($"up: {upIsSolved}");
        //print($"down: {downIsSolved}");
        //print($"left: {leftIsSolved}");
        //print($"right: {rightIsSolved}");
    }
}
