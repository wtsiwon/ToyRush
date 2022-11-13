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
        StartCoroutine(Animator_Controller(Player.Instance.vehicleType, Player.Instance.boosterType));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.instance.PlaySoundClip("Coin", SoundType.BGM, 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.instance.PlaySoundClip("PlayerRun", SoundType.BGM, 1);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Animator_Controller(EVehicleType vehicleType, EBoosterType boosterType)
    {
        animator.SetBool("Booster", Player.Instance.IsBoosting);

        if (Player.Instance.IsBoosting == true && Check == false)
        {
            Check = true;

            yield return new WaitForSeconds(2);
            animator.SetBool("Booster_Wait", true);

            animator.SetInteger("Booster_Number", ItemManager.inst.boosterNumber);

            yield return new WaitForSeconds(3);
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
