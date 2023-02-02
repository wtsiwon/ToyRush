using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GravitySuit : Vehicle
{
    public GravitySuit(EVehicleState state) : base(state)
    {
        
    }
    /// <summary>
    /// 키 입력
    /// </summary>
    public override void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.isPressing = true;
        }
        else
        {
            player.isPressing = false;
        }
    }

    /// <summary>
    /// 탈것 타기
    /// </summary>
    protected override void RideVehicle()
    {
        animator.runtimeAnimatorController = player.gravityAnimator;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void ChangeState()
    {

    }
}
