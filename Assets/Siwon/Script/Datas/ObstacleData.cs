using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleDatas", menuName = "Datas", order = int.MinValue)]
public class ObstacleData : MonoBehaviour
{
    [Tooltip("드릴 장애물 애니메이션")]
    public List<RuntimeAnimatorController> drillAnimator = new List<RuntimeAnimatorController>();

    [Tooltip("기어 장애물 애니메이션")]
    public List<RuntimeAnimatorController> gearAnimator = new List<RuntimeAnimatorController>();
}