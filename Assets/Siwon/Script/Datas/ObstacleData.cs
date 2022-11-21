using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleDatas", menuName = "Datas", order = int.MinValue)]
public class ObstacleData : MonoBehaviour
{
    [Tooltip("드릴 장애물Datas")]
    public List<Obstacle> drillObstacleDatas = new List<Obstacle>();

    [Tooltip("기어 장애물Datas")]
    public List<Obstacle> gearObstacleDatas = new List<Obstacle>();

    [Tooltip("주먹 장애물Datas")]
    public List<Obstacle> fistObstacleDatas = new List<Obstacle>();
    
}
