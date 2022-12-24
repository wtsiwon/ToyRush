using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Shop
{
    public string itemName;
    public Image itemIcon;
    public int itemPirce;
    public int itemNum;

    public Shop(Shop shop)
    {
        itemName = shop.itemName;
        itemIcon = shop.itemIcon;
        itemPirce = shop.itemPirce;
        itemNum = shop.itemNum;
    }
}
