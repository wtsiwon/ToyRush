using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GadgetDatas", menuName = "Datas", order = int.MinValue)]
public class GadgetDatas : ScriptableObject
{
    [Tooltip("GadgetDatas")]
    public List<GadgetData> data = new List<GadgetData>();
    

}
