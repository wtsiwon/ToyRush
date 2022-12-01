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

    public Shop(Shop shop)
    {
        itemName = shop.itemName;
        itemIcon = shop.itemIcon;
        itemPirce = shop.itemPirce;
    }
}
