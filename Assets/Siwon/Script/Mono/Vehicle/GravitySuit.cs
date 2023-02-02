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
    /// Ű �Է�
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
    /// Ż�� Ÿ��
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
