using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GadgetSlot : MonoBehaviour
{
    [SerializeField]
    private GadgetData data;
    public GadgetData Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
            if (data != null)
            {
                gadgetIcon.gameObject.SetActive(true);
                gadgetIcon.sprite = data.icon;
            }
            else
            {
                gadgetIcon.gameObject.SetActive(false);
                gadgetIcon.sprite = null;
            }
        }
    }

    [Header("GadgetSlot UI")]
    [Tooltip("���� ������")]
    public Image gadgetIcon;

    public int slotIndex;

    private void Start()
    {
        //putOnbtn.onClick.AddListener(() =>
        //{
        //    if (GadgetManager.Instance.IsPutOnMode == false) return;

        //    if (GadgetManager.Instance.IsPutOnMode == true)
        //    {
        //        Data = GadgetManager.Instance.currentSelectGadget.Data;
        //    }
        //});
        StartCoroutine(CCheck());
    }

    private IEnumerator CCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            //print(Data);
        }
    }

    private void Update()
    {

    }

    public void ApplyIcon()
    {
        gadgetIcon.sprite = data.icon;
        gadgetIcon.gameObject.SetActive(true);
    }
}