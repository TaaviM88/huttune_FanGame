using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspect : MonoBehaviour
{
    public Transform inspectNode;
    private ScriptableItem itemToInspect;
    private GameObject itemToInspectObj;
    PlayerInventory inventory;
    PlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        manager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleInspectMode();
        }
    }

    private void ToggleInspectMode()
    {
        if(manager.enumManager.actionState != PlayerActionState.inspecting)
        {
            manager.canMove = false;
            manager.enumManager.actionState = PlayerActionState.inspecting;
            SpawnItemFromInventory();
        }
        else
        {
            manager.canMove = true;
            if(itemToInspectObj != null)
            {
                inventory.AddItem(itemToInspect);
                itemToInspectObj.transform.parent = null;
                Destroy(itemToInspectObj.gameObject);
                Cursor.lockState = CursorLockMode.Locked;

            }
            manager.enumManager.actionState = PlayerActionState.nothing;
        }
    }

    private void SpawnItemFromInventory()
    {
        itemToInspect = inventory.GetFirstItem();
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
        inspectNode.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, inspectNode.localPosition.z));
        itemToInspectObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, inspectNode.localPosition.z));
    }
}
