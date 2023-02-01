using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Gadget : MonoBehaviour
{
    #region UI
    [SerializeField]
    [Tooltip("가젯 아이콘")]
    private Image icon;

    [Header("TextMeshPros")]
    [Space(15f)]
    [SerializeField]
    [Tooltip("가젯의 이름Text")]
    private TextMeshProUGUI nameTxt;

    [SerializeField]
    [Tooltip("가젯의 대한 설명Text")]
    private TextMeshProUGUI explainTxt;

    [SerializeField]
    [Tooltip("가젯의 가격Text 구매시 비활성화")]
    private TextMeshProUGUI costTxt;

    [Header("Buttons")]
    [Space(15f)]
    [SerializeField]
    private Button buyBtn;

    [SerializeField]
    [Tooltip("가젯 선택하는 버튼Btn")]
    private Button selectBtn;

    [SerializeField]
    [Tooltip("가젯 선택 해제하는 버튼")]
    private Button deSelectBtn;
    #endregion

    public int slotIndex;

    public GadgetSlot Slot
    {
        get => GadgetManager.Instance.gadgetSlotList[slotIndex];
    }

    [Tooltip("이 가젯이 장착되어 있는가")]
    [Space(10f)]
    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            selectBtn.gameObject.SetActive(!value);
            deSelectBtn.gameObject.SetActive(value);
        }
    }

    public bool IsBought
    {
        get => data.isBought;
        set
        {
            data.isBought = value;

            buyBtn.gameObject.SetActive(!value);
            selectBtn.gameObject.SetActive(value);
        }
    }

    [SerializeField]
    [Tooltip("가젯에 필요한 정보Data")]
    private GadgetData data;

    public GadgetData Data
    {
        get => data;
        set
        {
            data = value;
        }
    }

    private void Start()
    {
        OnClickAddListener();
    }

    private void OnClickAddListener()
    {
        Debug.Assert(buyBtn != null, $"buyBtnBtn is null {data.gadgetType}");
        buyBtn.onClick.AddListener(() =>
        {
            if (GameManager.Instance.haveCoin >= data.cost)
            {
                GameManager.Instance.haveCoin -= data.cost;
                IsBought = true;
            }
        });

        Debug.Assert(selectBtn != null, $"SelectBtn is null {data.gadgetType}");
        selectBtn.onClick.AddListener(() =>
        {
            if (IsBought == true)
            {
                if (IsSelected == false)
                {
                    GadgetManager.Instance.ApplyGadget(this);
                    IsSelected = true;
                }
                else
                {
                    IsSelected = false;
                    GadgetManager.Instance.RemoveGadget(this);
                }
            }
        });

        Debug.Assert(deSelectBtn != null, $"DeSelectBtn is null {data.gadgetType}");
        deSelectBtn.onClick.AddListener(() =>
        {
            GadgetManager.Instance.RemoveGadget(this);
            IsSelected = false;
        });
    }

    private void OnEnable()
    {
        SetGadgetUI();
    }

    /// <summary>
    /// GadgetUI Setting
    /// </summary>
    private void SetGadgetUI()
    {
        costTxt.text = data.cost.ToString("F0");
        nameTxt.text = data.name;
        explainTxt.text = data.explain;
        GadgetManager.Instance.SetImageSpriteSize(icon, data.icon);
    }
    //최초로 열 때UI스폰을 해야 할까
    //그냥 처음부터 있을까

}
