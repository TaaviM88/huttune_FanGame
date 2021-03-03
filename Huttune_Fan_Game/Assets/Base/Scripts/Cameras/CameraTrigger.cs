using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera cameraToSwitch;


    // Start is called before the first frame update
    void Start()
    {
        //cameraToSwitch.enabled = false;
        cameraToSwitch.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //cameraToSwitch.enabled = true;
            cameraToSwitch.gameObject.SetActive(true);
            // other.gameObject.GetComponent<PlayerManager>().gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerManager>().playerCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //cameraToSwitch.enabled = false;
            //other.gameObject.GetComponent<PlayerManager>().gameObject.SetActive(true);
            cameraToSwitch.gameObject.SetActive(false);
            other.gameObject.GetComponent<PlayerManager>().playerCamera.gameObject.SetActive (true);
        }
    }
}
