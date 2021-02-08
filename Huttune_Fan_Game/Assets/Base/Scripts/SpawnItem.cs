using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public Transform spawnPoint;
    public ScriptableItem keyItemToSpawn;
    public bool isSpawn = false;
    public Vector3 customRotation;
    private GameObject itemToSpawnObj;
    
    public void SpawnKeyItem()
    {
        if(!isSpawn)
        {
            itemToSpawnObj = Instantiate(keyItemToSpawn.prefab, spawnPoint.position, Quaternion.identity);
            if(customRotation != Vector3.zero)
            {
                transform.Rotate(customRotation);
            }

            isSpawn = true;
        }
    }
}
