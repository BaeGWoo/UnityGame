using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManState : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;

    void Awake()
    {
        animator = GetComponent<Animator>();
        direction = new Vector3(0, 0, -1);
        speed = 1.0f;
    }


    void Update()
    {



        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetTrigger("Idle");
            }

            transform.position = transform.position + direction * speed * Time.deltaTime;
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetTrigger("Run");
            }
        }

        

    }


}

