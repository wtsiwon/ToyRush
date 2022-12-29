using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GadgetManager : Singleton<GadgetManager>
{
    #region GadgetLists
    [Space(10f)]
    [Tooltip("gadgets")]
    public List<Gadget> gadgetList = new List<Gadget>();

    [Space(10f)]
    [Tooltip("Slot(2칸)")]
    public List<GadgetSlot> gadgetSlotList = new List<GadgetSlot>(2);

    [SerializeField]
    [Tooltip("가젯슬롯에 있는 버튼")]//이 버튼으로 
    private List<Button> gadgetSlotBtns = new List<Button>(2);

    [Space(10f)]
    [Tooltip("gadgetDatas")]
    public List<GadgetData> gadgetDataList = new List<GadgetData>();
    #endregion
    #region UIs
    [Header("UI Sprite")]
    [Space(15f)]
    [Tooltip("장착하기 전 버튼 UISprite")]
    public Sprite selectBtnSprite;
    [Tooltip("장착된 버튼 UISprite")]
    public Sprite selectedBtnSprite;

    [SerializeField]
    [Space(10f)]
    [Tooltip("가젯 장착 시 반투명 배경")]
    private Button pauseBackBtn;//배경을 클릭할시 장착모드 해제해야함

    [SerializeField]
    [Tooltip("가젯 슬롯2개를 가지고 있는 GameObject")]
    private GameObject slot;
    #endregion

    [Space(10f)]
    [Tooltip("현재 선택된 있는 가젯")]
    public Gadget currentSelectGadget;

    #region Positions
    [SerializeField]
    [Tooltip("상점 UI가 켜져 있을 때 SlotPos")]
    private Vector3 truePos;

    [SerializeField]
    [Tooltip("상점 UI가 꺼져 있을 때 SlotPos")]
    private Vector3 falsePos;

    [SerializeField]
    [Tooltip("게임 시작했을 때 SlotPos")]
    private Vector3 gameStartPos;
    #endregion

    private Coroutine CselectGadgetSlot;

    private bool isPutOnMode;
    //가젯 장착 모드
    public bool IsPutOnMode
    {
        get => isPutOnMode;
        set
        {
            //다른건 선택X
            pauseBackBtn.gameObject.SetActive(isPutOnMode);
        }
    }

    private bool isShopActive;
    public bool IsShopActive
    {
        get => isShopActive;
        set
        {
            if (GameManager.Instance.IsGameStart == false)
            {
                if (value == true)
                {
                    slot.transform.position = truePos;
                }
                else
                {
                    slot.transform.position = falsePos;
                }
            }
            else
            {
                slot.transform.DOMove(gameStartPos, 0.5f).SetEase(Ease.InBack);
            }
        }
    }

    private void Start()
    {
        //pauseBackBtn.onClick.AddListener(() =>
        //{
        //    IsPutOnMode = false;
        //});
    }

    /// <summary>
    /// 가젯 능력치 적용함수
    /// </summary>
    /// <param name="type"></param>
    public void ApplyGadgetAbility(EGadgetType type)
    {
        switch (type)
        {
            case EGadgetType.None:
            //null

            case EGadgetType.GravityBelt:

                //중력 증가
                break;
            case EGadgetType.SlowRocket:
                //공격패턴 속도 감소

                break;
            case EGadgetType.Magnet:
                //자석 활성화
                Player.Instance.IsMagneting = true;
                break;
            case EGadgetType.XrayGoggles:
                Player.Instance.isUseXray = true;
                //...
                break;
        }
    }

    /// <summary>
    /// 가젯 장착하는함수
    /// </summary>
    public void ApplyGadget(Gadget gadget)
    {
        //gadgetSlotList
        if (TryApplyGadget(gadget)) return;
        else
        {
            CselectGadgetSlot = StartCoroutine(CSelectGadgetSlot(gadget));//실행 될까
        }
    }

    public void RemoveGadget(Gadget gadget)
    {
        gadgetSlotList[gadget.slotIndex] = null;
    }


    /// <summary>
    /// 가젯 적용 시도 함수
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private bool TryApplyGadget(Gadget gadget)
    {
        for (int i = 0; i < gadgetSlotList.Count; i++)
        {
            if (gadgetSlotList[i].Data == null)
            {
                gadgetSlotList[i].gadgetIcon.sprite = gadget.Data.icon;
                gadgetSlotList[i].Data = gadget.Data;
                gadget.slotIndex = i;
                return true;
            }
        }
        return false;
    }


    private bool[] CheckSlot()
    {
        bool[] checks = {false};

        for (int i = 0; i < gadgetSlotList.Count; i++)
        {
            if(gadgetSlotList[i].Data != null)
            {
                checks[i] = true;
            }
            else
            {
                checks[i] = false;
            }
        }

        return checks;
    }

    private IEnumerator CSelectGadgetSlot(Gadget gadget)
    {
        IsPutOnMode = true;
        currentSelectGadget = gadget;
        yield return null;
    }
}
