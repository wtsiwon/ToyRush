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
        StartCoroutine(Animator_Controller(Player.Instance.vehicleType, Player.Instance.boosterType));
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Animator_Controller(EVehicleType vehicleType, EBoosterType boosterType)
    {
        animator.SetBool("Booster", Player.Instance.IsBoosting);

        if (Player.Instance.IsBoosting == true)
        {
            animator.SetInteger("Booster_Number", 0);
            yield return new WaitForSeconds(2);

            switch (boosterType)
            {
                case EBoosterType.Booster500:
                    animator.SetInteger("Booster_Number", 1);
                    break;

                case EBoosterType.Booster1500:
                    animator.SetInteger("Booster_Number", 2);
                    break;

                case EBoosterType.BoosterItem:
                    animator.SetInteger("Booster_Number", 3);
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
        if (collision.transform.CompareTag("Ground") && Player.Instance.IsBoosting == false)
        {
            animator.SetBool("fly", false);
            Debug.Log("asdf");
        }
    }
}
