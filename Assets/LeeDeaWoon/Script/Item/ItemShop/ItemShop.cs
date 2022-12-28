using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public EShopItem eShopItem;
    ItemStretegy itemStretegy;
    Button itemShopBtn;

    void Start()
    {
        itemShopBtn = GetComponent<Button>();
        itemStretegy = new ItemStretegy(this);

        ItemBtn();
    }

    void Update()
    {
        itemStretegy.StretegyInit(eShopItem);
    }

    void ItemBtn()
    {
        itemShopBtn.onClick.AddListener(() =>
        {
            itemStretegy.Attack();
        });
    }
}
