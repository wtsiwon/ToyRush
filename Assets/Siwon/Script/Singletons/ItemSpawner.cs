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
                ObjPool.Instance.GetItem(EItemType.Sizecontrol, spawnPos.position);
                break;
            case EItemType.Magnet:
                ObjPool.Instance.GetItem(EItemType.Magnet, spawnPos.position);
                break;
            
        }
    }



}
