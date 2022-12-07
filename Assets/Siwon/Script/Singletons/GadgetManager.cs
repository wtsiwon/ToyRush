using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManager : Singleton<GadgetManager>
{
    [Tooltip("gadgetDatas")]
    public List<GadgetData> gadgetDatas = new List<GadgetData>();

    public void SetGadget()
    {

    }

    public List<EGadgetType> gad = new List<EGadgetType>();
}
