using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ETheme//테마
{

}
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackGround : PoolingObj
{
    private const float SCROLLXPOS = -21f;


    private void Update()
    {
        //거리비례 속도조절
        if(transform.position.x < SCROLLXPOS)
        {
            Return();
        }
    }

    private void OnEnable()
    {
        
    }

    
    public override void Return()
    {
        base.Return();

    }
}
