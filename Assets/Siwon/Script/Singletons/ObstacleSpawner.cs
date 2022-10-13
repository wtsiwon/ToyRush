using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("장애물을 소환할 위치들")]
    private List<Transform> poses = new List<Transform>();

    private IEnumerator ISpawnPattern1()
    {
        yield return new WaitForSeconds(1f);
        int rand = Random.Range(0, poses.Count);
        //ObjPool.Instance.GetObstacle()
        yield return null;
    }


}
