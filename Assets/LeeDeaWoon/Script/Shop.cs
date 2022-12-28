using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EShopItem
{
    Shield,
    Slime,
    Clockwork,
    PirateRoulette,
    TreasureBox,
    None,
}

[System.Serializable]
public class Shop
{
    public EShopItem eShopItem;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemPirce;
    public int itemNum;

    public Shop(Shop shop)
    {
        eShopItem = shop.eShopItem;
        itemName = shop.itemName;
        itemDescription = shop.itemDescription;
        itemIcon = shop.itemIcon;
        itemPirce = shop.itemPirce;
        itemNum = shop.itemNum;
    }
}
