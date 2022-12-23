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
            return data;
        }
        set
        {
            data = value;
            if (data == null)
            {
                print("GadgetSlot" + isActiveAndEnabled);
                gameObject.SetActive(false);
            }
            else
            {
                print("GadgetSlot" + isActiveAndEnabled);
                gameObject.SetActive(true);
                ApplyIcon();
            }
        }
    }

    private void OnEnable()
    {

    }

    [Header("GadgetSlot UI")]
    [Space(15f)]
    [SerializeField]
    [Tooltip("ÀåÂø ¹öÆ°")]
    private Button putOnbtn;

    [Tooltip("°¡Á¬ ¾ÆÀÌÄÜ")]
    public Image gadgetIcon;

    private void Start()
    {
        putOnbtn.onClick.AddListener(() =>
        {
            if (GadgetManager.Instance.IsPutOnMode == true)
            {
                Data = GadgetManager.Instance.currentSelectGadget.Data;
            }
        });
    }

    private void Update()
    {
        
    }
    public void ApplyIcon()
    {
        gadgetIcon.sprite = data.icon;
    }

    private void SetSlot()
    {
        if (Data == null)
        {
            gadgetIcon = null;
        }
    }

}
