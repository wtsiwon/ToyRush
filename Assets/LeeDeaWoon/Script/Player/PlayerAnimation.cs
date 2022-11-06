using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
    }

    void Update()
    {
        Animator_Controller(Player.Instance.vehicleType);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Animator_Controller(EVehicleType type)
    {
        if (Player.Instance.isPressing == true && Player.Instance.isBoosting == false)
        {
            switch (type)
            {
                case EVehicleType.None:
                    animator.SetBool("fly", true);
                    break;
            }
        }

        if (Player.Instance.isBoosting == true)
        {
            animator.SetBool("Booster", Player.Instance.isBoosting);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") && Player.Instance.isBoosting == false)
        {
            animator.SetBool("fly", false);
            Debug.Log("asdf");
        }
    }
}
