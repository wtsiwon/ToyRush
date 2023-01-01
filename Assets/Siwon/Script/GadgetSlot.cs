using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GadgetSlot : MonoBehaviour
{
    private GadgetData data;
    public GadgetData Data
    {
        get
        {
            if (data != null)
            {
                gadgetIcon.gameObject.SetActive(true);
                gadgetIcon.sprite = data.icon;
            }
            else
            {
                gadgetIcon.gameObject.SetActive(false);
            }
            return data;
        }
        set
        {
            data = value;
            if (data == null)
            {
                putOnbtn.gameObject.SetActive(false);
                print("GadgetSlot" + isActiveAndEnabled);
            }
            else
            {
                putOnbtn.gameObject.SetActive(true);
                print("GadgetSlot" + isActiveAndEnabled);
                ApplyIcon();
            }
        }
    }

    [Header("GadgetSlot UI")]
    [Space(15f)]
    [SerializeField]
    [Tooltip("ÀåÂø ¹öÆ°")]
    private Button putOnbtn;

    [Tooltip("°¡Á¬ ¾ÆÀÌÄÜ")]
    public Image gadgetIcon;

    public int slotIndex;

    private void Start()
    {
        putOnbtn.onClick.AddListener(() =>
        {
            if (GadgetManager.Instance.IsPutOnMode == false) return;

            if (GadgetManager.Instance.IsPutOnMode == true)
            {
                Data = GadgetManager.Instance.currentSelectGadget.Data;
            }
        });
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