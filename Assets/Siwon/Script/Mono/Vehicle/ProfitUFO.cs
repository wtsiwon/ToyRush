using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfitUFO : Vehicle
{
    public ProfitUFO(EVehicleState state) : base(state)
    {

    }

    public override void InputKey()
    {
        if (player.isPressing == true)
        {
            player.rb.AddForce(Vector2.up * player.force);
        }
    }

    protected override void ChangeState()
    {
        animator.SetInteger("State", (int)State);
    }

    protected override void RideVehicle()
    {
        animator.runtimeAnimatorController = player.ufoAnimator;
    }

    protected override void Start()
    {

    }

    protected override void Update()
    {

    }
}
