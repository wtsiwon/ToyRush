using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemStretegy 
{
    ITEM thisITEM;
    public ItemShop context;

    public ItemStretegy(ItemShop _itemShop)
    {
        context = _itemShop;
    }

    public void StretegyInit(EShopItem type)
    {
        switch (type)
        {
            case EShopItem.Shield:
                thisITEM = new ShieldItem(context);
                break;
            case EShopItem.Slime:
                thisITEM = new SlimeItem(context);
                break;
            case EShopItem.Clockwork:
                thisITEM = new ClockworkItem(context);
                break;
            case EShopItem.PirateRoulette:
                thisITEM = new PirateRouletteItem(context);
                break;
            case EShopItem.TreasureBox:
                thisITEM = new TreasureBoxItem(context);
                break;
        }

    }

    public void Attack()
    {
        thisITEM.Attack();
    }
}

public abstract class ITEM
{
    protected ItemShop context;
    protected GameObject gameObject;
    protected Transform transform;

    public abstract void Attack();
}

public class ShieldItem : ITEM
{
    public ShieldItem(ItemShop _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
    }

    public override void Attack()
    {
        Debug.Log("ShieldItme 사용");
    }
}

public class SlimeItem : ITEM
{
    public SlimeItem(ItemShop _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
    }

    public override void Attack()
    {
        Debug.Log("SlimeItem 사용");
    }
}

public class ClockworkItem : ITEM
{
    public ClockworkItem(ItemShop _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
    }

    public override void Attack()
    {
        Debug.Log("ClockworkItem 사용");
    }
}

public class PirateRouletteItem : ITEM
{
    public PirateRouletteItem(ItemShop _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
    }

    public override void Attack()
    {
        Debug.Log("PirateRouletteItem 사용");
    }
}

public class TreasureBoxItem : ITEM
{
    public TreasureBoxItem(ItemShop _context)
    {
        context = _context;
        gameObject = _context.gameObject;
        transform = _context.transform;
    }

    public override void Attack()
    {
        Debug.Log("TreasureBoxItem 사용");
    }
}

