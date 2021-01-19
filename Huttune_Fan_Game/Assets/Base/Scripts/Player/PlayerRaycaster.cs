using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    public float throwForce = 10f;
    public float maxThrowForce = 4f;

    public float range = 10f;
    public float pushForce = 25f;
    public GameObject throwObjPrefab;
    private GameObject apple = null;
    private float holdDownStartTimer;
    Camera FPSCamera;
    PlayerManager manager;
    PlayerInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerManager>();
        inventory = GetComponent<PlayerInventory>();
        FPSCamera = manager.playerCamera;

    }

    // Update is called once per frame
    void Update()
    {
        if(!manager.canMove)
        {
            return;
        }

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    //Throw();
        //    holdDownStartTimer = Time.time;
        //}

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    float holdDownTime = Time.time - holdDownStartTimer;
        //    // float newForce = CalculateHoldDownForce(holdDownTime * 2f);
        //    //Throw(newForce);
        //    Throw(CalculateHoldDownForce(holdDownTime * 2f));
        //}

        if (Input.GetButtonDown("Fire1"))
        {
            ProcessRayCast();
        }

    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        {
            Item item = hit.collider.gameObject.GetComponent<Item>();
            if (item == null)
            {
                return;
            }

            inventory.AddItem(item.scriptableItem);
            item.PickUpItem();
            //rb.AddForce(FPSCamera.transform.forward * pushForce, ForceMode.Impulse);
        }
    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxThrowForce);
        float force = holdTimeNormalized * throwForce;
        return force;
    }

    public void Throw(float newthrowForce)
    {
        if (apple != null)
        {
            Destroy(apple);
        }

        apple = Instantiate(throwObjPrefab, transform.position, Quaternion.identity);
        apple.GetComponent<Rigidbody>().AddForce(FPSCamera.transform.forward * newthrowForce, ForceMode.Impulse);
    }
}
