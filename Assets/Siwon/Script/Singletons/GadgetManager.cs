using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManager : Singleton<GadgetManager>
{
    [Tooltip("gadgetDatas")]
    public List<Gadget> gadgetDataList = new List<Gadget>();

    [Tooltip("Slot(2Ä­)")]
    public List<GadgetSlot> gedgetSlotList = new List<GadgetSlot>();

    [Tooltip("ÀåÂøÇÏ±â Àü ¹öÆ° UI")]
    public Sprite selectBtnSprite;
    [Tooltip("ÀåÂøµÈ ¹öÆ° UI")]
    public Sprite selectedBtnSprite;

    /// <summary>
    /// °¡Á¬ ÀåÂøÇÔ¼ö
    /// </summary>
    public void ApplyGadget(Gadget gadget)
    {
        //gadgetSlotList
    }
}
