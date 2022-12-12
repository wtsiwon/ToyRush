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
            gadgetIcon.sprite = data.icon;
        }
    }

    [SerializeField]
    private Image gadgetIcon;

    public void ApplyCheck()
    {
        gadgetIcon.sprite = data.icon;
    }

}
