using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    [SerializeField] AudioClip attackSound;
    [SerializeField] int attackCount = 0;
    [SerializeField] Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
       
    }

    public void Walk()
    {

    }

    public void Attack()
    {

    }

    public void Die()
    {
        Destroy(gameObject,0.5f);
    }

    public void OnDamage()
    {
        SoundManager.Instance.Sound(attackSound);
        attackCount++;

        if (attackCount >= 5)
        {
            animator.SetBool("Die", true);
        }
    }
}
