using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class RoomManager : MonoBehaviour
{
    public string roomName = "";
    public List<Light> roomLights = new List<Light>();
    //public List<Transform> randomRotateObjects = new List<Transform>();
    //HDAdditionalLightData a;
    public List<lightSwitchController> lightSwitches = new List<lightSwitchController>();
    float timeLeft = 3f;
    float originalTimeLeft;
    // Start is called before the first frame update
    void Start()
    {

        originalTimeLeft = timeLeft;

        foreach (Transform child in transform)
        {
            if(child.GetComponent<Light>())
            {
                roomLights.Add(child.GetComponent<Light>());
            }

           // randomRotateObjects.Add(child);
        }

        TurnAllLightOff();
    }

    // Update is called once per frame
    void Update()
    {
       // CooldownTimer();
    }

    private void CooldownTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            RandomRotateRoomObjects();
            timeLeft = originalTimeLeft;
        }
    }

    public bool Toggle()
    {
        // testivalo.enabled = !testivalo.enabled;

        for (int i = 0; i < roomLights.Count; i++)
        {
            roomLights[i].enabled = !roomLights[i].enabled;

        }

        return roomLights[0].enabled;
    }
    public void RandomRange()
    {
        for (int i = 0; i < roomLights.Count; i++)
        {
          
            roomLights[i].range += UnityEngine.Random.Range(-0.2f, 0.2f);
        }
    }

    public void TurnAllLightOff()
    {
        for (int i = 0; i < roomLights.Count; i++)
        {
            roomLights[i].enabled = false;

        }
    }

    public void RandomRotateRoomObjects()
    {
        //for (int i = 0; i < randomRotateObjects.Count; i++)
        //{

        //    randomRotateObjects[i].Rotate(Vector3.up *UnityEngine.Random.Range(0.2f, 180f));
        //}
    }
}
