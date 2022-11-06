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
        Animator_Controller(Player.Instance.vehicleType, Player.Instance.boosterType);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Animator_Controller(EVehicleType vehicleType, EBoosterType boosterType)
    {
        animator.SetBool("Booster", Player.Instance.isBoosting);

        if (Player.Instance.isBoosting == true)
        {
            switch (boosterType)
            {
                case EBoosterType.Booster500:
                    animator.SetInteger("Booster_Number", 0);
                    break;

                case EBoosterType.Booster1500:
                    animator.SetInteger("Booster_Number", 1);
                    break;

                case EBoosterType.BoosterItem:
                    animator.SetInteger("Booster_Number", 2);
                    break;
            }

        }

        else
        {
            if (Player.Instance.isPressing == true)
            {
                switch (vehicleType)
                {
                    case EVehicleType.None:
                        animator.SetBool("fly", true);
                        break;
                }
            }
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
