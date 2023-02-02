using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMachine : Vehicle
{
    public BirdMachine(EVehicleState state) : base(state)
    {

    }

    public override void InputKey()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            player.isPressing = true;
        }
        else
        {
            player.isPressing = false;
        }
    }

    protected override void ChangeState()
    {
        if (player.isPressing == false)
        {

        }
    }

    protected override void RideVehicle()
    {
        animator.runtimeAnimatorController = player.birdAnimator;
        animator.SetInteger("State", (int)State);
    }
    private void AnimationRun()
    {
        if(player.IsGround == true)
        {
            State = EVehicleState.Run;
        }
        else if(player.IsCelling == true)
        {
            State = EVehicleState.Run;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}