using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AttackPatternDatas", menuName = "Datas", order = int.MinValue)]
public class AttackPatternData : MonoBehaviour
{
    public List<AttackPattern> attackPatternDatas = new List<AttackPattern>();

}
