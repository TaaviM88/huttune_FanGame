using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ScriptableItem : ScriptableObject
{
    public Sprite icon = null;
    public string objName = "";
    public GameObject prefab = null;


    public void RemoveFromInventory()
    {
        //InventoryB.instance.RemoveItem(this);
    }
}
