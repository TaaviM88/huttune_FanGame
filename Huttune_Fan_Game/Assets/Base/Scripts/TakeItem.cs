using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : Item
{
    //public ScriptableItem scriptableItem;
    public bool hasInfiniteAmount = false;
    public string descriptionWhatToSayWhenItIsEmpty = "You got item: ";
    int outlineLayer = 15;
    int originalLayer;
    bool isOutlineLayerOn = false;
    bool isItemTaken = false;
    //public float howLongOutlineLast = 2;
    float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        originalLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override ScriptableItem PickUpItem()
    {
        //base.PickUpItem();

        if (!isItemTaken)
        {
            if(!hasInfiniteAmount)
            {
                Journal.Instance?.Log($"You got {scriptableItem.objName}");
                isItemTaken = true;
            }

            return scriptableItem;
        }else
        {
            Journal.Instance?.Log($"{descriptionWhatToSayWhenItIsEmpty} {scriptableItem.objName}");
            return null;
        }
    }

    public override void DestroyThisObj()
    {
        //base.DestroyThisObj();

        
    }
}
