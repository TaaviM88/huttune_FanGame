using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : MonoBehaviour
{
    public ScriptableItem scriptableItem;

    public void Use()
    {
        //Do use
    }

    public void StudyItem()
    {
        //tutki esinettä
    }

   public void PickUpItem()
    {
        print($"You got {scriptableItem.objName}");
        Destroy(gameObject);
    }
}
