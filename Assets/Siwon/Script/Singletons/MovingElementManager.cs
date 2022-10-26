using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElementManager : Singleton<MovingElementManager>
{
    [Tooltip("현재 나와있는 MovingElements")]
    public List<MovingElement> movingElementList = new List<MovingElement>();
    //여기서 장애물 상태조정, 아이템상태, 배경의 속도 등을 조정할 예정


    public void BackGroundSpeedSet(float spd)
    {
        foreach (MovingElement background in movingElementList)
        {
            if (background is BackGround)
            {
                background.SetMovingSpd(spd);
            }
        }
    }
    public void ObstacleSpeedSet(float spd)
    {
        foreach (MovingElement obstacle in movingElementList)
        {
            if (obstacle is Obstacle)
            {
                obstacle.SetMovingSpd(spd);
            }
        }
    }
    public void ItemSpeedSet(float spd)
    {
        foreach (MovingElement item in movingElementList)
        {
            if(item is Item)
            {
                item.SetMovingSpd(spd);
            }
        }
    }

    public void MovingElementSpeedSet(float spd)
    {
        foreach(MovingElement movingElement in movingElementList)
        {
            movingElement.SetMovingSpd(spd);
        }
    }
}
