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
        //Animator_Controller();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    void Animator_Controller(EVehicleType type)
    {
        //if (Input.GetKeyDown)
        //    switch (type)
        //    {
        //        case EVehicleType.None:

        //            break;
        //    }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            animator.SetBool("fly", false);
    }
}
