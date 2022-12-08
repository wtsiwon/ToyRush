using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GadgetData", menuName = "Datas/GadgetData", order = int.MinValue)]
public class GadgetData : ScriptableObject
{
    public EGadgetType gedgetType;
    public Sprite icon;
    public string name;
    public string explain;
    public int cost;
    public bool isBought;
}
