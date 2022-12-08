using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetManager : Singleton<GadgetManager>
{
    [Tooltip("gadgetDatas")]
    public List<GadgetData> gadgetDatas = new List<GadgetData>();

    [Tooltip("장착하기 전 버튼 UI")]
    public Sprite selectBtnSprite;
    [Tooltip("장착된 버튼 UI")]
    public Sprite selectedBtnSprite;
}
