﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspect : MonoBehaviour
{
    public Transform inspectNode;
    public Transform ItemSlot;
    //public bool isHoldingItem { get; private set;}
    private ScriptableItem itemToInspect;
    private GameObject itemToInspectObj;
    private GameObject currentlyEquippedItem;
    PlayerInventory inventory;
    PlayerManager manager;

    bool canChangeNextItem = true;
    Vector3 originalInspectNodeZPosition;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        manager = GetComponent<PlayerManager>();
        originalInspectNodeZPosition = inspectNode.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleInspectMode();
        }

        if(manager.enumManager.actionState == PlayerActionState.inspecting)
        {
           float horizontal = Input.GetAxisRaw("Horizontal");

            if(horizontal != 0 && canChangeNextItem)
            {
                InspectNextItem(horizontal);
                canChangeNextItem = false;
            }
            if(horizontal == 0)
            {
                canChangeNextItem = true;
            }

            if(Input.GetButtonDown("Fire1"))
            {
                EquipItem();
            }

            if(Input.GetButtonDown("Fire3"))
            {
                UnEquipItem();
            }
        }



        //print(isHoldingItem);
    }

    private void EquipItem()

    {   if(currentlyEquippedItem != null)
        {
            //inventory.AddItem(currentlyEquipedItem.GetComponent<Item>().scriptableItem);
            UnEquipItem();
        }
        currentlyEquippedItem = Instantiate(itemToInspectObj, Vector3.zero, Quaternion.identity);

        currentlyEquippedItem.transform.parent = ItemSlot;
        currentlyEquippedItem.transform.localPosition = Vector3.zero;
        currentlyEquippedItem.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //isHoldingItem = true;
        manager.isHoldingItem = true;
    }

    private void UnEquipItem()
    {
        currentlyEquippedItem.transform.parent = null;
        Destroy(currentlyEquippedItem.gameObject);
        //isHoldingItem = false;
        manager.isHoldingItem = false;
    }

    private void ToggleInspectMode()
    {
        if(manager.enumManager.actionState != PlayerActionState.inspecting)
        {
            manager.canMove = false;
            manager.enumManager.actionState = PlayerActionState.inspecting;
            //Spawn first item from inventory
            SpawnItemFromInventory(0);
        }
        else
        {
            manager.canMove = true;
            if(itemToInspectObj != null)
            {
               PutItemBackToInventory();
            }
            manager.enumManager.actionState = PlayerActionState.nothing;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void SpawnItemFromInventory(int i)
    {
        itemToInspect = inventory.GetNextItem(i); //inventory.GetFirstItem();
        inspectNode.localRotation = Quaternion.Euler(Vector3.zero);
        if (itemToInspect == null)
        {
            ToggleInspectMode();
            return;
        }

        itemToInspectObj = Instantiate(itemToInspect.prefab, Vector3.zero, Quaternion.identity);
        itemToInspectObj.transform.parent = inspectNode;
        itemToInspectObj.transform.localPosition = Vector3.zero;
        itemToInspectObj.transform.localRotation = Quaternion.Euler(Vector3.zero);

        inventory.RemoveItem(itemToInspect);
        Cursor.lockState = CursorLockMode.None;

        CenterTheObject();
    }

    public void CenterTheObject()
    {
        inspectNode.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, originalInspectNodeZPosition.z));
        itemToInspectObj.transform.localPosition = Vector3.zero;
    }

    private void InspectNextItem(float dir)
    {
        //vaihdetaan esinettä
        //jos dir on 1 = mennään listassa eteenpäin(oikealle)
        //jos dir on -1 = mennään listassa taaksepäin(vasemmalle)
        PutItemBackToInventory();
        SpawnItemFromInventory((int)dir);
    }

    private void PutItemBackToInventory()
    {
        //laitetaan esine takaisin inventoryyn
        inventory.AddItem(itemToInspect);
        itemToInspectObj.transform.parent = null;
        Destroy(itemToInspectObj.gameObject);
    }

    private void DropItem()
    {
        //pudotetaan esine maahan
    }

    public Item UseEquippedItem()
    {
        if(manager.isHoldingItem)
        {
            return currentlyEquippedItem.GetComponent<Item>();
        }
        else
        {
            return null;
        }
    }
}
