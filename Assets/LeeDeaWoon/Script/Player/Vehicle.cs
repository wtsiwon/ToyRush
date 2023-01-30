using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public Animator animator;

    private EVehicleState state;
    public EVehicleState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    [SerializeField]
    protected RuntimeAnimatorController controller;



    protected virtual void Update()
    {
        RideVahicle();
    }

    private void RideVahicle()
    {
        switch (state)
        {
            case EVehicleState.None:
                
                break;
        }
    }
}
