using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
public class SolveTwoPhase : MonoBehaviour
{
    ReadRubiksCube readCube;
    RubiksCubeState cubeState;
    private bool doOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        readCube = GetComponent<ReadRubiksCube>();
        cubeState = GetComponent<RubiksCubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RubiksCubeState.started && doOnce)
        {
            doOnce = false;
            Solver();
        }
    }

    public void Solver()
    {
        readCube.ReadState();

        //get the state of the cube as a string
        string moveString = cubeState.GetStateString();

        //solve the cube
        string info = "";
        //first time build the tables
        //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);

        //every other time
        string solution = Search.solution(moveString, out info);

        //convert the solved moves from string to a list
        List<string> solutionList = StringToList(solution);

        //automate the list
        Automate.moveList = solutionList;
        //print(info);
    }


    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }
}
