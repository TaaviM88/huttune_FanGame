using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : MonoBehaviour
{
    public ScriptableItem scriptableItem;
    protected int outlineLayer = 15;
     protected int originalLayer;
    protected bool isOutlineLayerOn = false;
    public float howLongOutlineLast = 2; 
    float cooldownTimer;

    private void Start()
    {
        originalLayer = gameObject.layer;
    }

    protected void Update()
    {
        if(isOutlineLayerOn)
        {
            cooldownTimer -= Time.deltaTime;

            if(cooldownTimer < 0)
            {
                TurnOutlineOff();
            }
        }
    }

    public void Use()
    {
        //Do use
    }

    public void StudyItem()
    {
        //tutki esinettä
    }

   public virtual ScriptableItem PickUpItem()
    {
        //print($"You got {scriptableItem.objName}");
        Journal.Instance?.Log($"You got {scriptableItem.objName}");
        return scriptableItem;
    }

    public virtual void DestroyThisObj()
    {
        Destroy(gameObject);
    }

    public void SetOutlineLayerOn()
    {
        cooldownTimer = howLongOutlineLast;
        if(isOutlineLayerOn)
        {
            return;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.layer = outlineLayer;
        }

        gameObject.layer = outlineLayer;
        isOutlineLayerOn = true;
    }

    public void TurnOutlineOff()
    {
        
        if(!isOutlineLayerOn)
        {
            return;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.layer = originalLayer;
        }

        gameObject.layer = originalLayer;
        isOutlineLayerOn = false;
    }
}
