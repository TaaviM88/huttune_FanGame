using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ScriptableItem> items = new List<ScriptableItem>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ScriptableItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(ScriptableItem item)
    {
        items.Remove(item);
    }

    public ScriptableItem GetFirstItem()
    {
        if(items.Count <= 0)
        {
            return null;
        }

        return items[0];
    }
}
