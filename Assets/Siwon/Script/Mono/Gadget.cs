using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Gadget : MonoBehaviour
{
    #region UI
    [SerializeField]
    [Tooltip("���� ������")]
    private Image icon;

    [Header("TextMeshPros")]
    [Space(15f)]
    [SerializeField]
    [Tooltip("������ �̸�Text")]
    private TextMeshProUGUI nameTxt;

    [SerializeField]
    [Tooltip("������ ���� ����Text")]
    private TextMeshProUGUI explainTxt;

    [SerializeField]
    [Tooltip("������ ����Text ���Ž� ��Ȱ��ȭ")]
    private TextMeshProUGUI costTxt;

    [Header("Buttons")]
    [Space(15f)]
    [SerializeField]
    private Button buyBtn;

    [SerializeField]
    [Tooltip("���� �����ϴ� ��ưBtn")]
    private Button selectBtn;

    [SerializeField]
    [Tooltip("���� ���� �����ϴ� ��ư")]
    private Button deSelectBtn;
    #endregion

    public int slotIndex;

    public GadgetSlot Slot
    {
        get => GadgetManager.Instance.gadgetSlotList[slotIndex];
    }

    [Tooltip("�� ������ �����Ǿ� �ִ°�")]
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
    [Tooltip("������ �ʿ��� ����Data")]
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
    //���ʷ� �� ��UI������ �ؾ� �ұ�
    //�׳� ó������ ������

}
