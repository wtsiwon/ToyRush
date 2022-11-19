using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private bool Check;

    void Start()
    {
    }

    void Update()
    {
        StartCoroutine(Animator_Controller(Player.Instance.vehicleType));
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Animator_Controller(EVehicleType vehicleType)
    {
        animator.SetBool("Booster", Player.Instance.IsBoosting);

        if (Player.Instance.IsBoosting == true && Check == false)
        {
            Check = true;

            yield return new WaitForSeconds(2);
            animator.SetBool("Booster_Wait", true);

            animator.SetInteger("Booster_Number", ItemManager.inst.boosterNumber);

            switch (ItemManager.inst.boosterNumber)
            {
                case 1:
                    yield return new WaitForSeconds(2);
                    break;
                case 2:
                    yield return new WaitForSeconds(4);
                    break;
                case 3:
                    yield return new WaitForSeconds(2);
                    break;
            }
            Check = false;
            animator.SetBool("Booster_Wait", false);
        }

        if (Player.Instance.isPressing == true && animator.GetBool("Booster_Wait") == false)
        {
            switch (vehicleType)
            {
                case EVehicleType.None:
                    animator.SetBool("fly", true);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") && animator.GetBool("Booster_Wait") == false)
        {
            animator.SetBool("fly", false);

        }
    }
}
