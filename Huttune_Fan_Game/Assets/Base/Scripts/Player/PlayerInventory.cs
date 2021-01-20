using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ScriptableItem> items = new List<ScriptableItem>();
    private int currentItemListIndex = 0;

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

    public ScriptableItem GetNextItem(int i)
    {
        if(items.Count == 0)
        {
            return null;
        }
        currentItemListIndex += i;
        //jos meinataan mennä yli niin palautetaan ensimmäinen
      if (items.Count >= currentItemListIndex-1)
      {
            currentItemListIndex = 0;
        return items[0];
      }
      //jos meinaa mennä miinuksen puolelle palautetaan listan viimeinen
      else if(currentItemListIndex < 0)
        {
            return items[items.Count - 1];
        }
      //kaikissa muissa tapauksissa palautetaan ideksin kohdalta item
        else
        {
            return items[currentItemListIndex];
        }
    }

    
}
