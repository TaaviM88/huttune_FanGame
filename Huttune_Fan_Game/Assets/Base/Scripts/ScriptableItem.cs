using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ScriptableItem : ScriptableObject
{
    public Sprite icon = null;
    public string objName = "";
    public string description = "";
    public GameObject prefab = null;
    public Vector3 spawnRotation = Vector3.zero;
    public Vector3 spawnPosition = Vector3.zero;
    public int useTimes = 1;
    public bool infiniteUse = false;



    public void RemoveFromInventory()
    {
        //InventoryB.instance.RemoveItem(this);
    }
}
