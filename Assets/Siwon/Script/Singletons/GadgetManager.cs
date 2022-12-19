using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManager : Singleton<GadgetManager>
{
    [Tooltip("gadgets")]
    public List<Gadget> gadgetList = new List<Gadget>();

    [Tooltip("Slot(2칸)")]
    public List<GadgetSlot> gadgetSlotList = new List<GadgetSlot>();

    [Tooltip("gadgetDatas")]
    public List<GadgetData> gadgetDataList = new List<GadgetData>();

    [Tooltip("장착하기 전 버튼 UI")]
    public Sprite selectBtnSprite;
    [Tooltip("장착된 버튼 UI")]
    public Sprite selectedBtnSprite;

    public void ApplyGadgetAbility(EGadgetType type)
    {
        switch (type)
        {
            case EGadgetType.None:

                break;
            case EGadgetType.GravityBelt:

                //중력 증가
                break;
            case EGadgetType.SlowRocket:
                //공격패턴 속도 감소
                break;
            case EGadgetType.Magnet:
                //자석 활성화
                break;
            case EGadgetType.XrayGoggles:
                //...
                break;
        }
    }

    /// <summary>
    /// 가젯 장착함수
    /// </summary>
    public void ApplyGadget(Gadget gadget)
    {
        //gadgetSlotList
        if (TryApplyGadget(gadget.Data)) return;
        else
        {
            StartCoroutine(nameof(SelectGadgetSlot));
        }
    }

    private bool TryApplyGadget(GadgetData data)
    {
        for (int i = 0; i < gadgetSlotList.Count; i++)
        {
            if (gadgetSlotList[i].Data == null)
            {
                gadgetSlotList[i].Data = data;
                return true;
            }
        }
        return false;
    }

    private IEnumerator SelectGadgetSlot()
    {
        yield return null;
    }
}
