using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    public EItemType itemType;

    public Transform spawnPos;

    public void GetItem(EItemType type)
    {
        switch (type)
        {
            case EItemType.Sizecontrol:
                break;
            case EItemType.Magnet:
                break;
            
        }
    }



}
