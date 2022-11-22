using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElementSpawner : Singleton<MovingElementSpawner>
{
    public ItemData itemData;

    public ObstacleData obstacleData;

    private void Start()
    {

    }

    /// <summary>
    /// ½ºÆ÷³Ê
    /// </summary>
    /// <returns></returns>
    private IEnumerator CUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

        }

    }

    
    




}
