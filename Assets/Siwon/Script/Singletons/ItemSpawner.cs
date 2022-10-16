using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    public EItemType itemType;

    public void GetItem(EItemType type)
    {
        switch (type)
        {
            case EItemType.Big:

                break;
        }
    }



}
