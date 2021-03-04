using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ScriptableItem> items = new List<ScriptableItem>();
    [SerializeField]
    private int currentItemListIndex = 0;

    public void AddItem(ScriptableItem item)
    {
        if(items.Count <= 0)
        {
            items.Add(item);
        }
        else
        {
            if(items.Contains(item))
            {
                return;
            }

            items.Add(item);
        }
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

    public ScriptableItem GetNextItem(int i)
    {
        if(items.Count == 0)
        {
            return null;
        }

        currentItemListIndex += i;

        //jos meinataan mennä yli niin palautetaan ensimmäinen
        if (currentItemListIndex > items.Count-1  )
        {
            currentItemListIndex = 0;
            return items[0];
        }
        //jos meinaa mennä miinuksen puolelle palautetaan listan viimeinen
        if(currentItemListIndex < 0)
        {
            return items[items.Count - 1];
        }
        //kaikissa muissa tapauksissa palautetaan ideksin kohdalta item
        
        return items[currentItemListIndex];
        
    }

    
}
