using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : MonoBehaviour
{
    public ScriptableItem scriptableItem;
    int outlineLayer = 15;
    int originalLayer;
    bool isOutlineLayerOn = false;
    public float howLongOutlineLast = 2; 
    float cooldownTimer;

    private void Start()
    {
        originalLayer = gameObject.layer;
    }

    private void Update()
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

        gameObject.layer = outlineLayer;
        isOutlineLayerOn = true;
    }

    public void TurnOutlineOff()
    {
        
        if(!isOutlineLayerOn)
        {
            print("fukkk");
            return;
        }

        gameObject.layer = originalLayer;
        isOutlineLayerOn = false;
    }
}
