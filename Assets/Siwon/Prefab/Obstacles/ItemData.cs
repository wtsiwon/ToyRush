using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDatas", menuName = "Datas", order = int.MinValue)]
public class ItemData : MonoBehaviour
{
    public List<Item> itemDatas = new List<Item>();
}
